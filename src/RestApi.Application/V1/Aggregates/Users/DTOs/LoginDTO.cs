namespace RestApi.Application.V1.Aggregates.Users.DTOs
{
    public class LoginDTO
    {
        public LoggedUserDTO User { get; set; }
        public string Token { get; set; }
    }
}
