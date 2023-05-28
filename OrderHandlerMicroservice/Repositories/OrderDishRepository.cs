using Dapper;
using OrderHandlerMicroservice.Repositories.Entities;

namespace OrderHandlerMicroservice.Repositories.Migrations;

public class OrderDishRepository : BaseRepository
{
    public async Task<int[]> Add(OrderDishEntityV1[] entityV1)
    {
        const string sqlQuery = @"
insert into order_dish (order_id, dish_id, quantity, price)
select order_id, dish_id, quantity, price
  from UNNEST(@OrderDishes)
returning id;
";
        
        var sqlQueryParams = new
        {
            OrderDishes = entityV1
        };
        
        await using var connection = await GetAndOpenConnection();
        var result = await connection.QueryAsync<int>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
        
        return result.ToArray();
    }

    public async Task<OrderDishEntityV1[]> GetByOrderId(int orderId)
    {
        string sqlQuery = @"
select id, 
       order_id, 
       dish_id, 
       quantity, 
       price
from order_dish
where order_id = @OrderId
order by id
";
        
        var sqlQueryParams = new
        {
            OrderId = orderId
        };

        await using var connection = await GetAndOpenConnection();
        var dishes = await connection.QueryAsync<OrderDishEntityV1>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
        
        return dishes
            .ToArray();
    }

    public void Remove()
    {
        throw new NotImplementedException();
    }
}