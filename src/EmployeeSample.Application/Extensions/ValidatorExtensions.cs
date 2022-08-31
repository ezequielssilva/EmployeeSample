using EmployeeSample.Application.Interfaces.Commands;
using EmployeeSample.Domain.Exceptions;
using FluentValidation;

namespace EmployeeSample.Application.Extensions;

public static class ValidatorExtensions
{
    public static async Task ValidateAsync<T>(this T command, AbstractValidator<T> validator) where T : ICommand
    {
        var results = await validator.ValidateAsync(command);

        if (results.IsValid)
            return;

        var errors = new Dictionary<string, IList<string>>();

        foreach (var error in results.Errors)
        {
            if (!errors.ContainsKey(error.PropertyName))
                errors.Add(error.PropertyName, new List<string>());

            errors[error.PropertyName].Add(error.ErrorMessage);
        }

        throw new ValidatorException(errors);
    }
}
