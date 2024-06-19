using System.Data;

namespace Kiron.Application.Interfaces;

public interface IDBConnectionManager : IDisposable
{
    IDbConnection GetOpenConnection();
    void ReleaseConnection(IDbConnection connection);
}
