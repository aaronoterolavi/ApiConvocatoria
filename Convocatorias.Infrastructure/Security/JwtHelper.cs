using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Convocatorias.Infrastructure.Security
{
    //public class JwtHelper
    //{
    //    private readonly IConfiguration _configuration;

    //    public JwtHelper(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }

    //    public string GenerateToken(int userId, int roleId, string email)
    //    {
    //        var jwtSettings = _configuration.GetSection("Jwt");

    //        var claims = new[]
    //        {
    //            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
    //            new Claim("role", roleId.ToString()),
    //            new Claim(JwtRegisteredClaimNames.Email, email),
    //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    //        };

    //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
    //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //        var token = new JwtSecurityToken(
    //            issuer: jwtSettings["Issuer"],
    //            audience: jwtSettings["Audience"],
    //            claims: claims,
    //            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
    //            signingCredentials: creds
    //        );

    //        return new JwtSecurityTokenHandler().WriteToken(token);
    //    }
    //}
}
