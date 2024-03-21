using Domain.Interfaces;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;


namespace Domain.Repository
{
    public class LogsRepository : ILogsRepository
    {
        private readonly DataContext Context;
        public LogsRepository(DataContext context) 
        {
            Context = context;
        }
        public async Task AddLogAsync(int idAccion, string descripcion)
        {
            // Crear un nuevo registro de log
            var log = new Logs
            {
                IdAccion = idAccion,
                Descripcion = descripcion,
                DateLog = DateTime.Now
            };

            // Agregar el nuevo registro de log al contexto
            Context.Logs.Add(log);

            // Guardar los cambios en la base de datos
            await Context.SaveChangesAsync();
        }
    }
}