using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using UsuarioProject.Application.Interfaces;
using UsuarioProject.Application.Mapper;
using UsuarioProject.Application.Services;
using UsuarioProject.Domain.Interfaces;
using UsuarioProject.Domain.Services;
using UsuarioProject.Infrastructure.Persistence;
using System.Reflection;
using UsuarioProject.DTo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var allOrigin = "AllOrigin";

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(allOrigin,
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(GlobalMapperProfile));

builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = true;
}).AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddTransient<IPersonService, PersonService>();
builder.Services.AddTransient<IPersonDomain, PersonDomain>();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserDomain, UserDomain>();



var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Load JWT settings from configuration
var jwtSettings = config.GetSection("Jwt").Get<JwtSettings>();
builder.Services.AddSingleton(jwtSettings);


// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        //ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        //ValidIssuer = jwtSettings.Issuer,
        //ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});

// Add authorization
builder.Services.AddAuthorization();




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(allOrigin);

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
