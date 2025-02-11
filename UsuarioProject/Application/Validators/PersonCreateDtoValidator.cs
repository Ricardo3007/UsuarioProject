﻿using FluentValidation;
using UsuarioProject.Application.DTO.Person;
using UsuarioProject.Application.DTO.User;

namespace UsuarioProject.Application.Validators
{
    public class PersonCreateDtoValidator : AbstractValidator<PersonCreateDto>
    {
        public PersonCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required.");
            RuleFor(x => x.DocumentType).NotEmpty().WithMessage("DocumentType is required.");
            RuleFor(x => x.DocumentNumber).NotEmpty().WithMessage("DocumentNumber is required.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");

            RuleFor(x => x.DataUser).SetValidator(new UserCreateDtoValidator());
        }
    }

    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
