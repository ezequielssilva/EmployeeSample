using EmployeeSample.Domain.Entities;
using EmployeeSample.Domain.Enums;

namespace EmployeeSample.UnitTests.Entities;

public class EmployeeTests
{
    [Fact]
    public void SuccessWhenChangeEmployee()
    {
        // Arrange
        Employee employee = new Employee(
            document: "32928190082",
            fullName: "Test Employee Fullname",
            socialName: "Test Employee Socialname",
            sex: "M",
            maritalStatus: 1,
            educationLevel: 1,
            birthDate: "1990-06-30",
            phone: "0091122334455",
            email: "testemployee@email.com"
        );

        string document = "69082766434";
        string fullName = "Selma Dutra Eger";
        string socialName = "Selma Dutra Eger Social";
        string sex = ((char)Sex.Feminine).ToString();
        int? maritalStatus = (int)MaritalStatus.Married;
        int? educationLevel = (int)EducationLevel.CompleteDoctorate;
        string birthDate = "1975-12-19";
        string phone = "83968384043";
        string email = "selma.eger@geradornv.com.br";

        // Act
        employee.ChangeEmployee(
            document: document,
            fullName: fullName,
            socialName: socialName,
            sex: sex,
            maritalStatus: maritalStatus,
            educationLevel: educationLevel,
            birthDate: birthDate,
            phone: phone,
            email: email
        );

        // Assert
        Assert.NotNull(employee.UpdatedAt);
        Assert.NotNull(employee.Document);
        Assert.NotNull(employee.Name);
        Assert.Equal(document, employee.Document.ToString());
        Assert.Equal(fullName, employee.Name.FullName);
        Assert.Equal(socialName, employee.Name.SocialName);
        Assert.Equal(Sex.Feminine, employee.Sex);
        Assert.Equal(MaritalStatus.Married, employee.MaritalStatus);
        Assert.Equal(EducationLevel.CompleteDoctorate, employee.EducationLevel);
        Assert.Equal(birthDate, employee.BirthDate.ToString());
        Assert.Equal(phone, employee.Phone.ToString());
        Assert.Equal(email.ToLower(), employee.Email.ToString());
    }

    [Fact]
    public void ErrorWhenChangeEmployeeWithDataInvalid()
    {
        // Arrange
        Employee employee = new Employee(
            document: "32928190082",
            fullName: "Test Employee Fullname",
            socialName: "Test Employee Socialname",
            sex: "M",
            maritalStatus: 1,
            educationLevel: 1,
            birthDate: "1990-06-30",
            phone: "0091122334455",
            email: "testemployee@email.com"
        );

        string document = "69082766431";
        string fullName = "";
        string socialName = "";
        string sex = "";
        int? maritalStatus = null;
        int? educationLevel = null;
        string birthDate = "0001-01-01";
        string phone = "83968384";
        string email = "s.geradornv.com.br";

        // Act
        employee.ChangeEmployee(
            document: document,
            fullName: fullName,
            socialName: socialName,
            sex: sex,
            maritalStatus: maritalStatus,
            educationLevel: educationLevel,
            birthDate: birthDate,
            phone: phone,
            email: email
        );

        // Assert
        Assert.True(employee.HasNotification());
    }

    [Fact]
    public void ErrorWhenMaritalStatusInvalid()
    {
        // Arrange
        Employee employee = new Employee(
            document: "32928190082",
            fullName: "Test Employee Fullname",
            socialName: "Test Employee Socialname",
            sex: "M",
            maritalStatus: 6,
            educationLevel: 1,
            birthDate: "1990-06-30",
            phone: "0091122334455",
            email: "testemployee@email.com"
        );

        // Assert
        Assert.True(employee.HasNotification());
    }

    [Fact]
    public void SuccessWhenMaritalStatusValid()
    {
        // Arrange
        Employee employee = new Employee(
            document: "32928190082",
            fullName: "Test Employee Fullname",
            socialName: "Test Employee Socialname",
            sex: "M",
            maritalStatus: 1,
            educationLevel: 1,
            birthDate: "1990-06-30",
            phone: "0091122334455",
            email: "testemployee@email.com"
        );

        // Assert
        Assert.False(employee.HasNotification());
    }

    [Fact]
    public void ErrorWhenEducationLevelInvalid()
    {
        // Arrange
        Employee employee = new Employee(
            document: "32928190082",
            fullName: "Test Employee Fullname",
            socialName: "Test Employee Socialname",
            sex: "M",
            maritalStatus: 1,
            educationLevel: 10,
            birthDate: "1990-06-30",
            phone: "0091122334455",
            email: "testemployee@email.com"
        );

        // Assert
        Assert.True(employee.HasNotification());
    }

    [Fact]
    public void SuccessWhenEducationLevelValid()
    {
        // Arrange
        Employee employee = new Employee(
            document: "32928190082",
            fullName: "Test Employee Fullname",
            socialName: "Test Employee Socialname",
            sex: "M",
            maritalStatus: 1,
            educationLevel: 9,
            birthDate: "1990-06-30",
            phone: "0091122334455",
            email: "testemployee@email.com"
        );

        // Assert
        Assert.False(employee.HasNotification());
    }
}
