using FluentValidation;
using RestApi.Application.V1.Aggregates.Users.Queries;

namespace RestApi.Application.V1.Aggregates.Users.Validators
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
