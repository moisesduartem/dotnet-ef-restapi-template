namespace RestApi.Application.V1.Aggregates.Users.DTOs
{
    public class LoggedUserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
