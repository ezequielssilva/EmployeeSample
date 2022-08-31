using EmployeeSample.Application.Interfaces.Queries;
using EmployeeSample.Application.Interfaces.ReadRepositories;
using EmployeeSample.Domain.Exceptions;

namespace EmployeeSample.Application.Features.Employees.Queries.FindEmployeeById;

public class FindEmployeeByIdQueryHandler : IQueryHandler<FindEmployeeByIdQuery, EmployeeDto>
{
    private readonly IReadEmployeeRepository _readRepository;

    public FindEmployeeByIdQueryHandler(IReadEmployeeRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<EmployeeDto> Execute(FindEmployeeByIdQuery query)
    {
        if (string.IsNullOrEmpty(query.Id))
            throw new QueryException("EmployeeId not informed");

        var employee = await _readRepository.GetById(query.Id);
        if (employee == null)
            throw new EntityNotFoundException("Employee not found");

        return new EmployeeDto().MapFrom(employee);
    }
}
