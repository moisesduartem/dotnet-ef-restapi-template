using FluentValidation;
using RestApi.Application.V1.Aggregates.Users.Commands;

namespace RestApi.Application.V1.Aggregates.Users.Validators
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
