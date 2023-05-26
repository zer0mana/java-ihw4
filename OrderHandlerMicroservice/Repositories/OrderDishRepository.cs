using Dapper;
using OrderHandlerMicroservice.Repositories.Entities;

namespace OrderHandlerMicroservice.Repositories.Migrations;

public class OrderDishRepository : BaseRepository
{
    public async void Add()
    {
        const string sqlQuery = @"
insert into order_dish (order_id, dish_id, quantity, price) values(1, 1, 1, 1);
";

        await using var connection = await GetAndOpenConnection();
        await connection.QueryAsync(
            new CommandDefinition(
                sqlQuery));
        
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