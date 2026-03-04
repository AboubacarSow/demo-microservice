using System.Collections.Concurrent;
using System.Threading.Tasks;
using authentication.Dtos;
using authentication.Models;
using authentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace authentication.Controllers;

[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly static ConcurrentDictionary<string, User> Users = new();
    [HttpPost]
    public  IActionResult Login([FromServices] JwtService jwtService, [FromBody] LoginDto login)
    {
        var email = Users.Keys.FirstOrDefault(k => k == login.Email);
        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest("Invalid Credentials");
        }
        Users.TryGetValue(email, out User? user);
        if (!(user?.Password == login.Password))
        {
            return BadRequest("Invalid Credentials");
        }
        var token =  jwtService.CreateToken(user);
        return Ok(token);
    }
    public IActionResult Register([FromBody] RegisterDto registerDto )
    {
        var email = Users.Keys.FirstOrDefault(k => k == registerDto.Email);
        if (!string.IsNullOrWhiteSpace(email))
        {
            return BadRequest("User alredy exist");
        }
        var isSuccess = Users.TryAdd(registerDto.Email,new User(registerDto.Name,registerDto.Email,registerDto.Password));
        if(!isSuccess)
            return BadRequest("Invalid request");
        return Ok("User registered successfully");
    }
}