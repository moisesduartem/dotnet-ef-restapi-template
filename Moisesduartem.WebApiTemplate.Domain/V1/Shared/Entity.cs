namespace Moisesduartem.WebApiTemplate.Domain.V1.Shared
{
    public class Entity
    {
        public Guid Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
