using FluentValidation;
using InventAutoApi.DTO;

namespace UsuarioProject.Application.Validators
{
    public class LoginUserDtoValidator: AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage("Username is required.");
            RuleFor(x => x.UserName).NotNull().WithMessage("Password is required.");
        }
    }
}
