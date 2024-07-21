using UsuarioProject.Application.DTO.Person;

namespace UsuarioProject.Application.Interfaces 
{ 
    public interface IPersonService 
    {    
        public Task<IEnumerable<PersonListDto?>> Get();
        public Task<bool> Save(PersonCreateDto person);
        public Task<bool> Update(PersonUpdateDto person);
        public Task<bool> Delete(int id);
    } 
}  
