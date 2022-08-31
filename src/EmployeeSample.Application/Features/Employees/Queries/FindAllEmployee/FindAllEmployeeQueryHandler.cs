using EmployeeSample.Application.Interfaces.Queries;
using EmployeeSample.Application.Interfaces.ReadRepositories;

namespace EmployeeSample.Application.Features.Employees.Queries.FindAllEmployee;

public class FindAllEmployeeQueryHandler : IQueryHandler<FindAllEmployeeQuery, FindAllEmployeeQueryResult>
{
    private readonly IReadEmployeeRepository _readRepository;

    public FindAllEmployeeQueryHandler(IReadEmployeeRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<FindAllEmployeeQueryResult> Execute(FindAllEmployeeQuery query)
    {
        var results = await _readRepository.GetAll(query.Page, query.Limit);

        return new FindAllEmployeeQueryResult(query.Page, query.Limit, results);
    }
}
