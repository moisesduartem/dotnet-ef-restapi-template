using FluentResults;
using MediatR;
using Moisesduartem.WebApiTemplate.Application.V1.Users.DTOs;
using Moisesduartem.WebApiTemplate.Application.V1.Users.Queries;

namespace Moisesduartem.WebApiTemplate.Application.V1.Users.Handlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<LoginDTO>>
    {
        public Task<Result<LoginDTO>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
