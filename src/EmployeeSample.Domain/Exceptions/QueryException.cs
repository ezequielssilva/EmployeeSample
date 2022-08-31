namespace EmployeeSample.Domain.Exceptions;

public class QueryException : Exception
{
    public QueryException(string message) : base(message) { }
}
