using Microsoft.EntityFrameworkCore;
using UsuarioProject.Domain.Entities;
using UsuarioProject.Domain.Interfaces;
using UsuarioProject.Infrastructure.Persistence;

namespace UsuarioProject.Domain.Services
{
    public class PersonDomain : IPersonDomain
    {
        private readonly DataContext _dbContext;
        public PersonDomain(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Person>> Get()
        {
            return await _dbContext.Person.AsNoTracking().Include(x => x.Users).Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task Save(Person person)
        {
            person.CreatedAt = DateTime.Now;
            person.IsDeleted = false;
            await _dbContext.Person.AddAsync(person);
        }

        public async Task<bool> Update(Person person)
        {
            var ExistPerson = await _dbContext.Person.FirstOrDefaultAsync(x => x.Id.Equals(person.Id));
            if (ExistPerson is null)
                return false;

            person.CreatedAt = ExistPerson.CreatedAt;
            person.UpdatedAt = DateTime.Now;
            person.IsDeleted = ExistPerson.IsDeleted;

            _dbContext.Entry(ExistPerson).CurrentValues.SetValues(person);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var ExistPerson = await _dbContext.Person.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (ExistPerson is null)
                return false;

            ExistPerson.DeletedAt = DateTime.Now;
            ExistPerson.IsDeleted = true;

            return true;
        }
    }
} 
