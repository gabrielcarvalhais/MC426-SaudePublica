using MC426_Backend.Infrastructure.Configuration;
using MC426_Backend.Infrastructure.Identity;
using MC426_Domain.Entities;
using MC426_Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC426_Infrastructure.Data
{
    public class SaudePublicaContext : IdentityDbContext<Usuario>
    {
        public SaudePublicaContext()
        {
            //construtor vazio requerido para rodar as migrations neste projeto
        }

        public SaudePublicaContext(DbContextOptions<SaudePublicaContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //gabs
            optionsBuilder.UseSqlServer("server=localhost\\SQLEXPRESS;database=SaudePublica;Trusted_Connection=True;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioRoleConfiguration());
            modelBuilder.ApplyConfiguration(new PacienteConfiguration());
        }

        public DbSet<Paciente> Pacientes { get; set; }
    }
}
