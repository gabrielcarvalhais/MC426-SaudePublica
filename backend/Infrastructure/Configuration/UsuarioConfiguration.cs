using MC426_Backend.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MC426_Backend.Infrastructure.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            var usuario = new Usuario
            {
                Id = "6bc92124-2866-4d0b-82c3-e629139c2c03",
                Email = "admin@saudepublica.com",
                NormalizedEmail = "admin@saudepublica.com",
                EmailConfirmed = true,
                UserName = "admin@saudepublica.com",
                NormalizedUserName = "admin@saudepublica.com"
            };

            PasswordHasher<Usuario> ph = new PasswordHasher<Usuario>();
            usuario.PasswordHash = ph.HashPassword(usuario, "Admin@123");

            builder.HasData(usuario);
        }
    }
}
