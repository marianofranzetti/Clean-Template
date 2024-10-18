
namespace CleanArchitecture.Application.Models.Identity
{
    public class RegistrationRequest
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }

    }
}
