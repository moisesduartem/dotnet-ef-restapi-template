using FluentValidation;
using FluentValidation.Results;
using Moisesduartem.WebApiTemplate.Domain.V1.Validation;

namespace Moisesduartem.WebApiTemplate.Domain.V1.Shared
{
    public abstract class Entity<T> where T : class
    {
        public Guid Id { get; init; }
        public DomainValidator<T> Validator { get; init; }
        public ValidationResult ValidationResult { get; protected set; }
        
        protected Entity()
        {
            Id = Guid.NewGuid();
            Validator = new DomainValidator<T>();
            ValidationResult = new ValidationResult();
        }

        public virtual bool IsValid()
        {
            return ValidationResult.IsValid;
        }

        public abstract void Validate();
    }
}
