using AutoMapper;
using MediatR;
using RestApi.Application.V1.Aggregates.Users.DTOs;
using RestApi.Application.V1.Aggregates.Users.Queries;
using RestApi.Application.V1.Services;
using RestApi.Application.V1.Shared;
using RestApi.Domain.V1.Aggregates.Users.Entities;
using RestApi.Domain.V1.Aggregates.Users.Repositories;

namespace RestApi.Application.V1.Aggregates.Users.Handlers
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
                return Result<LoginDTO>.Create().Error("Login", "Invalid email and/or password");
            }

            string token = _tokenGenerationService.GenerateFor(user);

            var loginDto = _mapper.Map<User, LoginDTO>(user);
            loginDto = _mapper.Map(token, loginDto);

            return Result<LoginDTO>.Create().Ok(loginDto);
        }
    }
}
