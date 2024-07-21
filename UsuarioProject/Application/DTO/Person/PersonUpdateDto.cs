using UsuarioProject.Application.DTO.User;

namespace UsuarioProject.Application.DTO.Person
{
    public class PersonUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Email { get; set; }
        public UserUpdateDto DataUser { get; set; }
    }
}
