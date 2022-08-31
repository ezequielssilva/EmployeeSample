using System.Text.RegularExpressions;
using EmployeeSample.Domain.Abstractions.Notifications;

namespace EmployeeSample.Domain.ValueObjects;

public class Document : BaseValueObject
{
    private string Value;

    public Document(Notificable notificable, string value)
    {
        this.Value = string.Empty;

        if (string.IsNullOrEmpty(value))
            notificable.AddNotification("Document", "Document is required");
        else if (Sanitize(value).Length < 11)
            notificable.AddNotification("Document", "Document less than 11 characters");
        else if (!Regex.IsMatch(Sanitize(value), @"^\d+$"))
            notificable.AddNotification("Document", "Fill only with numbers");
        else if (Sanitize(value).Length == 11 && !CPFIsValid(Sanitize(value)))
            notificable.AddNotification("Document", "CPF is invalid");
        else
            this.Value = Sanitize(value);
    }

    private string Sanitize(string value)
    {
        return value.Replace(".", "").Replace("-", "").Replace("/", "");
    }

    private int GetCalculateDigit(string document, int sequence)
    {
        int index = 0;
        int result = 0;
        for (int seq = sequence; sequence >= 2; sequence--, index++)
            result += sequence * Convert.ToInt32(document[index].ToString());

        int rest = (result * 10) % 11;

        return rest > 9 ? 0 : rest;
    }

    private bool CPFIsValid(string document)
    {
        string[] invalids = new string[]
        {
            "00000000000",
            "11111111111",
            "22222222222",
            "33333333333",
            "44444444444",
            "55555555555",
            "66666666666",
            "77777777777",
            "88888888888",
            "99999999999",
            "01234567890"
        };

        if (invalids.Contains(document))
            return false;

        if (this.GetCalculateDigit(document, 10) != Convert.ToInt32(document[9].ToString()))
            return false;

        if (this.GetCalculateDigit(document, 11) != Convert.ToInt32(document[10].ToString()))
            return false;

        return true;
    }

    public override string ToString()
    {
        return this.Value;
    }
}
