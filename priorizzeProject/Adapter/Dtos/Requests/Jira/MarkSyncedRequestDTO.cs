namespace priorizzeProject.Adapter.Dtos.Requests;

public sealed class MarkSyncedRequestDTO
{
    public DateTime SyncTime { get; set; }
}

// Criado para marcar a última sincronização do Jira, pois essa é uma ação específica e não uma atualização completa da configuração de sincronização.