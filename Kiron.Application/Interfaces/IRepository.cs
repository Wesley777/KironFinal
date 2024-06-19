using System.Data;

namespace Kiron.Application.Interfaces;

public interface IRepository
{
    Task<T> ExecuteStoredProcedureAsync<T>(string storedProcedure, object? parameters = null); 
    Task<IEnumerable<T>> ExecuteStoredProcedureManyAsync<T>(string storedProcedure, object? parameters = null);
}
