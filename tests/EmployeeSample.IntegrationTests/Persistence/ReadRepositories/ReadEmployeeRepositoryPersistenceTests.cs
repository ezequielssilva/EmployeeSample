using System.Data;
using EmployeeSample.Application.Interfaces.Data;
using EmployeeSample.Application.Interfaces.ReadRepositories;
using EmployeeSample.Application.Interfaces.WriteRepositories;
using EmployeeSample.Domain.Entities;
using EmployeeSample.IntegrationTests.Persistence.Data;
using EmployeeSample.Persistence.ReadRepositories;
using EmployeeSample.Persistence.WriteRepositories;
using Xunit.Extensions.Ordering;

namespace EmployeeSample.IntegrationTests.Persistence.ReadRepositories;

[Order(3)]
public class ReadEmployeeRepositoryPersistenceTests
{
    private readonly IWriteEmployeeRepository _writeEmployeeRepository;
    private readonly IReadEmployeeRepository _readEmployeeRepository;

    public ReadEmployeeRepositoryPersistenceTests()
    {
        (IDbConnection connection, IPersistenceAdapter adapter) = SqlFactory.Create();
        _writeEmployeeRepository = new WriteEmployeeRepository(adapter);

        (IDbConnection connectionRead, IPersistenceAdapter adapterRead) = SqlFactory.Create();
        _readEmployeeRepository = new ReadEmployeeRepositoryPersistence(adapterRead);
    }

    [Fact]
    public async void SuccessWhenCheckDocumentExistsReturnTrue()
    {
        // Arange
        Employee employee = new Employee(
            document: "46884630221",
            fullName: "Test Employee Fullname",
            socialName: "Test Employee Socialname",
            sex: "M",
            maritalStatus: 1,
            educationLevel: 1,
            birthDate: "1990-06-30",
            phone: "0091122334455",
            email: "testemployee@email.com"
        );
        await _writeEmployeeRepository.Insert(employee);

        // Act
        bool result = await _readEmployeeRepository.CheckDocumentExists(employee.Document.ToString());

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async void SuccessWhenCheckDocumentExistsReturnFalse()
    {
        // Arange
        string document = "71169777163";

        // Act
        bool result = await _readEmployeeRepository.CheckDocumentExists(document);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async void SuccessWhenCheckDocumentExistsReturnFalseWhenEmployeeReadyExists()
    {
        // Arange
        Employee employee = new Employee(
            document: "67952748297",
            fullName: "Test Employee Fullname",
            socialName: "Test Employee Socialname",
            sex: "M",
            maritalStatus: 1,
            educationLevel: 1,
            birthDate: "1990-06-30",
            phone: "0091122334455",
            email: "testemployee@email.com"
        );
        await _writeEmployeeRepository.Insert(employee);

        // Act
        bool result = await _readEmployeeRepository.CheckDocumentExists(employee.Id.ToString(), employee.Document.ToString());

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async void SuccessWhenCheckDocumentExistsReturnTrueWhenEmployeeReadyExists()
    {
        // Arange
        Employee employee1 = new Employee(
            document: "25281852191",
            fullName: "Test Employee Fullname",
            socialName: "Test Employee Socialname",
            sex: "M",
            maritalStatus: 1,
            educationLevel: 1,
            birthDate: "1990-06-30",
            phone: "0091122334455",
            email: "testemployee@email.com"
        );

        Employee employee2 = new Employee(
            document: "65630971115",
            fullName: "Test Employee Fullname",
            socialName: "Test Employee Socialname",
            sex: "M",
            maritalStatus: 1,
            educationLevel: 1,
            birthDate: "1990-06-30",
            phone: "0091122334455",
            email: "testemployee@email.com"
        );

        _writeEmployeeRepository.BeginTransaction();
        await _writeEmployeeRepository.Insert(employee1);
        await _writeEmployeeRepository.Insert(employee2);
        _writeEmployeeRepository.CommitTransaction();

        // Act
        bool result = await _readEmployeeRepository.CheckDocumentExists(employee1.Id.ToString(), employee2.Document.ToString());

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async void SuccessWhenGetEmployeeByDocument()
    {
        // Arange
        Employee employee = new Employee(
            document: "52718835826",
            fullName: "Test Employee Fullname",
            socialName: "Test Employee Socialname",
            sex: "M",
            maritalStatus: 1,
            educationLevel: 1,
            birthDate: "1990-06-30",
            phone: "0091122334455",
            email: "testemployee@email.com"
        );
        await _writeEmployeeRepository.Insert(employee);

        // Act
        Employee? employeeByDocument = await _readEmployeeRepository.GetByDocument(employee.Document.ToString());

        // Assert
        Assert.NotNull(employeeByDocument);
        Assert.Equal(employee.Id.ToString(), employeeByDocument!.Id.ToString());
        Assert.Equal(employee.Document.ToString(), employeeByDocument!.Document.ToString());
    }

    [Fact]
    public async void SuccessWhenGetEmployeeById()
    {
        // Arange
        Employee employee = new Employee(
            document: "86736856485",
            fullName: "Test Employee Fullname",
            socialName: "Test Employee Socialname",
            sex: "M",
            maritalStatus: 1,
            educationLevel: 1,
            birthDate: "1990-06-30",
            phone: "0091122334455",
            email: "testemployee@email.com"
        );
        await _writeEmployeeRepository.Insert(employee);

        // Act
        Employee? employeeById = await _readEmployeeRepository.GetById(employee.Id.ToString());

        // Assert
        Assert.NotNull(employeeById);
        Assert.Equal(employee.Id.ToString(), employeeById!.Id.ToString());
        Assert.Equal(employee.Document.ToString(), employeeById!.Document.ToString());
    }
}
