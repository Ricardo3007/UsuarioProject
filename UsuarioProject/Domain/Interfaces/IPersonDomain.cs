using UsuarioProject.Domain.Entities;

namespace UsuarioProject.Domain.Interfaces 
{ 
    public interface IPersonDomain 
    {
        public Task<List<Person>> Get();
        public Task Save(Person person);
        public Task<bool> Update(Person person);
        public Task<bool> Delete(int id);
    } 
} 
