using EmployeeSample.Application.Features.Employees.Commands.UpdateEmployee;
using EmployeeSample.Application.Interfaces.Commands;
using EmployeeSample.Application.Interfaces.ReadRepositories;
using EmployeeSample.Application.Interfaces.WriteRepositories;
using EmployeeSample.Domain.Entities;
using EmployeeSample.Domain.Exceptions;
using Moq;

namespace EmployeeSample.UnitTests.Features.Employees.Commands;

public class UpdateEmployeeCommandHandlerTests
{
    private readonly Mock<IWriteEmployeeRepository> _writeRepositoryMock;
    private readonly Mock<IReadEmployeeRepository> _readRepositoryMock;
    private readonly ICommandHandler<UpdateEmployeeCommand, UpdateEmployeeCommandResult> _handler;

    public UpdateEmployeeCommandHandlerTests()
    {
        _writeRepositoryMock = new Mock<IWriteEmployeeRepository>();
        _readRepositoryMock = new Mock<IReadEmployeeRepository>();
        IWriteEmployeeRepository writeRepository = _writeRepositoryMock.Object;
        IReadEmployeeRepository readRepository = _readRepositoryMock.Object;
        _handler = new UpdateEmployeeCommandHandler(writeRepository, readRepository);
    }

    [Fact]
    public async void SuccessWhenUpdateEmployeeCommandIsValid()
    {
        // Arrange
        Employee employeeExpected = new Employee(
            document: "32928190082",
            fullName: "Test Employee Fullname",
            socialName: "Test Employee Socialname",
            sex: "M",
            maritalStatus: 2,
            educationLevel: 1,
            birthDate: "1990-06-30",
            phone: "0091122334455",
            email: "testemployee@email.com"
        );
        _readRepositoryMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(Task.FromResult<Employee?>(employeeExpected));

        UpdateEmployeeCommand command = new UpdateEmployeeCommand
        {
            Id = employeeExpected.Id.ToString(),
            Document = "32928190082",
            FullName = "Test Employee Fullname Update",
            BirthDate = "1995-12-10",
            EducationLevel = 3,
            MaritalStatus = 3,
            Sex = "F",
            Email = "testemployeeupdate@email.com",
            Phone = "1191176334875"
        };
        
        // Act
        var result = await _handler.Handle(command);

        // Assert
        _readRepositoryMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        _writeRepositoryMock.Verify(x => x.Update(It.IsAny<Employee>()), Times.Once);

        Assert.NotNull(result);
        Assert.Equal(employeeExpected.Id.ToString(), result.Id);
    }

    [Fact]
    public async void ErrorWhenUpdateEmployeeWithDocumentReadyExists_ThrowApplicationValidationException()
    {
        // Arrange
        Employee employeeExpected = new Employee(
            document: "32928190082",
            fullName: "Test Employee Fullname",
            socialName: "Test Employee Socialname",
            sex: "M",
            maritalStatus: 2,
            educationLevel: 1,
            birthDate: "1990-06-30",
            phone: "0091122334455",
            email: "testemployee@email.com"
        );

        UpdateEmployeeCommand command = new UpdateEmployeeCommand
        {
            Id = employeeExpected.Id.ToString(),
            Document = "32928190082",
            FullName = "Test Employee Fullname",
            BirthDate = "1990-06-30",
            EducationLevel = 0,
            MaritalStatus = 1,
            Sex = "M",
            Email = "testemployee@email.com",
            Phone = "0091122334455"
        };

        _readRepositoryMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(Task.FromResult<Employee?>(employeeExpected));
        _readRepositoryMock.Setup(x => x.CheckDocumentExists(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));

        // Act
        var act = () => _handler.Handle(command);

        // Assert
        var exception = await Assert.ThrowsAsync<ApplicationValidationException>(act);
        _writeRepositoryMock.Verify(x => x.Insert(It.IsAny<Employee>()), Times.Never);
        Assert.Equal("Document ready exists", exception.Message);
    }
}
