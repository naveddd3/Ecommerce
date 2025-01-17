using Application.Interfaces;
using Domain.Entities;
using Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly IDapperRepository _dbContext;
        public LoggerService(IDapperRepository dapperRepository)
        {
            _dbContext = dapperRepository;
        }
        public async void LogError(ErrorLog errorLog)
        {
            const string query = @"
            INSERT INTO ErrorLogs (Message, StackTrace, LogDate, LogLevel)
            VALUES (@Message, @StackTrace, @LogDate, @LogLevel)";

            _ = await _dbContext.GetAsync<Response>(query, new
            {
                errorLog.Message,
                errorLog.StackTrace,
                errorLog.LogDate,
                errorLog.LogLevel
            }, System.Data.CommandType.Text);
        }
        public async Task<IEnumerable<ErrorLog>> GetAllLogsAsync()
        {
            const string query = "SELECT * FROM ErrorLogs ORDER BY LogDate DESC";
            var response = await _dbContext.GetAllAsync<ErrorLog>(query, null, System.Data.CommandType.Text);
            return response;
        }
    }
}
