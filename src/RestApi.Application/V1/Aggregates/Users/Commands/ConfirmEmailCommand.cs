namespace RestApi.Application.V1.Aggregates.Users.Commands
{
    public class ConfirmEmailCommand
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
