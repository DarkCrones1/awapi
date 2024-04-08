using FluentValidation;
using AW.Domain.Dto.Request.Create;

namespace AW.Application.Validators;
public class UserAccountCreateRequestCustomerValidator : AbstractValidator<UserAccountCustomerCreateRequestDto>
{
    public UserAccountCreateRequestCustomerValidator()
    {
        RuleFor(x => x.UserName).Length(10).NotNull().NotEmpty().Must(LowerCase).WithMessage("El nombre de usuario debe de ir en minúsculas");
        RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("La contraseña es requerido");
        RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("El correo es requerido");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Tiene que ser un correo válido");
        // RuleFor(x => x.CellPhone).Null().Empty();
        // RuleFor(x => x.CellPhone).Length(10).WithMessage("El número celular debe de tener 10 dígitos");
        // RuleFor(x => x.FirstName).Null().Empty();
        // RuleFor(x => x.LastName).Null().Empty();
    }

    private bool LowerCase(string UserName)
    {
        return UserName == UserName.ToLower();
    }
}