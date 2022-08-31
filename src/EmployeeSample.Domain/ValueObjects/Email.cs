using System.Text.RegularExpressions;
using EmployeeSample.Domain.Abstractions.Notifications;

namespace EmployeeSample.Domain.ValueObjects;

public class Email : BaseValueObject
{
    private string Address;

    public Email(Notificable notificable, string address)
    {
        this.Address = string.Empty;

        if (string.IsNullOrEmpty(address))
            notificable.AddNotification("Email", "E-mail is required");
        else if (!Regex.IsMatch(address, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            notificable.AddNotification("Email", "E-mail is invalid");
        else
            this.Address = address.ToLower();
    }

    public override string ToString()
    {
        return this.Address;
    }
}
