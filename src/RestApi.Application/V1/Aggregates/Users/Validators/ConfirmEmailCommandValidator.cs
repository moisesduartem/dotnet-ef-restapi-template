using FluentValidation;
using RestApi.Application.V1.Aggregates.Users.Commands;

namespace RestApi.Application.V1.Aggregates.Users.Validators
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.Token).NotEmpty();
        }
    }
}
