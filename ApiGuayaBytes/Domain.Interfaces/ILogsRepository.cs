namespace Domain.Interfaces
{
    public interface ILogsRepository
    {
        Task AddLogAsync(int idAccion, string descripcion);
    }
}