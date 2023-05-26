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

    public void Get()
    {
        throw new NotImplementedException();
    }

    public void Remove()
    {
        throw new NotImplementedException();
    }
}