using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moisesduartem.WebApiTemplate.Application.V1.Options;
using Moisesduartem.WebApiTemplate.Application.V1.Services;
using Moisesduartem.WebApiTemplate.Domain.V1.Aggregates.Users.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Moisesduartem.WebApiTemplate.Infra.Services
{
    public class TokenGenerationService : ITokenGenerationService
    {
        private readonly string _secret;

        public TokenGenerationService(IOptions<AuthenticationOptions> options)
        {
            _secret = options.Value.Secret;
        }

        public string GenerateFor(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
