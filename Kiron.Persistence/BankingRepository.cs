using System.Data;
using Dapper;
using Kiron.Application.Interfaces;
using Kiron.Application.Models.ThirdPartyModels;

namespace Kiron.Persistence;

public class BankingRepository : IBankingRepository
{
    private readonly IDBConnectionManager _connectionManager;

    public BankingRepository(IDBConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task SaveBankingHolidays(RootObject rootObject)
    {
        var bankingHolidays = rootObject;

        using (var connection = _connectionManager.GetOpenConnection())
        {
            var transaction = connection.BeginTransaction();

            try
            {
                await InsertHolidaysAsync(bankingHolidays.EnglandAndWales.events, "England and Wales", connection, transaction);
                await InsertHolidaysAsync(bankingHolidays.Scotland.events, "Scotland", connection, transaction);
                await InsertHolidaysAsync(bankingHolidays.NorthernIreland.events, "Northern Ireland", connection, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Failed to save banking holidays.", ex);
            }
            finally
            {
                _connectionManager.ReleaseConnection(connection);
            }
        }
    }

    async Task InsertHolidaysAsync(IEnumerable<Event> holidays, string regionName, IDbConnection connection, IDbTransaction transaction)
    {
        foreach (var item in holidays)
        {
            await connection.ExecuteAsync("InsertHoliday",
                new { Title = item.title, Date = item.date, Notes = item.notes, Bunting = item.bunting, RegionName = regionName },
                commandType: CommandType.StoredProcedure,
                transaction: transaction);
        }
    }

}