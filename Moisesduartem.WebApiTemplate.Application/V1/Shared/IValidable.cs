namespace Moisesduartem.WebApiTemplate.Application.V1.Shared
{
    public interface IValidable<T> where T : class
    {
        void Validate();
    }
}
