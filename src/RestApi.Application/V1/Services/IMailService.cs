using RestApi.Application.V1.Configuration;

namespace RestApi.Application.V1.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request, CancellationToken cancellationToken);
    }
}
