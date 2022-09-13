using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestApi.Application.V1.Services;
using RestApi.Infra.Configuration;
using RestApi.Infra.Services;
using System.Net;
using System.Net.Mail;

namespace RestApi.Infra.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEmailSenderConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("EmailSender");

            var emailSenderOptions = section.Get<EmailSenderOptions>();

            services.AddFluentEmail(emailSenderOptions.FromEmail)
                            .AddRazorRenderer()
                            .AddSmtpSender(new SmtpClient(emailSenderOptions.SMTPHost, emailSenderOptions.SMTPPort)
                            {
                                Credentials = new NetworkCredential(
                                    userName: emailSenderOptions.Username,
                                    password: emailSenderOptions.Password),
                                EnableSsl = true
                            });

            services.Configure<EmailSenderOptions>(section);

            services.AddScoped<IMailService, MailService>();

            return services;
        }
    }
}
