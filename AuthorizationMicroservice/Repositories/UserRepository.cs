using Dapper;
using AuthorizationMicroservice.Repositories.Entities;

namespace AuthorizationMicroservice.Repositories;

public class UserRepository : BaseRepository
{
    public async Task<UserEntityV1> GetUserByEmail(UserEntityV1 user)
    {
        string sqlQuery = @"
select id, 
       username, 
       email, 
       password_hash,
       role,
       created_at, 
       updated_at
from ""user""
where email = @Email
limit 1
";
        
        var sqlQueryParams = new
        {
            @Email = user.Email,
        };

        await using var connection = await GetAndOpenConnection();
        var orders = await connection.QueryAsync<UserEntityV1>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
        
        return orders.FirstOrDefault();
    }
    
    public async Task<UserEntityV1> GetUserById(int id)
    {
        string sqlQuery = @"
select id, 
       username, 
       email, 
       password_hash,
       role,
       created_at, 
       updated_at
from ""user""
where id = @Id
limit 1
";
        
        var sqlQueryParams = new
        {
            @Id = id,
        };

        await using var connection = await GetAndOpenConnection();
        var orders = await connection.QueryAsync<UserEntityV1>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
        
        return orders.FirstOrDefault();
    }

    public async Task<int[]> AddUsers(UserEntityV1[] users)
    {
        const string sqlQuery = @"
insert into ""user"" (username, email, password_hash, role, created_at, updated_at)
select username, email, password_hash, role, created_at, updated_at
  from UNNEST(@Users)
returning id;
";
        
        var sqlQueryParams = new
        {
            Users = users
        };
        
        await using var connection = await GetAndOpenConnection();
        var result = await connection.QueryAsync<int>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
        
        return result.ToArray();
    }
}