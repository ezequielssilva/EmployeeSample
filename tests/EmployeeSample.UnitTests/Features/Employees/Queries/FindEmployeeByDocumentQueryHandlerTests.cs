using EmployeeSample.Application.Features.Employees.Queries;
using EmployeeSample.Application.Features.Employees.Queries.FindEmployeeByDocument;
using EmployeeSample.Application.Interfaces.ReadRepositories;
using EmployeeSample.Domain.Entities;
using Moq;

namespace EmployeeSample.UnitTests.Features.Employees.Queries;

public class FindEmployeeByDocumentQueryHandlerTests
{
    private readonly Mock<IReadEmployeeRepository> _readRepositoryMock;
    private readonly FindEmployeeByDocumentQueryHandler _handler;

    public FindEmployeeByDocumentQueryHandlerTests()
    {
        _readRepositoryMock = new Mock<IReadEmployeeRepository>();
        IReadEmployeeRepository readRepository = _readRepositoryMock.Object;
        _handler = new FindEmployeeByDocumentQueryHandler(readRepository);
    }

    [Fact]
    public async void SuccessWhenReturnEmployDto()
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
        _readRepositoryMock.Setup(x => x.GetByDocument("32928190082")).Returns(Task.FromResult<Employee?>(employee));

        FindEmployeeByDocumentQuery query = new FindEmployeeByDocumentQuery
        {
            Document = employee.Document.ToString()
        };

        // Act
        var result = await _handler.Execute(query);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<EmployeeDto>(result);
    }
}
