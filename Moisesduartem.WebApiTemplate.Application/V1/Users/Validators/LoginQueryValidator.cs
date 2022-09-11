using FluentValidation;
using Moisesduartem.WebApiTemplate.Application.V1.Users.Queries;

namespace Moisesduartem.WebApiTemplate.Application.V1.Users.Validators
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
