using EmployeeSample.Application.Interfaces.Queries;
using EmployeeSample.Application.Interfaces.ReadRepositories;
using EmployeeSample.Domain.Exceptions;

namespace EmployeeSample.Application.Features.Employees.Queries.FindEmployeeByDocument;

public class FindEmployeeByDocumentQueryHandler : IQueryHandler<FindEmployeeByDocumentQuery, EmployeeDto>
{
    private readonly IReadEmployeeRepository _readRepository;

    public FindEmployeeByDocumentQueryHandler(IReadEmployeeRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<EmployeeDto> Execute(FindEmployeeByDocumentQuery query)
    {
        if (string.IsNullOrEmpty(query.Document))
            throw new QueryException("Document not informed");

        var employee = await _readRepository.GetByDocument(query.Document);
        if (employee == null)
            throw new EntityNotFoundException("Employee not found");

        return new EmployeeDto().MapFrom(employee);
    }
}
