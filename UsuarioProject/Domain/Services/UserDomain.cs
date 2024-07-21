using Microsoft.EntityFrameworkCore;
using UsuarioProject.Domain.Entities;
using UsuarioProject.Domain.Interfaces;
using UsuarioProject.Infrastructure.Persistence;
using BC = BCrypt.Net.BCrypt;

namespace UsuarioProject.Domain.Services 
{ 
   public class UserDomain: IUserDomain 
   { 
       private readonly DataContext _dataContext; 
       public UserDomain(DataContext dataContext) 
       {
            _dataContext = dataContext;  
       }

        public async Task<User?> GetByUserName(string userName)
        {
            return await _dataContext.User.AsNoTracking().Include(x => x.Person).FirstOrDefaultAsync(x => x.Username.Equals(userName));
        }

        public async Task Save(User user)
        {
            user.Password = BC.HashPassword(user.Password);
            Console.WriteLine($"Hashed Password: {user.Password}");
            Console.WriteLine($"Hashed Password Length: {user.Password.Length}");

            user.CreatedAt = DateTime.Now;
            await _dataContext.User.AddAsync(user);
        }

        public async Task<bool> Update(User user)
        {
            var ExistUser = await _dataContext.User.FindAsync(user.Id);
            if (ExistUser is null)
                return false;

            ExistUser.Username = user.Username;
            ExistUser.Password = BC.HashPassword(user.Password);

            return true;
        }
    } 
} 
