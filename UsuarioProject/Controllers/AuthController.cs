using InventAutoApi.DTO;
using Microsoft.AspNetCore.Mvc;
using UsuarioProject.Application.Interfaces;

namespace UsuarioProject.Controllers 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class AuthController : ControllerBase 
    { 
        private readonly IAuthService _authService; 
        public AuthController(IAuthService authService) 
        { 
            _authService = authService; 
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ResultLoginDto>> Login([FromBody] LoginUserDto loginUser)
        {
            var result = await _authService.Login(loginUser);
            if (result is null)
                return Unauthorized("Usuario o contraseña incorrecto.");

            return Ok(result);
        }
    } 
} 
