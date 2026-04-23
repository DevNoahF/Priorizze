using Microsoft.EntityFrameworkCore;
using priorizzeProject.Adapter.Persistence;
using priorizzeProject.Adapter.UseCases;
using priorizzeProject.Core.Interfaces;
using System.Net.Sockets;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration.");
}

// Add DbContext with MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 0)),
        mySqlOptions =>
        {
            mySqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
        }
    ),
    ServiceLifetime.Scoped);

// ============================================
// Add other services
// ============================================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<CreateKeyResultUseCase>();
builder.Services.AddScoped<CreateJiraSyncConfigUseCase>();
builder.Services.AddScoped<IJiraProjectSyncUseCase, SyncJiraProjectUseCase>();

builder.Services.AddScoped<IUserUseCase, CreateUserUseCase>();
builder.Services.AddScoped<IKeyResultUseCase, CreateKeyResultUseCase>();
builder.Services.AddScoped<IJiraSyncConfigUseCase, CreateJiraSyncConfigUseCase>();

builder.Services.AddHttpClient();

var app = builder.Build();

// Apply database migrations automatically in development (with error handling)
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
        .CreateLogger("Startup");

    var connectionBuilder = new MySqlConnectionStringBuilder(connectionString);
    var databaseHost = connectionBuilder.Server;
    var databasePort = (int)connectionBuilder.Port;

    if (string.IsNullOrWhiteSpace(databaseHost) || databasePort <= 0 ||
        !IsTcpPortOpen(databaseHost, databasePort, TimeSpan.FromSeconds(2)))
    {
        logger.LogWarning(
            "MySQL is not reachable at {Host}:{Port}. Start it (e.g. 'docker compose up -d') or update the connection string.",
            databaseHost,
            databasePort);
    }
    else
    {
        var maxAttempts = 5;
        var delay = TimeSpan.FromSeconds(2);

        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
                logger.LogInformation("Database migrations applied successfully");
                break;
            }
            catch (Exception ex) when (attempt < maxAttempts)
            {
                logger.LogWarning(ex, "Database not ready yet (attempt {Attempt}/{MaxAttempts}). Retrying...", attempt, maxAttempts);
                Task.Delay(delay).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Database initialization deferred. The database will be created when first accessed.");
            }
        }
    }
}

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();

static bool IsTcpPortOpen(string host, int port, TimeSpan timeout)
{
    try
    {
        using var client = new TcpClient();
        var connectTask = client.ConnectAsync(host, port);
        return connectTask.Wait(timeout) && client.Connected;
    }
    catch
    {
        return false;
    }
}
