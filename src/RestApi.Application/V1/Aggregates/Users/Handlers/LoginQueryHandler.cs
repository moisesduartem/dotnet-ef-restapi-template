using MediatR;
using RestApi.Application.V1.Aggregates.Users.DTOs;
using RestApi.Application.V1.Aggregates.Users.Queries;

namespace RestApi.Application.V1.Aggregates.Users.Handlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginDTO>
    {
        public Task<LoginDTO> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
