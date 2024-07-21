using InventAutoApi.DTO;

namespace UsuarioProject.Application.Interfaces 
{ 
    public interface IAuthService 
    {
        public Task<ResultLoginDto?> Login(LoginUserDto loginUser);
    } 
}  
