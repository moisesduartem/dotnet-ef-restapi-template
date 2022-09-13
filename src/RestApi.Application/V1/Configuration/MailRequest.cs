namespace RestApi.Application.V1.Configuration
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public string? TemplatePath { get; set; }
        public object? TemplateModel { get; set; }

        public bool HasRazorTemplate => TemplatePath != null;
    }
}
