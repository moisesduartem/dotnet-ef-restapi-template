using Moisesduartem.WebApiTemplate.Domain.V1.Shared;
using Moisesduartem.WebApiTemplate.Domain.V1.Users.Enums;

namespace Moisesduartem.WebApiTemplate.Domain.V1.Users.Entities
{
    public class User : Entity<User>, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public EAccountRole Role { get; private set; }

        public User(string name, string email, string username, string passwordHash)
        {
            Name = name;
            Email = email;
            Username = username;
            PasswordHash = passwordHash;
            Role = EAccountRole.User;
        }

        public override void Validate()
        {
            ValidationResult = Validator.Validate(this);
        }
    }
}
