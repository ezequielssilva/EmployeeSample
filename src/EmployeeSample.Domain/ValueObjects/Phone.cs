using System.Text.RegularExpressions;
using EmployeeSample.Domain.Abstractions.Notifications;

namespace EmployeeSample.Domain.ValueObjects;

public class Phone : BaseValueObject
{
    private string Input;

    public Phone(Notificable notificable, string input)
    {
        this.Input = string.Empty;

        if (string.IsNullOrEmpty(input))
            notificable.AddNotification("Phone", "Phone is required");
        else if (Sanitize(input).Length < 10)
            notificable.AddNotification("Phone", "Phone less than 10 characters");
        else if (Sanitize(input).Length > 20)
            notificable.AddNotification("Phone", "Phone must a maximum of 20 characters");
        else if (!Regex.IsMatch(Sanitize(input), @"^\d+$"))
            notificable.AddNotification("Phone", "Fill only with numbers");
        else
            this.Input = Sanitize(input);
    }

    private string Sanitize(string value)
    {
        return value.Replace(".", "").Replace("-", "").Replace("(", "").Replace(")", "");
    }

    public override string ToString()
    {
        return this.Input;
    }
}
