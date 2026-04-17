// Minimal Program.cs for ASP.NET Core (.NET 10)

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers and a simple root redirect to Swagger UI
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
