using ErrorOr;
using FluentValidation;
using MediatR;
using Moisesduartem.WebApiTemplate.Application.V1.Shared;
using Moisesduartem.WebApiTemplate.Application.V1.Users.DTOs;

namespace Moisesduartem.WebApiTemplate.Application.V1.Users.Queries
{
    public class LoginQuery : 
        AbstractValidator<LoginQuery>, 
        IRequest<ErrorOr<LoginDTO>>, 
        IValidable<LoginQuery>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public void Validate()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
            
            Validate(this);
        }
    }
}
