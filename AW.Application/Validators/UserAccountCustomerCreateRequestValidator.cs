using FluentValidation;
using AW.Domain.Dto.Request.Create;

namespace AW.Application.Validators
{
    public class UserAccountCustomerCreateRequestValidator : AbstractValidator<UserAccountCustomerCreateRequestDto>
    {
        public UserAccountCustomerCreateRequestValidator()
        {
            RuleFor(x => x.UserName)
                .MaximumLength(10).WithMessage("El nombre de usuario debe tener 10 caracteres")
                .NotNull().WithMessage("El nombre de usuario no puede ser nulo")
                .NotEmpty().WithMessage("El nombre de usuario no puede estar vacío")
                .Must(LowerCase).WithMessage("El nombre de usuario debe estar en minúsculas");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("La contraseña es requerida")
                .NotEmpty().WithMessage("La contraseña no puede estar vacía");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("El correo es requerido")
                .NotEmpty().WithMessage("El correo no puede estar vacío")
                .EmailAddress().WithMessage("Debe ser un correo válido");
        }

        private bool LowerCase(string userName)
        {
            return userName == userName.ToLower();
        }
    }
}
