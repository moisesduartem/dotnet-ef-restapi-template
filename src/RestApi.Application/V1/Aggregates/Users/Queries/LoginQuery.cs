using MediatR;
using RestApi.Application.V1.Aggregates.Users.DTOs;

namespace RestApi.Application.V1.Aggregates.Users.Queries
{
    public class LoginQuery : IRequest<LoginDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
