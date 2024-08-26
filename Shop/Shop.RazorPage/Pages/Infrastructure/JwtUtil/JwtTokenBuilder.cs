using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Shop.Query.Users.DTOs;

namespace Shop.RazorPage.Pages.Infrastructure.JwtUtil;

public class JwtTokenBuilder
{
    public static string BuildToken(UserDto user, IConfiguration configuration)
    {

        //var roles = user.UserRoles.Select(f => f.RoleTitle);
        var roles = user.RoleTitls;

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Role,string.Join("-",roles))
        };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SignInKey"]));
        var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtConfig:Issuer"],
            audience: configuration["JwtConfig:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credential);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}