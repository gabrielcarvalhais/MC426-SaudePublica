using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MC426_Backend.Infrastructure.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "b4344bed-9d81-4c83-8fb5-b4653894ff90",
                    Name = "Administrador",
                    NormalizedName = "Administrador"
                },
                new IdentityRole
                {
                    Id = "d02b1923-6c99-4fe1-a1fa-4477ae031242",
                    Name = "Paciente",
                    NormalizedName = "Paciente"
                },
                new IdentityRole
                {
                    Id = "93c1d0a4-2a15-4b80-b062-be9bac57c979",
                    Name = "Funcionário",
                    NormalizedName = "Funcionário"
                }
            );
        }
    }
}
