using Dapper;

namespace OrderHandlerMicroservice.Repositories;

public class OrderRepository : BaseRepository
{
    public async void Add()
    {
        const string sqlQuery = @"
insert into order_dish (1, 1, 1, 1, 1.0)";

        await using var connection = await GetAndOpenConnection();
        await connection.QueryAsync(new CommandDefinition(sqlQuery));
    }

    public void Get()
    {
        throw new NotImplementedException();
    }

    public void Remove()
    {
        throw new NotImplementedException();
    }
}