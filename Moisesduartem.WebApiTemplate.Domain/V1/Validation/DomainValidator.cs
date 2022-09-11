using FluentValidation;

namespace Moisesduartem.WebApiTemplate.Domain.V1.Validation
{
    public class DomainValidator<T> : AbstractValidator<T> where T : class
    {
    }
}
