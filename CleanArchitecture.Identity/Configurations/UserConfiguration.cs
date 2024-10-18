using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationuUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationuUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationuUser>();

            builder.HasData(

                    new ApplicationuUser
                    {
                        Id = "d77b8013-598c-4261-9762-cf12329628e6",
                        Email = "admin@localhost.com",
                        NormalizedEmail = "admin@localhost.com",
                        Nombre = "Vaxi",
                        Apellidos = "Drez",
                        UserName = "vaxidrex",
                        NormalizedUserName = "vaxidrex",
                        PasswordHash = hasher.HashPassword(null, "vaxidrex2025$"),
                        EmailConfirmed = true
                    },
                     new ApplicationuUser
                     {
                         Id = "a7d60636-cc37-4a9d-a9c4-be56ac851df1",
                         Email = "juanperez@localhost.com",
                         NormalizedEmail = "juanperez@localhost.com",
                         Nombre = "Juan",
                         Apellidos = "Perez",
                         UserName = "juanperez",
                         NormalizedUserName = "juanperez",
                         PasswordHash = hasher.HashPassword(null, "vaxidrex2025$"),
                         EmailConfirmed = true
                     }

                );
        }
    }
}
