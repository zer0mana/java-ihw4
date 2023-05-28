using AuthorizationMicroservice.Options;
using AuthorizationMicroservice.Repositories.Entities;
using AuthorizationMicroservice.Requests;
using AuthorizationMicroservice.Responses;
using AuthorizationMicroservice.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationMicroservice.Controllers;

[ApiController]
[Route("authorization-controller")]
public class AuthorizationController : ControllerBase
{
    private readonly AuthorizationService _authorizationService;

    public AuthorizationController(AuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }
    
    [HttpPost]
    [Route("register-user")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var user = new UserEntityV1(
            0,
            request.Username,
            request.Email,
            request.Password,
            request.Role,
            DateTimeOffset.Now,
            DateTimeOffset.Now);

        var checkUser = await _authorizationService.FindUserByEmail(user);

        if (checkUser != null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "User with this email already exist.");
        }

        if (!Role.Types.Contains(user.Role))
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Role should be 'customer' or 'manager'.");
        }

        var hasher = new PasswordHasher<UserEntityV1>();
        user.PasswordHash = hasher.HashPassword(user, user.PasswordHash);

        var id = await _authorizationService.AddUser(user);
        
        return StatusCode(StatusCodes.Status200OK, "User successfully register.");
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginUserRequest request)
    {
        var user = new UserEntityV1(
            0,
            "",
            request.Email,
            "",
            "",
            DateTimeOffset.Now,
            DateTimeOffset.Now);

        var checkUser = await _authorizationService.FindUserByEmail(user);

        if (checkUser == null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "User with this email not exist.");
        }

        user = checkUser;
        
        var passwordHasher = new PasswordHasher<UserEntityV1>();
        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (result != PasswordVerificationResult.Success)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Wrong password.");
        }

        var token = _authorizationService.GetJwtToken(user);
        var signature = token.Split('.')[2];

        var session = await _authorizationService.GetSessionByUserId(user.Id);

        if (session == null)
        {
            await _authorizationService.AddSession(new SessionEntityV1(
                0, 
                user.Id,
                signature,
                DateTimeOffset.Now));
        }
        else
        {
            await _authorizationService.UpdateSession(session.Id, DateTimeOffset.Now);
        }
        
        return StatusCode(StatusCodes.Status200OK, $"You successfully login, your token: {signature}");
    }

    [HttpPost]
    [Route("user-info")]
    public async Task<IActionResult> GetUserById(GetUserByTokenRequest request)
    {
        var session = await _authorizationService.GetSessionByToken(request.Token);

        if (session == null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Bad token");
        }

        var user = await _authorizationService.GetUserById(session.UserId);

        return StatusCode(StatusCodes.Status200OK, $"user info:" +
                                                   $"\n\tid: {user.Id}" +
                                                   $"\n\tusername: {user.Username}" +
                                                   $"\n\temail: {user.Email}" +
                                                   $"\n\trole: {user.Role}");
    }
}