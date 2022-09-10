namespace Moisesduartem.WebApiTemplate.Application.V1.Users.DTOs
{
    public class LoginDTO
    {
        public LoggedUserDTO User { get; set; }
        public string token { get; set; }
    }
}
