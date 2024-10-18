using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Identity.Models
{
    public class ApplicationuUser : IdentityUser
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos {  get; set; } = string.Empty;
    }
}
