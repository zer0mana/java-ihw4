using AuthorizationMicroservice.Repositories.Entities;
using Dapper;

namespace OrderHandlerMicroservice.Repositories;

public class AuthorizationRepository : BaseRepository
{
    public AuthorizationRepository()
    {
        ConnectionString =
            "User ID=postgres;Password=123456;Host=localhost;Port=15433;Database=authorization;Pooling=true;";   
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
    
    public async Task<SessionEntityV1> GetSessionByToken(string token)
    {
        string sqlQuery = @"
select id, user_id, session_token, expires_at
from ""session""
where session_token = @Token
limit 1
";
        
        var sqlQueryParams = new
        {
            @Token = token,
        };

        await using var connection = await GetAndOpenConnection();
        var orders = await connection.QueryAsync<SessionEntityV1>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
        
        return orders.FirstOrDefault();
    }
}