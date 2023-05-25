using System.Transactions;
using Npgsql;
using OrderHandlerMicroservice.Repositories.Interfaces;

namespace OrderHandlerMicroservice.Repositories;

public class BaseRepository : IBaseRepository
{
    private readonly string ConnectionString =
        "User ID=postgres;Password=123456;Host=localhost;Port=15432;Database=order-handler;Pooling=true;";

    public async Task<NpgsqlConnection> GetAndOpenConnection()
    {
        var connection = new NpgsqlConnection(ConnectionString);
        await connection.OpenAsync();
        connection.ReloadTypes();
        return connection;
    }

    public TransactionScope CreateTransactionScope(
        IsolationLevel level = IsolationLevel.ReadCommitted)
    {
        return new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions
            {
                IsolationLevel = level,
                Timeout = TimeSpan.FromSeconds(5)
            },
            TransactionScopeAsyncFlowOption.Enabled);
    }
}