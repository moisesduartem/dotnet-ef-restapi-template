namespace Moisesduartem.WebApiTemplate.Application.V1.Aggregates.Users.DTOs
{
    public class LoggedUserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Role { get; set; }
    }
}
