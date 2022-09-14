using RestApi.Domain.V1.Shared;

namespace RestApi.Domain.V1.Aggregates.Users.Entities
{
    public class User : Entity<User>, IAggregateRoot
    {
        public string Email { get; private set; }

        public User(string email)
        {
            Email = email;
        }

        public override void Validate()
        {
            ValidationResult = Validator.Validate(this);
        }
    }
}
