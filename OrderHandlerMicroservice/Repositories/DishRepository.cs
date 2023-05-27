using Dapper;
using OrderHandlerMicroservice.Repositories.Entities;

namespace OrderHandlerMicroservice.Repositories;

public class DishRepository : BaseRepository
{
    public async Task<int[]> Add(
        DishEntityV1[] entityV1,
        CancellationToken token)
    {
        const string sqlQuery = @"
insert into dish (name, description, price, quantity)
select name, description, price, quantity
  from UNNEST(@Dishes)
returning id;
";
        
        var sqlQueryParams = new
        {
            Dishes = entityV1
        };
        
        await using var connection = await GetAndOpenConnection();
        var result = await connection.QueryAsync<int>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: token));
        
        return result.ToArray();
    }

    public DishEntityV1[] GetAll()
    {
        throw new NotImplementedException();
    }

    public void Remove()
    {
        throw new NotImplementedException();
    }
}