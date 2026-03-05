using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using authentication.Models;
using Microsoft.IdentityModel.Tokens;

namespace authentication.Services;

public class JwtService(IConfiguration configuration)
{
    public  TokenContainer CreateToken(User user)
    {
        //Generate Token Options
        var signInCredentials = GetSignInCredentials();
        var claims = GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signInCredentials,claims);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return new TokenContainer(
            AccessToken:accessToken
        );
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = configuration.GetRequiredSection("Authentication");
        return  new JwtSecurityToken(
            issuer:jwtSettings["ValidIssuer"],
            audience:jwtSettings["ValidAudience"],
            claims:claims,
            expires:DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["Expires"])),
            signingCredentials:signingCredentials
        );
    }


    private static List<Claim> GetClaims(User user)
    {
       var claims = new List<Claim>()
       {
           new(JwtRegisteredClaimNames.Name, user.Name!),
           new(JwtRegisteredClaimNames.Email, user.Email!),
           new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
       };
       return claims;
    }

    private SigningCredentials GetSignInCredentials()
    {
        var jwtSettings= configuration.GetRequiredSection("Authentication");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
        var secretKey = new SymmetricSecurityKey(key);
        return new SigningCredentials(key:secretKey,algorithm:SecurityAlgorithms.HmacSha256);
    }
}