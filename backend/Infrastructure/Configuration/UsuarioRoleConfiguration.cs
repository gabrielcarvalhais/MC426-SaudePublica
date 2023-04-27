using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MC426_Backend.Infrastructure.Configuration
{
    public class UsuarioRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasKey(e => new { e.UserId, e.RoleId });

            builder.HasData(
                    new IdentityUserRole<string>
                    {
                        UserId = "6bc92124-2866-4d0b-82c3-e629139c2c03",
                        RoleId = "b4344bed-9d81-4c83-8fb5-b4653894ff90"
                    }
                );
        }
    }
}
