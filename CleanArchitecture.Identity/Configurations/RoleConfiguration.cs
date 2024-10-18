using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            var hasher = new PasswordHasher<IdentityRole>();

            builder.HasData(

                    new IdentityRole
                    {
                      Id = "3d703444-d119-4d52-a232-bcd1efa5bf8e",
                      Name = "Administrator",
                      NormalizedName = "Administrator"
                    },
                     new IdentityRole
                     {
                         Id = "28c059ff-dfb1-4128-9799-a47e6cc3157b",
                         Name = "Operator",
                         NormalizedName = "Operator"
                     }

                );
        }
    }
}
