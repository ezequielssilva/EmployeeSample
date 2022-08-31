namespace EmployeeSample.Domain.Exceptions;

public class ApplicationValidationException : Exception
{
    public IDictionary<string, IList<string>> Errors { get; private set; }

    public ApplicationValidationException(string propertyName, string message) : base(message)
    {
        this.Errors = new Dictionary<string, IList<string>>();

        this.Errors.Add(propertyName, new List<string> { message });
    }
}
