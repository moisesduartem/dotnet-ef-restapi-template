using FluentValidation;
using RestApi.Application.V1.Aggregates.Users.Commands;

namespace RestApi.Application.V1.Aggregates.Users.Validators
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.PasswordConfirmation).Equal(x => x.Password);
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
