namespace RestApi.Application.V1.Aggregates.Users.Commands
{
    public class ResetPasswordCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Token { get; set; }
    }
}
