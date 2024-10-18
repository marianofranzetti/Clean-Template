using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Identity.Models
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(

                    new IdentityUserRole<string>
                    {
                        RoleId = "3d703444-d119-4d52-a232-bcd1efa5bf8e",
                        UserId = "d77b8013-598c-4261-9762-cf12329628e6"
                    },
                      new IdentityUserRole<string>
                      {
                          RoleId = "28c059ff-dfb1-4128-9799-a47e6cc3157b",
                          UserId = "a7d60636-cc37-4a9d-a9c4-be56ac851df1"
                      }
                );
        }
    }
}
