using AssessementProjectForAddingUser.Domain.DTOs;
using AssessementProjectForAddingUser.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace AssessementProjectForAddingUser.Infrastructure.CustomLogic
{
    public class TokenGenerationService
    {
        private readonly IConfiguration _config;

        public TokenGenerationService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string GenerateToken(UserDetailsAnkit userDetail)
        {
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
            //    expires: DateTime.Now.AddMinutes(5),
            //    signingCredentials: credentials);
            //return new JwtSecurityTokenHandler().WriteToken(token);

            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                    //new Claim("Id", user.Id.ToString()),
                    new Claim("Id", userDetail.UserId.ToString()),
                    new Claim("Email", userDetail.Email.ToString())
            };

            //Converting minutes into int because configuration will always return string
            var expiryMinutes = int.Parse(_config["Jwt:ExpiryMinutes"]);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: signIn

            );

            //Write token used for serialize a JwtSecurityToken into a JWT using the compact serialization format
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
