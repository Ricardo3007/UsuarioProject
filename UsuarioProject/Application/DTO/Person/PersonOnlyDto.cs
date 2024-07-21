using UsuarioProject.Application.DTO.User;
using System.Data;
using System.Text.Json.Serialization;

namespace UsuarioProject.Application.DTO.Person
{
    public class PersonOnlyDto: PersonCreateDto
    {
        [JsonIgnore]
        public new UserCreateDto DataUser { get; set; }
    }
}
