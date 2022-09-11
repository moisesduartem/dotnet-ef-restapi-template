using AutoMapper;
using FluentResults;
using MediatR;
using Moisesduartem.WebApiTemplate.Application.V1.Services;
using Moisesduartem.WebApiTemplate.Application.V1.Users.DTOs;
using Moisesduartem.WebApiTemplate.Application.V1.Users.Queries;
using Moisesduartem.WebApiTemplate.Domain.V1.Users.Entities;
using Moisesduartem.WebApiTemplate.Domain.V1.Users.Repositories;

namespace Moisesduartem.WebApiTemplate.Application.V1.Users.Handlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<LoginDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerationService _tokenGenerationService;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IUserRepository userRepository, ITokenGenerationService tokenGenerationService, IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenGenerationService = tokenGenerationService;
            _mapper = mapper;
        }

        public async Task<Result<LoginDTO>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user == null || user.PasswordHash != request.Password)
            {
                return Result.Fail<LoginDTO>(new Error("Invalid email and/or password"));
            }

            string token = _tokenGenerationService.GenerateFor(user);

            var loginDto = _mapper.Map<User, LoginDTO>(user);
            loginDto = _mapper.Map(token, loginDto);

            return Result.Ok(loginDto);
        }
    }
}
