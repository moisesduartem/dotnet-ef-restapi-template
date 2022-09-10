using Moisesduartem.WebApiTemplate.Domain.V1.Shared;
using Moisesduartem.WebApiTemplate.Domain.V1.Users.Enums;

namespace Moisesduartem.WebApiTemplate.Domain.V1.Users.Entities
{
    public class User : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public EAccountRole Role { get; private set; }
    }
}
