using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RestApi.Application.Models;
using RestApi.Application.V1.Aggregates.Users.Commands;
using RestApi.Application.V1.Aggregates.Users.DTOs;
using RestApi.Application.V1.Aggregates.Users.Queries;
using RestApi.Application.V1.Configuration;
using RestApi.Application.V1.Services;
using RestApi.Application.V1.Shared;
using RestApi.Identity.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestApi.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<RestApiUser> _signInManager;
        private readonly UserManager<RestApiUser> _userManager;
        private readonly JwtOptions _jwtOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMailService _mailService;

        public IdentityService(SignInManager<RestApiUser> signInManager, UserManager<RestApiUser> userManager, IOptions<JwtOptions> jwtOptions, IHttpContextAccessor httpContextAccessor, IMailService mailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
            _httpContextAccessor = httpContextAccessor;
            _mailService = mailService;
        }

        private async Task SendVerificationEmailAsync(RestApiUser user, CancellationToken cancellationToken)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var mailRequest = new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Confirm your email",
                Body = $"Your confirmation token is: {token}"
            };

            await _mailService.SendAsync(mailRequest, cancellationToken);
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

        public async Task<Result> RegisterAsync(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var identityUser = new RestApiUser
            {
                UserName = command.Email,
                Email = command.Email,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(identityUser, command.Password);

            if (result.Succeeded)
            {
                await SendVerificationEmailAsync(identityUser, cancellationToken);

                await _userManager.SetLockoutEnabledAsync(identityUser, false);

                return Result.Create();
            }

            return Result.Create().Error(result.Errors.Select(x => x.Description));
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

        private async Task<IList<Claim>> GetClaimsAsync(RestApiUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
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

        public Task<RestApiUser> FindUserByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public async Task<Result> ConfirmEmailAsync(RestApiUser user, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return Result.Create();
            }

            return Result.Create().Error(result.Errors.Select(x => x.Description));
        }

        public async Task<Result> ForgotPasswordAsync(RestApiUser user, CancellationToken cancellationToken)
        {
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var mailRequest = new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Reset Password",
                TemplatePath = "ResetPassword.cshtml",
                TemplateModel = new
                {
                    Token = token
                }
            };

            await _mailService.SendAsync(mailRequest, cancellationToken);

            return Result.Create();
        }

        public async Task<Result> ResetPasswordAsync(RestApiUser user, string token, string password)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, password);

            if (result.Succeeded)
            {
                return Result.Create();
            }

            return Result.Create().Error(result.Errors.Select(x => x.Description));
        }
    }
}
