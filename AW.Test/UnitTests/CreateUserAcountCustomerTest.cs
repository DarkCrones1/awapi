using System.ComponentModel.DataAnnotations;
using AW.Application.Validators;
using AW.Domain.Dto.Request.Create;

namespace AW.Test.UnitTests;

[TestClass]
public class CreateUserAcountCustomerTest
{
    [TestMethod]
    public void CreateUserAccountToCustomerTesting_InvalidData()
    {
        var validator = new UserAccountCreateRequestCustomerValidator();
        var invalidData = new UserAccountCustomerCreateRequestDto
        {
            UserName = "ivanGarciaca",
            Password = "H0L4",
            Email = "ivangmail.com",
        };

        var validationResult = validator.Validate(invalidData);

        Assert.IsFalse(validationResult.IsValid);
        Assert.IsTrue(validationResult.Errors.Any());
    }

    [TestMethod]
    public void CreateUserAccountToCustomerTesting_ValidData()
    {
        // Preparación
        var validator = new UserAccountCreateRequestCustomerValidator();
        var validData = new UserAccountCustomerCreateRequestDto
        {
            UserName = "ivangarcia",
            Password = "H0L4",
            Email = "ivan@gmail.com",
        };

        // Ejecución
        var validationResult = validator.Validate(validData);

        // Verificación
        Assert.IsTrue(validationResult.IsValid);
        Assert.IsTrue(validationResult.Errors.Count == 0);
    }
}