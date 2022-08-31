namespace EmployeeSample.Application.Features.Employees.Queries.FindAllEmployee;

public class FindAllEmployeeQueryResult
{
    public int Page { get; private set; }
    public int Limit { get; private set; }
    public int Quantity { get; private set; }
    public int Total { get; private set; }
    public IEnumerable<EmployeeDto> Records { get; private set; }

    public FindAllEmployeeQueryResult(int page, int limit, IEnumerable<EmployeeDto> data)
    {
        Page = page;
        Limit = limit;
        Records = data;

        Quantity = data.Count();
    }
}
