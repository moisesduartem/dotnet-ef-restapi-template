using RestApi.Application.V1.Shared;

namespace RestApi.Application.V1.Aggregates.Users.DTOs
{
    public class LoginDTO : Result
    {
        public LoggedUserDTO User { get; set; }
        public string Token { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
