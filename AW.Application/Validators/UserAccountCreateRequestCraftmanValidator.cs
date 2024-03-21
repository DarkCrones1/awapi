using FluentValidation;
using AW.Domain.Dto.Request.Create;

namespace AW.Application.Validators;
public class UserAccountCreateRequestCraftmanValidator : AbstractValidator<UserAccountCraftmanCreateRequestDto>
{
    public UserAccountCreateRequestCraftmanValidator()
    {
        RuleFor(x => x.UserName).Length(10).NotNull().NotEmpty().Must(LowerCase).WithMessage("El nombre de usuario debe de ir en minúsculas");
        RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("La contraseña es requerido");
        RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("El correo es requerido");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Tiene que ser un correo válido");
        RuleFor(x => x.CellPhone).NotNull().NotEmpty();
        RuleFor(x => x.CellPhone).Length(10).WithMessage("El número celular debe de tener 10 dígitos");
        RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("El primer nombre es requerido");
        RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Los apellidos son requeridos");
    }

    private bool LowerCase(string UserName)
    {
        return UserName == UserName.ToLower();
    }
}