using RestApi.Domain.V1.Shared;

namespace RestApi.Domain.V1.Aggregates.Users.Entities
{
    public class User : Entity<User>, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; private set; }

        public User(string name, string email, string username, string passwordHash)
        {
            Name = name;
            Email = email;
            Username = username;
            PasswordHash = passwordHash;
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }

        public override void Validate()
        {
            ValidationResult = Validator.Validate(this);
        }
    }
}
