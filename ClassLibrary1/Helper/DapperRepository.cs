using Dapper;
using Microsoft.Data.SqlClient;
using static Dapper.SqlMapper;
using System.Data.Common;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace Domain.Helper
{
    public class DapperRepository : IDapperRepository, IDisposable
    {
        private readonly IConfiguration _config;
        private readonly ILogger<DapperRepository> _logger;
        private readonly string Connectionstring = "SqlConnection";
        public DapperRepository(IConfiguration config, Iconnectionstring connectionString, ILogger<DapperRepository> logger)
        {
            _config = config;
            Connectionstring = connectionString.Connectionstring;
            _logger = logger;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public async Task<int> ExecuteAsync(string sp, object param = null, CommandType commandType = CommandType.StoredProcedure)
        {
            int i = -1;
            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    i = await db.ExecuteAsync(sp, param, commandType: commandType);
                }
            }
            catch (Exception ex)
            {
                var w32ex = ex as SqlException;
                if (w32ex == null)
                {
                    w32ex = ex.InnerException as SqlException;
                }
                if (w32ex != null)
                {
                    i = w32ex.Number;
                }
                _logger.LogError(ex, ex.Message);
            }
            return i;
        }
        public async Task<T> GetByDynamicParamAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            T result;
            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var response = await db.QueryAsync<T>(sp, parms, commandType: commandType);
                    result = response.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
            return result;
        }
        public async Task<T> GetAsync<T>(string sp, object parms = null, CommandType commandType = CommandType.Text)
        {
            T result;
            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var response = await db.QueryAsync<T>(sp, parms, commandType: commandType);
                    result = response.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
            return result;
        }

        public IQueryable<T> GetAsQueryable<T>(string sp, object parms = null, CommandType commandType = CommandType.Text)
        {
            IQueryable<T> result;
            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var response = db.Query<T>(sp, parms, commandType: commandType);
                    result = response.AsQueryable();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var result = await db.QueryAsync<T>(sp, parms, commandType: commandType);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new List<T>();
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(string sp, object parms = null, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var result = await db.QueryAsync<T>(sp, parms, commandType: commandType);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new List<T>();
            }
        }
        public async Task<T> InsertAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using (IDbConnection db = new SqlConnection(Connectionstring))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    using (var tran = db.BeginTransaction())
                    {
                        try
                        {
                            var resultAsync = await db.QueryAsync<T>(sp, parms, commandType: commandType, transaction: tran);
                            result = resultAsync.FirstOrDefault();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            _logger.LogError(ex, ex.Message);
                            throw ex;
                        }
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    throw ex;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        db.Close();
                }
            }
            return result;
        }

        public async Task<T> UpdateAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using (IDbConnection db = new SqlConnection(Connectionstring))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    using (var tran = db.BeginTransaction())
                    {
                        try
                        {
                            var resultAsync = await db.QueryAsync<T>(sp, parms, commandType: commandType, transaction: tran);
                            result = resultAsync.FirstOrDefault();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            _logger.LogError(ex, ex.Message);
                            throw ex;
                        }
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    throw ex;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        db.Close();
                }

            }

            return result;
        }

        public async Task<dynamic> GetMultipleAsync<T1, T2>(string sp, object parms, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var result = await db.QueryMultipleAsync(sp, parms, commandType: commandType).ConfigureAwait(false);
                    var res = new
                    {
                        Table1 = result.Read<T1>(),
                        Table2 = result.Read<T2>()
                    };
                    return res;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
        public async Task<dynamic> GetMultipleAsync<T1, T2, T3>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var result = await db.QueryMultipleAsync(sp, parms, commandType: commandType).ConfigureAwait(false);
                    var res = new
                    {
                        Table1 = result.Read<T1>(),
                        Table2 = result.Read<T2>(),
                        Table3 = result.Read<T3>(),
                    };
                    return res;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<GridReader> GetMultipleAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var result = await db.QueryMultipleAsync(sp, parms, commandType: commandType).ConfigureAwait(false);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public DbConnection GetDbconnection() => new SqlConnection(_config.GetConnectionString(Connectionstring));

        public IDbConnection GetMasterConnection() => new SqlConnection(_config.GetConnectionString("MasterConnection"));

        public IEnumerable<TReturn> Get<T1, T2, TReturn>(string sqlQuery, Func<T1, T2, TReturn> p, string splitOn, DynamicParameters parms = null, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var result = db.Query<T1, T2, TReturn>(sqlQuery, p, splitOn: splitOn, param: parms, commandType: commandType);
                    return result;
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<TReturn>> GetAllAsync<T1, T2, TReturn>(string sqlQuery, object parms, Func<T1, T2, TReturn> p, string splitOn, CommandType commandType)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var result = await db.QueryAsync<T1, T2, TReturn>(sqlQuery, p, splitOn: splitOn, param: parms, commandType: commandType);
                    return result;
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<TReturn>> GetAllAsync<T1, T2, T3, TReturn>(string sqlQuery, object parms, Func<T1, T2, T3, TReturn> p, string splitOn, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var result = await db.QueryAsync<T1, T2, T3, TReturn>(sqlQuery, p, splitOn: splitOn, param: parms, commandType: commandType);
                    return result;
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
        public async Task<IEnumerable<TReturn>> GetAllAsyncProc<T1, T2, T3, T4, TReturn>(T1 entity, string sqlQuery
            , DynamicParameters parms, Func<T1, T2, T3, T4, TReturn> p, string splitOn)
        {
            try
            {
                //var prepared = PrepareParameters(sqlQuery, entity.ToDictionary());
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var result = await db.QueryAsync<T1, T2, T3, T4, TReturn>(sqlQuery, p, splitOn: splitOn, param: parms, commandType: CommandType.StoredProcedure);
                    return result;
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<TReturn>> GetAllAsyncProc<T1, T2, T3, T4, T5, T6, T7, TReturn>(T1 entity, string sqlQuery,
            DynamicParameters parms, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> p, string splitOn)
        {
            try
            {
                //var prepared = PrepareParameters(sqlQuery, entity.ToDictionary());
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var result = await db.QueryAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(sqlQuery, p, splitOn: splitOn, param: parms, commandType: CommandType.StoredProcedure);
                    return result;
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
        public async Task<IEnumerable<TReturn>> GetMultiSplit<T1, T2, TReturn>(string sqlQuery, Func<T1, T2, TReturn> p, string splitOn, DynamicParameters parms = null, CommandType commandType = CommandType.StoredProcedure)
        {

            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var result = await db.QueryAsync<T1, T2, TReturn>(sqlQuery, p, splitOn: splitOn, param: parms, commandType: commandType);
                    return result;
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
        public async Task<IEnumerable<TReturn>> GetAsync<T1, T2, TReturn>(string sqlQuery, Func<T1, T2, TReturn> p, string splitOn, DynamicParameters parms = null, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var result = await db.QueryAsync<T1, T2, TReturn>(sqlQuery, p, splitOn: splitOn, param: parms, commandType: commandType);
                    return result;
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public Task<IEnumerable<T>> GetAllAsync<T>(T entity, string sp)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TReturn>> GetAllAsyncProc<T1, T2, T3, TReturn>(T1 entity, string sqlQuery, DynamicParameters parms, Func<T1, T2, T3, TReturn> p, string splitOn)
        {
            throw new NotImplementedException();
        }
    }
}
