using UsuarioProject.Domain.Entities;

namespace UsuarioProject.Domain.Interfaces 
{ 
    public interface IUserDomain 
    {
        public Task<User?> GetByUserName(string userName);
        public Task Save(User user);
        public Task<bool> Update(User user);
    } 
} 
