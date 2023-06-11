using MC426_Backend.Domain.Entities;
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
            var fileName = "appsettings.json";
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (!string.IsNullOrEmpty(envName))
            {
                fileName = $"appsettings.{envName}.json";
            }

            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                    .AddJsonFile(fileName, optional: false)
                    .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("sqlConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioRoleConfiguration());
            modelBuilder.ApplyConfiguration(new PacienteConfiguration());
            modelBuilder.ApplyConfiguration(new FuncionarioConfiguration());
            modelBuilder.ApplyConfiguration(new AgendamentoConfiguration());
        }

        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Agendamento> Agendamento { get; set; }
    }
}
