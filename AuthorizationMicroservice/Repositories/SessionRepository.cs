using Dapper;
using AuthorizationMicroservice.Repositories.Entities;

namespace AuthorizationMicroservice.Repositories.Migrations;

public class SessionRepository : BaseRepository
{
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
    
    public async Task<SessionEntityV1> GetSessionByUserId(int id)
    {
        string sqlQuery = @"
select id, user_id, session_token, expires_at
from ""session""
where user_id = @Id
limit 1
";
        
        var sqlQueryParams = new
        {
            @Id = id,
        };

        await using var connection = await GetAndOpenConnection();
        var orders = await connection.QueryAsync<SessionEntityV1>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
        
        return orders.FirstOrDefault();
    }
    
    public async Task<int[]> AddSessions(SessionEntityV1[] sessions)
    {
        const string sqlQuery = @"
insert into ""session"" (user_id, session_token, expires_at)
select user_id, session_token, expires_at
  from UNNEST(@Sessions)
returning id;
";
        
        var sqlQueryParams = new
        {
            Sessions = sessions
        };
        
        await using var connection = await GetAndOpenConnection();
        var result = await connection.QueryAsync<int>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
        
        return result.ToArray();
    }

    public async Task<int> UpdateSession(int id, DateTimeOffset offset)
    {
        string sqlQuery = @"
update ""session""
set expires_at = @offset
where id = @Id
";
        
        var sqlQueryParams = new
        {
            @Offset = offset,
            @Id = id
        };

        await using var connection = await GetAndOpenConnection();
        var orders = await connection.QueryAsync<SessionEntityV1>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
        
        return id;
    }
}