using AuthorizationMicroservice.Repositories.Entities;

namespace Interfaces;

public interface IAuthorizationService
{
    public Task<int> AddUser(UserEntityV1 user);

    public Task<UserEntityV1> FindUserByEmail(UserEntityV1 user);

    public Task<SessionEntityV1> GetSessionByUserId(int userID);

    public Task<SessionEntityV1> GetSessionByToken(string token);

    public Task<int[]> AddSession(SessionEntityV1 session);

    public Task<UserEntityV1> GetUserById(int id);

    public string GetJwtToken(UserEntityV1 user);
}