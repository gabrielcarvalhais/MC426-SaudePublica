using MC426_Backend.Infrastructure.Identity;
using MC426_Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<SaudePublicaContext>(opts =>
    opts.UseSqlServer("server=localhost\\SQLEXPRESS;database=SaudePublica;Trusted_Connection=True;TrustServerCertificate=true"));

builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<SaudePublicaContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Autenticacao/Login";
    options.Cookie.Name = "SaudePublicaAuthenticationCookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
    options.LoginPath = "/Autenticacao/Login";
    options.SlidingExpiration = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
