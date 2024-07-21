using AutoMapper;
using UsuarioProject.Application.DTO.Person;
using UsuarioProject.Application.DTO.User;
using UsuarioProject.Domain.Entities;

namespace UsuarioProject.Application.Mapper
{
    public class GlobalMapperProfile: Profile
    {
        public GlobalMapperProfile()
        {
            CreateMap<PersonCreateDto, Person>();
            CreateMap<UserCreateDto, User>();

            CreateMap<PersonUpdateDto, Person>();
            CreateMap<UserUpdateDto, User>();

            CreateMap<Person, PersonOnlyDto>();

            CreateMap<Person, PersonListDto>()
                .ForMember(dest => dest.DataUser, opt => opt.MapFrom(src => new UserListDto
                {
                    Id = src.Users.FirstOrDefault().Id,
                    Username = src.Users.FirstOrDefault().Username,
                    Password = src.Users.FirstOrDefault().Password,
                }));

        }
    }
}
