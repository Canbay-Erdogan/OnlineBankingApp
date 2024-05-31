using Microsoft.IdentityModel.Tokens;
using OnlineBankingApp.Entities.Concrete;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace OnlineBankingApp.WebAPI.Security
{
    public static class TokenHandler
    {
        public static Token CreateToken(IConfiguration configuration)
        {
            Token token = new();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            token.Expires = DateTime.Now.AddMinutes(Convert.ToInt16(configuration["Jwt:Expires"]));
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                expires: token.Expires,
                notBefore: DateTime.Now,
                signingCredentials: creds
                );
            JwtSecurityTokenHandler handler = new();
            token.AccessToken = handler.WriteToken(jwtSecurityToken);

            byte[] numbers = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(numbers);
            token.RefreshToken=Convert.ToBase64String(numbers);

            return token;
        }
    }
}
