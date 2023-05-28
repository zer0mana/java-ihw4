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

    public async Task<DishEntityV1[]> GetDishesById(
        int[] dishesIds)
    {
        string sqlQuery = @"
select id
     , name
     , description
     , price
     , quantity
from dish
where id = any(@DishesIds)
order by id
";
        
        var sqlQueryParams = new
        {
            DishesIds = dishesIds
        };

        await using var connection = await GetAndOpenConnection();
        var dishes = await connection.QueryAsync<DishEntityV1>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
        
        return dishes
            .ToArray();
    }
    
    public async Task<DishEntityV1[]> GetAllDishes()
    {
        string sqlQuery = @"
select id
     , name
     , description
     , price
     , quantity
from dish
order by id
";

        await using var connection = await GetAndOpenConnection();
        var dishes = await connection.QueryAsync<DishEntityV1>(
            new CommandDefinition(
                sqlQuery));
        
        return dishes
            .ToArray();
    }

    public async void Update(DishEntityV1[] entityV1)
    {
        const string sqlQuery = @"
with table_1 (id, name, description, price, quantity) as
(select id, name, description, price, quantity
  from UNNEST(@Dishes))
update dish
set quantity = table_1.quantity
from table_1
where dish.id = table_1.id
";
        
        var sqlQueryParams = new
        {
            Dishes = entityV1
        };
        
        await using var connection = await GetAndOpenConnection();
        var result = await connection.QueryAsync<int>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
    }
}