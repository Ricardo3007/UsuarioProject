using UsuarioProject.Application.DTO.Person;

namespace InventAutoApi.DTO
{
    public class ResultLoginDto
    {
        public PersonOnlyDto Person { get; set; }
        public string Token { get; set; } = null!;
    }
}
