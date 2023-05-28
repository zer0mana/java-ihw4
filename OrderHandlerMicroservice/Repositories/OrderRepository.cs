using Dapper;
using OrderHandlerMicroservice.Repositories.Entities;

namespace OrderHandlerMicroservice.Repositories;

public class OrderRepository : BaseRepository
{
    public async Task<int[]> Add(
        OrderEntityV1[] entityV1,
        CancellationToken token)
    {
        const string sqlQuery = @"
insert into ""order"" (user_id, status, special_requests, created_at, updated_at)
select user_id, status, special_requests, created_at, updated_at
  from UNNEST(@Orders)
returning id;
";
        
        var sqlQueryParams = new
        {
            Orders = entityV1
        };
        
        await using var connection = await GetAndOpenConnection();
        var result = await connection.QueryAsync<int>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: token));
        
        return result.ToArray();
    }

    public async Task<OrderEntityV1[]> GetWaitingOrders(int count)
    {
        string sqlQuery = @"
select id, 
       user_id, 
       status, 
       special_requests, 
       created_at, 
       updated_at
from ""order""
where status = @Status
order by created_at desc
limit @Limit
";
        
        var sqlQueryParams = new
        {
            @Status = "В ожидании",
            @Limit = count
        };

        await using var connection = await GetAndOpenConnection();
        var orders = await connection.QueryAsync<OrderEntityV1>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
        
        return orders.ToArray();
    }

    public async void UpdateOrder(int orderId, string orderStatus)
    {
        string sqlQuery = @"
update ""order""
set status = @Status, updated_at = @At
where id = @OrderId
";
        
        var sqlQueryParams = new
        {
            @Status = orderStatus,
            @At = DateTimeOffset.Now,
            @OrderId = orderId
        };

        await using var connection = await GetAndOpenConnection();
        var orders = await connection.QueryAsync<OrderEntityV1>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
    }
}