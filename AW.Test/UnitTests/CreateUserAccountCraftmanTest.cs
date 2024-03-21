using System.ComponentModel.DataAnnotations;
using AW.Application.Validators;
using AW.Domain.Dto.Request.Create;

namespace AW.Test.UnitTests;

[TestClass]
public class CreateUserAccountCraftmanTest
{
    [TestMethod]
    public void CreateUserAccountToCraftmanTesting_InvalidData()
    {
        // Preparación
        var validator = new UserAccountCreateRequestCraftmanValidator();
        var invalidData = new UserAccountCraftmanCreateRequestDto
        {
            UserName = "ivanGarciaca",
            Password = "H0L4",
            Email = "ivangmail.com",
            FirstName = "ivanana",
            MiddleName = "Guadalupena",
            LastName = "Garcia Flores",
            CellPhone = "123",
            Phone = "SN",
            Gender = (short)1,
            BirthDate = DateTime.Parse("1996-10-14")
        };

        // Ejecución
        var validationResult = validator.Validate(invalidData);

        // Verificación
        Assert.IsFalse(validationResult.IsValid);
        Assert.IsTrue(validationResult.Errors.Count > 0);
    }

    [TestMethod]
    public void CreateUserAccountToCraftmanTesting_ValidData()
    {
        // Preparación
        var validator = new UserAccountCreateRequestCraftmanValidator();
        var validData = new UserAccountCraftmanCreateRequestDto
        {
            UserName = "ivangarcia",
            Password = "H0L4",
            Email = "ivan@gmail.com",
            FirstName = "Ivan",
            MiddleName = "Guadalupena",
            LastName = "Garcia Flores",
            CellPhone = "1234567890",
            Phone = "SN",
            Gender = (short)1,
            BirthDate = DateTime.Parse("1996-10-14")
        };

        // Ejecución
        var validationResult = validator.Validate(validData);

        // Verificación
        Assert.IsTrue(validationResult.IsValid);
        Assert.IsTrue(validationResult.Errors.Count == 0);
    }
}