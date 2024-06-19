using System.Data;
using Dapper;
using Kiron.Application.Interfaces;

namespace Kiron.Persistence;

public class DapperRepository : IRepository
{
    private readonly IDBConnectionManager _connectionManager;

    public DapperRepository(IDBConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task<T> ExecuteStoredProcedureAsync<T>(string storedProcedure, object? parameters = null)
    {
        using (var connection = _connectionManager.GetOpenConnection())
        {
            var result = await connection.QuerySingleOrDefaultAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            _connectionManager.ReleaseConnection(connection);
            return result;
        }
    }

    public async Task<IEnumerable<T>> ExecuteStoredProcedureManyAsync<T>(string storedProcedure, object? parameters = null)
    {
        using (var connection = _connectionManager.GetOpenConnection())
        {
            var result = await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            _connectionManager.ReleaseConnection(connection);
            return result;
        }
    }

}