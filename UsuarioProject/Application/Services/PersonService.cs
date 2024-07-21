using AutoMapper;
using UsuarioProject.Application.DTO.Person;
using UsuarioProject.Application.Exceptions;
using UsuarioProject.Application.Interfaces;
using UsuarioProject.Domain.Entities;
using UsuarioProject.Domain.Interfaces;
using UsuarioProject.Domain.Services;
using UsuarioProject.Infrastructure.Persistence;

namespace UsuarioProject.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonDomain _personDomain;
        private readonly IUserDomain _userDomain;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public PersonService(IPersonDomain personDomain, IUserDomain userDomain, IMapper mapper, DataContext dataContext)
        {
            _personDomain = personDomain;
            _userDomain = userDomain;
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<PersonListDto>?> Get()
        {
            try
            {
                return _mapper.Map<IEnumerable<PersonListDto>>(await _personDomain.Get());
            }
            catch (Exception ex)
            {
                throw new CustomException(ErrorMessages.GenericError, ex);
            }
        }

        public async Task<bool> Save(PersonCreateDto person)
        {
            try
            {
                var personSave = _mapper.Map<Person>(person);
                await _personDomain.Save(personSave);
                await _dataContext.SaveChangesAsync();

                User userSave = _mapper.Map<User>(person.DataUser);
                userSave.PersonId = personSave.Id;

                await _userDomain.Save(userSave);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new CustomException(ErrorMessages.GenericError, ex);
            }
        }

        public async Task<bool> Update(PersonUpdateDto person)
        {
            try
            {
                await _personDomain.Update(_mapper.Map<Person>(person));
                await _dataContext.SaveChangesAsync();

                User userSave = _mapper.Map<User>(person.DataUser);
                userSave.PersonId = person.Id;

                await _userDomain.Update(userSave);                
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new CustomException(ErrorMessages.GenericError, ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var deleted = await _personDomain.Delete(id);
                if (deleted)
                    await _dataContext.SaveChangesAsync();

                return deleted;
            }
            catch (Exception ex)
            {
                throw new CustomException(ErrorMessages.GenericError, ex);
            }
        }
    }
}
