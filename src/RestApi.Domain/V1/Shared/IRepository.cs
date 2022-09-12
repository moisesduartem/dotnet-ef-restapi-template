namespace RestApi.Domain.V1.Shared
{
    public interface IRepository<T> where T : IAggregateRoot
    {
    }
}
