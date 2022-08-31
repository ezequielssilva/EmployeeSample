using EmployeeSample.Domain.Abstractions.Notifications;

namespace EmployeeSample.Domain.ValueObjects;

public class BirthDate : BaseValueObject
{
    public DateTime Date { get; private set; }

    public BirthDate(Notificable notificable, string value)
    {
        if (string.IsNullOrEmpty(value))
            notificable.AddNotification("BirthDate", "BirthDate is required");
        else if (!DateTime.TryParse(value, out DateTime date) || date.Year < 1900)
            notificable.AddNotification("BirthDate", "BirthDate is invalid!");
        else if (GetAge(date) < 18)
            notificable.AddNotification("BirthDate", "Age under 18");
        else
            this.Date = date;
    }

    private int GetAge(DateTime date)
    {
        var diff = DateTime.Today - date;
        DateTime temp = DateTime.MinValue + diff;
        int age = temp.Year - 1;

        return age;
    }

    public override string ToString()
    {
        return this.Date.ToString("yyyy-MM-dd");
    }
}
