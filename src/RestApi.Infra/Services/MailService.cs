using FluentEmail.Core;
using Microsoft.Extensions.Options;
using RestApi.Application.V1.Configuration;
using RestApi.Application.V1.Services;
using RestApi.Infra.Configuration;

namespace RestApi.Infra.Services
{
    public class MailService : IMailService
    {
        private readonly IFluentEmail _fluentEmail;
        private readonly EmailSenderOptions _options;

        public MailService(IFluentEmail fluentEmail, IOptions<EmailSenderOptions> options)
        {
            _fluentEmail = fluentEmail;
            _options = options.Value;
        }

        public Task SendAsync(MailRequest request, CancellationToken cancellationToken)
        {
            var email = _fluentEmail
                     .To(request.ToEmail)
                     .Subject(request.Subject)
                     .Body(request.Body);

            if (request.HasRazorTemplate)
            {
                var path = $"{Directory.GetCurrentDirectory()}/{_options.TemplatesDirectory}/{request.TemplatePath}";
                email.UsingTemplateFromFile(path, request.TemplateModel);
            }

            return email.SendAsync(cancellationToken);
        }
    }
}
