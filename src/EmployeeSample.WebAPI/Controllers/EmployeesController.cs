using EmployeeSample.Application.Features.Employees.Commands.CreateEmployee;
using EmployeeSample.Application.Features.Employees.Commands.DeleteEmployee;
using EmployeeSample.Application.Features.Employees.Commands.UpdateEmployee;
using EmployeeSample.Application.Features.Employees.Queries;
using EmployeeSample.Application.Features.Employees.Queries.FindAllEmployee;
using EmployeeSample.Application.Features.Employees.Queries.FindEmployeeByDocument;
using EmployeeSample.Application.Features.Employees.Queries.FindEmployeeById;
using EmployeeSample.Application.Interfaces.Commands;
using EmployeeSample.Application.Interfaces.Queries;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSample.WebAPI.Controllers;

public class EmployeesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Post(
        [FromServices] ICommandHandler<CreateEmployeeCommand, CreateEmployeeCommandResult> handler,
        [FromBody] CreateEmployeeCommand command
    ) => Success((await handler.Handle(command)), true);

    [HttpPut]
    public async Task<IActionResult> Put(
        [FromServices] ICommandHandler<UpdateEmployeeCommand, UpdateEmployeeCommandResult> handler,
        [FromBody] UpdateEmployeeCommand command
    ) => Success((await handler.Handle(command)));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        [FromServices] ICommandHandler<DeleteEmployeeCommand, DeleteEmployeeCommandResult> handler,
        [FromRoute] DeleteEmployeeCommand command
    ) => Success((await handler.Handle(command)));


    [HttpGet("{document}/document")]
    public async Task<IActionResult> GetEmployeeByDocument(
        [FromRoute] FindEmployeeByDocumentQuery query,
        [FromServices] IQueryHandler<FindEmployeeByDocumentQuery, EmployeeDto> handler
    ) => Success(await handler.Execute(query));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(
        [FromRoute] FindEmployeeByIdQuery query,
        [FromServices] IQueryHandler<FindEmployeeByIdQuery, EmployeeDto> handler
    ) => Success(await handler.Execute(query));

    [HttpGet]
    public async Task<IActionResult> GetAllEmployee(
        [FromQuery] FindAllEmployeeQuery query,
        [FromServices] IQueryHandler<FindAllEmployeeQuery, FindAllEmployeeQueryResult> handler
    ) => Success(await handler.Execute(query));

}
