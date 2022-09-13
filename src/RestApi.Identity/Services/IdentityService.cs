using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RestApi.Application.V1.Aggregates.Users.Commands;
using RestApi.Application.V1.Aggregates.Users.DTOs;
using RestApi.Application.V1.Aggregates.Users.Queries;
using RestApi.Application.V1.Services;
using RestApi.Application.V1.Shared;
using RestApi.Identity.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestApi.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<JwtOptions> jwtOptions, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoggedUserDTO?> GetLoggedUserAsync()
        {
            if (_httpContextAccessor.HttpContext is not null)
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);
                
                return new LoggedUserDTO
                {
                    Id = user.Id,
                    Email = user.Email
                };
            }

            return null;
        }

        public async Task<LoginDTO> LoginAsync(LoginQuery query)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(query.Email, query.Password, false, true);

            if (signInResult.Succeeded)
            {
                return await GenerateJsonWebTokenAsync(query.Email);
            }

            var result = new LoginDTO();

            if (signInResult.IsLockedOut)
                result.AddError("This account is blocked");

            else if (signInResult.IsNotAllowed)
                result.AddError("This account is not allow to login");
            
            else if (signInResult.RequiresTwoFactor)
                result.AddError("It is necessary to confirm the login at your second device");
            
            else
                result.AddError("Invalid credentials");

            return result;
        }

        public async Task<Result> RegisterAsync(RegisterUserCommand command)
        {
            var identityUser = new IdentityUser
            {
                UserName = command.Email,
                Email = command.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(identityUser, command.Password);

            if (result.Succeeded)
            {
                await _userManager.SetLockoutEnabledAsync(identityUser, false);
                return Result.Create();
            }

            IList<string> errors = result.Errors.Select(x => x.Description).ToList();

            return Result.Create().Error(errors);
        }

        private async Task<LoginDTO> GenerateJsonWebTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var claims = await GetClaimsAsync(user);

            var expirationDate = DateTime.Now.AddSeconds(_jwtOptions.ExpirationInSeconds);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expirationDate,
                signingCredentials: _jwtOptions.SigningCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new LoginDTO
            {
                User = new LoggedUserDTO { Id = user.Id, Email = user.Email },
                Token = token,
                ExpirationDate = expirationDate,
            };
        }

        private async Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            return claims;
        }
    }
}
