using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using Kiron.Application.Interfaces;

namespace Kiron.Infrastructure.Managers;

public class DBConnectionManager : IDBConnectionManager
{
    private readonly string _connectionString;
    private readonly ConcurrentBag<SqlConnection> _connectionPool;
    private int _openConnections = 0;
    private readonly object _lock = new object();

    public DBConnectionManager(string connectionString)
    {
        _connectionString = connectionString;
        _connectionPool = new ConcurrentBag<SqlConnection>();
    }

    public IDbConnection GetOpenConnection()
    {
        lock (_lock)
        {
            if (_openConnections >= 10)
            {
                throw new InvalidOperationException("Maximum number of open connections exceeded.");
            }

            if (_connectionPool.TryTake(out var connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                _openConnections++;
                return connection;
            }
            else
            {
                var newConnection = new SqlConnection(_connectionString);
                newConnection.Open();
                _openConnections++;
                return newConnection;
            }
        }
    }

    public void ReleaseConnection(IDbConnection connection)
    {

        connection.Close();
        _openConnections--;
    }

    public void Dispose()
    {
        foreach (var connection in _connectionPool)
        {
            connection.Dispose();
        }
    }
}

