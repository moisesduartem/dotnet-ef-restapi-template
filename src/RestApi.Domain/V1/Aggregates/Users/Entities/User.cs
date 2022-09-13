using RestApi.Domain.V1.Shared;

namespace RestApi.Domain.V1.Aggregates.Users.Entities
{
    public class User : Entity<User>, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public override void Validate()
        {
            ValidationResult = Validator.Validate(this);
        }
    }
}
