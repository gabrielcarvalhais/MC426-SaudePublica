using MC426_Backend.ApplicationService.Services;
using MC426_Backend.Domain.Interfaces.Repositories;
using MC426_Backend.Domain.Interfaces.Services;
using MC426_Backend.Infrastructure.Identity;
using MC426_Backend.Infrastructure.Repositories;
using MC426_Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<SaudePublicaContext>(opts =>
                opts.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<SaudePublicaContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});

//Precisa do package AutoMapper.Extensions.Microsoft.DependencyInjection
builder.Services.AddAutoMapper(typeof(Program));

// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

// Services
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();
builder.Services.AddScoped<IAgendamentoService, AgendamentoService>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Autenticacao/Login";
    options.Cookie.Name = "SaudePublicaAuthenticationCookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
    options.LoginPath = "/Autenticacao/Login";
    options.SlidingExpiration = true;
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(
                    options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
