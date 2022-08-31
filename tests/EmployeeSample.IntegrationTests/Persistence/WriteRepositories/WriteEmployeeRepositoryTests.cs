using System.Data;
using EmployeeSample.Application.Interfaces.Data;
using EmployeeSample.Application.Interfaces.ReadRepositories;
using EmployeeSample.Application.Interfaces.WriteRepositories;
using EmployeeSample.Domain.Entities;
using EmployeeSample.Domain.Enums;
using EmployeeSample.IntegrationTests.Persistence.Data;
using EmployeeSample.Persistence.ReadRepositories;
using EmployeeSample.Persistence.WriteRepositories;
using Xunit.Extensions.Ordering;

namespace EmployeeSample.IntegrationTests.Persistence.WriteRepositories;

[Order(2)]
public class WriteEmployeeRepositoryTests
{
    private readonly IWriteEmployeeRepository _writeEmployeeRepository;
    private readonly IReadEmployeeRepository _readEmployeeRepository;

    public WriteEmployeeRepositoryTests()
    {
        (IDbConnection connection, IPersistenceAdapter adapter) = SqlFactory.Create();
        _writeEmployeeRepository = new WriteEmployeeRepository(adapter);
        _readEmployeeRepository = new ReadEmployeeRepositoryPersistence(adapter);
    }

    [Fact, Order(1)]
    public async void SuccessWhenInsertEmployee()
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

        // Act
        await _writeEmployeeRepository.Insert(employee);

        Employee? employeeInserted = await _readEmployeeRepository.GetById(employee.Id.ToString());

        // Assert
        Assert.NotNull(employeeInserted);
        Assert.NotNull(employeeInserted?.Document);
        Assert.NotNull(employeeInserted?.Name);
        Assert.NotNull(employeeInserted?.BirthDate);
        Assert.NotNull(employeeInserted?.Phone);
        Assert.NotNull(employeeInserted?.Email);
        Assert.NotNull(employeeInserted?.CreatedAt);

        Assert.Equal(employee.Id.ToString(), employeeInserted?.Id.ToString());
        Assert.Equal(employee.Document.ToString(), employeeInserted?.Document.ToString());
        Assert.Equal(employee.Name.FullName, employeeInserted?.Name.FullName);
        Assert.Equal(employee.Name.SocialName, employeeInserted?.Name.SocialName);
        Assert.Equal(employee.Sex, employeeInserted?.Sex);
        Assert.Equal(employee.MaritalStatus, employeeInserted?.MaritalStatus);
        Assert.Equal(employee.EducationLevel, employeeInserted?.EducationLevel);
        Assert.Equal(employee.BirthDate.Date, employeeInserted?.BirthDate.Date);
        Assert.Equal(employee.Phone.ToString(), employeeInserted?.Phone.ToString());
        Assert.Equal(employee.Email.ToString(), employeeInserted?.Email.ToString());
    }

    [Fact, Order(2)]
    public async void SuccessWhenUpdateEmployee()
    {
        // Arrange
        var employee = await _readEmployeeRepository.GetByDocument("32928190082");

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
        employee!.ChangeEmployee(
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

        await _writeEmployeeRepository.Update(employee);

        Employee? employeeUpdated = await _readEmployeeRepository.GetById(employee.Id.ToString());

        // Assert
        Assert.NotNull(employeeUpdated);
        Assert.NotNull(employeeUpdated?.Document);
        Assert.NotNull(employeeUpdated?.Name);
        Assert.NotNull(employeeUpdated?.BirthDate);
        Assert.NotNull(employeeUpdated?.Phone);
        Assert.NotNull(employeeUpdated?.Email);
        Assert.NotNull(employeeUpdated?.UpdatedAt);

        Assert.Equal(employee.Id.ToString(), employeeUpdated?.Id.ToString());
        Assert.Equal(document, employeeUpdated?.Document.ToString());
        Assert.Equal(fullName, employeeUpdated?.Name.FullName);
        Assert.Equal(socialName, employeeUpdated?.Name.SocialName);
        Assert.Equal(Sex.Feminine, employeeUpdated?.Sex);
        Assert.Equal(MaritalStatus.Married, employeeUpdated?.MaritalStatus);
        Assert.Equal(EducationLevel.CompleteDoctorate, employeeUpdated?.EducationLevel);
        Assert.Equal(birthDate, employeeUpdated?.BirthDate.Date.ToString("yyyy-MM-dd"));
        Assert.Equal(phone, employeeUpdated?.Phone.ToString());
        Assert.Equal(email, employeeUpdated?.Email.ToString());
    }

    [Fact, Order(3)]
    public async void SuccessWhenDeleteEmployee()
    {
        // Arrange
        Employee? employee = await _readEmployeeRepository.GetByDocument("69082766434");

        // Act
        await _writeEmployeeRepository.Delete(employee!);

        var employeeDeleted = await _readEmployeeRepository.GetById(employee!.Id.ToString());

        // Assert
        Assert.NotNull(employee);
        Assert.Null(employeeDeleted);
    }
}
