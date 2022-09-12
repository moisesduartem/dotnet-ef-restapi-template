using FluentValidation.Results;
using RestApi.Domain.V1.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApi.Domain.V1.Shared
{
    public abstract class Entity<T> where T : class
    {
        public Guid Id { get; init; }

        [NotMapped]
        public DomainValidator<T> Validator { get; init; }
        
        [NotMapped]
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
