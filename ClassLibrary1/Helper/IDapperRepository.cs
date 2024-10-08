﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using static Dapper.SqlMapper;

namespace Domain.Helper
{
    public interface IDapperRepository
    {
        DbConnection GetDbconnection();
        IDbConnection GetMasterConnection();
        Task<T> GetByDynamicParamAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<T> GetAsync<T>(string sp, object parms = null, CommandType commandType = CommandType.StoredProcedure);
        IQueryable<T> GetAsQueryable<T>(string sp, object parms = null, CommandType commandType = CommandType.Text);
        Task<IEnumerable<T>> GetAllAsync<T>(string sp, object parms = null, CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<T>> GetAllAsync<T>(T entity, string sp);
        Task<IEnumerable<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<int> ExecuteAsync(string sp, object parms = null, CommandType commandType = CommandType.StoredProcedure);
        Task<T> InsertAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<T> UpdateAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<TReturn>> GetAllAsync<T1, T2, TReturn>(string sqlQuery, object param, Func<T1, T2, TReturn> p, string splitOn, CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<TReturn>> GetAllAsync<T1, T2, T3, TReturn>(string sqlQuery, object param, Func<T1, T2, T3, TReturn> p, string splitOn, CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<TReturn>> GetAllAsyncProc<T1, T2, T3, TReturn>(T1 entity, string sqlQuery, DynamicParameters parms,
            Func<T1, T2, T3, TReturn> p, string splitOn);
        Task<IEnumerable<TReturn>> GetAllAsyncProc<T1, T2, T3, T4, TReturn>(T1 entity, string sqlQuery, DynamicParameters parms, Func<T1, T2, T3, T4, TReturn> p, string splitOn);
        Task<IEnumerable<TReturn>> GetAllAsyncProc<T1, T2, T3, T4, T5, T6, T7, TReturn>(T1 entity, string sqlQuery, DynamicParameters parms, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> p, string splitOn);
        Task<dynamic> GetMultipleAsync<T1, T2>(string sp, object parms, CommandType commandType = CommandType.StoredProcedure);
        Task<dynamic> GetMultipleAsync<T1, T2,T3>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        Task<GridReader> GetMultipleAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }
}
