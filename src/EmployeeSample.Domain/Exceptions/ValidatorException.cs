using System.Text.Json;

namespace EmployeeSample.Domain.Exceptions;

public class ValidatorException : Exception
{
    public IDictionary<string, IList<string>> Errors { get; private set; }

    public ValidatorException(IDictionary<string, IList<string>> errors)
        : base(JsonSerializer.Serialize(errors))
    {
        Errors = errors;
    }
}
