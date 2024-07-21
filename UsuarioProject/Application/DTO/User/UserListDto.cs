using System.Text.Json.Serialization;

namespace UsuarioProject.Application.DTO.User
{
    public class UserListDto : UserUpdateDto
    {

        [JsonIgnore]
        public string Password { get; set; }
    }
}
