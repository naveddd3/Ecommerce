using Domain.Entities;

namespace Application.Interfaces
{
    public interface ILoggerService
    {
        void LogError(ErrorLog errorLog);
        Task<IEnumerable<ErrorLog>> GetAllLogsAsync();
    }
}
