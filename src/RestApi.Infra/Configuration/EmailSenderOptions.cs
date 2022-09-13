namespace RestApi.Infra.Configuration
{
    public class EmailSenderOptions
    {
        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public string FromEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string TemplatesDirectory { get; set; }
    }
}
