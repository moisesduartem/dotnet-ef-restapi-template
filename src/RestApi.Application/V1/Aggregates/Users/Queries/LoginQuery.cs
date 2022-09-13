namespace RestApi.Application.V1.Aggregates.Users.Queries
{
    public class LoginQuery
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
