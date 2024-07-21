using AutoMapper;
using InventAutoApi.DTO;
using Microsoft.IdentityModel.Tokens;
using UsuarioProject.Application.DTO.Person;
using UsuarioProject.Application.Exceptions;
using UsuarioProject.Application.Interfaces;
using UsuarioProject.Domain.Entities;
using UsuarioProject.Domain.Interfaces;
using UsuarioProject.Infrastructure.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Configuration;
using UsuarioProject.DTo;

namespace UsuarioProject.Application.Services 
{ 
    public class AuthService: IAuthService 
    {
        private readonly IConfiguration _config;
        private readonly IUserDomain _userDomain; 
        private readonly IMapper _mapper; 
        private readonly DataContext _dataContext; 
        public AuthService(IUserDomain userDomain, IMapper mapper, DataContext dataContext, IConfiguration config) 
        {
            _userDomain = userDomain; 
            _mapper = mapper; 
            _dataContext = dataContext;
            _config = config;
        }

        public async Task<ResultLoginDto?> Login(LoginUserDto loginUser)
        {
            try
            {
                User? userData = await _userDomain.GetByUserName(loginUser.UserName);
                if(userData is null)               
                    return null;
                
                if (!BC.Verify(loginUser.Password, userData.Password))
                    return null;

                var token = GenerateJSONWebToken();

                var userLoginResult = new ResultLoginDto()
                {
                    Person = _mapper.Map<PersonOnlyDto>(userData.Person),
                    Token = token
                };

                return userLoginResult;
            }
            catch (Exception ex)
            {
                throw new CustomException(ErrorMessages.GenericError, ex);
            }
        }

        private string GenerateJSONWebToken()
        {

            var jwtSettings = _config.GetSection("Jwt").Get<JwtSettings>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "ADMIN"),
            };

            var token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenf = tokenHandler.CreateToken(token);

            return tokenHandler.WriteToken(tokenf);
            //var key = Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new CustomException("JWT key is not configured.");
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var claims = new List<Claim>
            //{
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
            //    new Claim(ClaimTypes.Role, "ADMIN"),
            //};

            //var token = new JwtSecurityToken(
            //    issuer: _config["Jwt:Issuer"],
            //    audience: _config["Jwt:Audience"],
            //    claims: claims,
            //    expires: DateTime.UtcNow.AddDays(1),
            //    signingCredentials: credentials);

            //var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);

            //return encodetoken;
        }
    } 
} 
