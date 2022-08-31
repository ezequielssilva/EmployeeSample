using System.Data;
using EmployeeSample.Application.Features.Employees.Commands.CreateEmployee;
using EmployeeSample.Application.Features.Employees.Commands.DeleteEmployee;
using EmployeeSample.Application.Features.Employees.Commands.UpdateEmployee;
using EmployeeSample.Application.Features.Employees.Queries;
using EmployeeSample.Application.Features.Employees.Queries.FindAllEmployee;
using EmployeeSample.Application.Features.Employees.Queries.FindEmployeeByDocument;
using EmployeeSample.Application.Features.Employees.Queries.FindEmployeeById;
using EmployeeSample.Application.Interfaces.Commands;
using EmployeeSample.Application.Interfaces.Data;
using EmployeeSample.Application.Interfaces.Queries;
using EmployeeSample.Application.Interfaces.ReadRepositories;
using EmployeeSample.Application.Interfaces.WriteRepositories;
using EmployeeSample.Persistence.Data;
using EmployeeSample.Persistence.ReadRepositories;
using EmployeeSample.Persistence.WriteRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeSample.Infrastructure.Extensions;

public static class BootstrapperExtensions
{
    public static IServiceCollection AddConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddTransient(
            typeof(IDbConnection),
            (provider) => new SqlConnection(configuration.GetConnectionString("PersistenceConnectionString"))
        );
        services.AddScoped<IPersistenceAdapter, DapperPersistenceAdapter>();

        services.AddScoped<IWriteEmployeeRepository, WriteEmployeeRepository>();

        services.AddScoped<IReadEmployeeRepository, ReadEmployeeRepositoryPersistence>();

        services.AddScoped<ICommandHandler<CreateEmployeeCommand, CreateEmployeeCommandResult>, CreateEmployeeCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateEmployeeCommand, UpdateEmployeeCommandResult>, UpdateEmployeeCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteEmployeeCommand, DeleteEmployeeCommandResult>, DeleteEmployeeCommandHandler>();

        services.AddScoped<IQueryHandler<FindEmployeeByDocumentQuery, EmployeeDto>, FindEmployeeByDocumentQueryHandler>();
        services.AddScoped<IQueryHandler<FindEmployeeByIdQuery, EmployeeDto>, FindEmployeeByIdQueryHandler>();
        services.AddScoped<IQueryHandler<FindAllEmployeeQuery, FindAllEmployeeQueryResult>, FindAllEmployeeQueryHandler>();

        return services;
    }
}
