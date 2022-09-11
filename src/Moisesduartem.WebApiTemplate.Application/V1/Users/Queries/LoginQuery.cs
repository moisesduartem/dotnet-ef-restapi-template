using MediatR;
using Moisesduartem.WebApiTemplate.Application.V1.Shared;
using Moisesduartem.WebApiTemplate.Application.V1.Users.DTOs;

namespace Moisesduartem.WebApiTemplate.Application.V1.Users.Queries
{
    public class LoginQuery : IRequest<Result<LoginDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
