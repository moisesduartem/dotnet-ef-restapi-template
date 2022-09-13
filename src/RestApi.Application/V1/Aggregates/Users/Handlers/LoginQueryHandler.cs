using AutoMapper;
using MediatR;
using RestApi.Application.V1.Aggregates.Users.DTOs;
using RestApi.Application.V1.Aggregates.Users.Queries;
using RestApi.Application.V1.Shared;
using RestApi.Domain.V1.Aggregates.Users.Entities;
using RestApi.Domain.V1.Aggregates.Users.Repositories;

namespace RestApi.Application.V1.Aggregates.Users.Handlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, IResult<LoginDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IResult<LoginDTO>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            
            if (user == null || user.PasswordHash != request.Password)
            {
                return Result.Create<LoginDTO>().Error<LoginDTO>("Invalid email and/or password");
            }

            //string token = _tokenGenerationService.GenerateFor(user);

            string token = "abc";

            var loginDto = _mapper.Map<User, LoginDTO>(user);
            loginDto = _mapper.Map(token, loginDto);

            return Result.Create<LoginDTO>().Ok(loginDto);
        }
    }
}
