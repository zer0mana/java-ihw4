using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthorizationMicroservice.Options;
using AuthorizationMicroservice.Repositories;
using AuthorizationMicroservice.Repositories.Entities;
using AuthorizationMicroservice.Repositories.Migrations;
using Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationMicroservice.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly UserRepository _userRepository;
    private readonly SessionRepository _sessionRepository;
    
    public AuthorizationService(
        UserRepository userRepository,
        SessionRepository sessionRepository)
    {
        _userRepository = userRepository;
        _sessionRepository = sessionRepository;
    }

    public async Task<int> AddUser(UserEntityV1 user)
    {
        var id = (await _userRepository.AddUsers(new[] { user })).First();

        return id;
    }

    public async Task<UserEntityV1> FindUserByEmail(UserEntityV1 user)
    {
        var checkUser = await _userRepository.GetUserByEmail(user);

        return checkUser;
    }

    public async Task<SessionEntityV1> GetSessionByUserId(int userID)
    {
        return await _sessionRepository.GetSessionByUserId(userID);
    }
    
    public async Task<SessionEntityV1> GetSessionByToken(string token)
    {
        return await _sessionRepository.GetSessionByToken(token);
    }

    public async Task<int[]> AddSession(SessionEntityV1 session)
    {
        var ids = await _sessionRepository.AddSessions(new[] { session });

        return ids;
    }

    public async Task<UserEntityV1> GetUserById(int id)
    {
        var user = await _userRepository.GetUserById(id);

        return user;
    }

    public string GetJwtToken(UserEntityV1 user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            JwtConfig.Issuer,
            JwtConfig.Audience,
            new[] {new Claim("userId", user.Id.ToString())},
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<int> UpdateSession(int id, DateTimeOffset offset)
    {
        var res = await _sessionRepository.UpdateSession(id, offset);

        return res;
    }
}