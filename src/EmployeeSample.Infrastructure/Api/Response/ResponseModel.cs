namespace EmployeeSample.Infrastructure.Api.Response;

public class ResponseModel
{
    public bool Success { get; private set; } = default!;
    public object Data { get; private set; } = default!;
    public object Errors { get; private set; } = default!;

    public ResponseModel(bool success, object value)
    {
        this.Success = success;

        if (success)
            this.Data = value;
        else
            this.Errors = value;
    }
}
