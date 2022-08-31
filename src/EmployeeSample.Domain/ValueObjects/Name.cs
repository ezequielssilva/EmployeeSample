using EmployeeSample.Domain.Abstractions.Notifications;

namespace EmployeeSample.Domain.ValueObjects;

public class Name : BaseValueObject
{
    public string FullName { get; private set; }
    public string SocialName { get; private set; }

    public Name(Notificable notificable, string fullName, string socialName)
    {
        FullName = SocialName = string.Empty;

        if (string.IsNullOrEmpty(fullName))
            notificable.AddNotification("FullName", "FullName is required");
        else if (fullName.Length > 60)
            notificable.AddNotification("FullName", "FullName must a maximum of 60 characters");
        else
            FullName = fullName;

        if (!string.IsNullOrEmpty(socialName) && socialName.Length > 60)
            notificable.AddNotification("SocialName", "SocialName must a maximum of 60 characters");
        else if (!string.IsNullOrEmpty(socialName))
            SocialName = socialName;
    }

    public override string ToString()
    {
        return string.IsNullOrEmpty(this.SocialName)
            ? this.FullName
            : this.SocialName;
    }
}
