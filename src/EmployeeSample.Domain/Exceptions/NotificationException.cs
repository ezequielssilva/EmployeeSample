using System.Text.Json;
using EmployeeSample.Domain.Abstractions.Notifications;

namespace EmployeeSample.Domain.Exceptions;

public class NotificationException : Exception
{
    public IDictionary<string, IList<string>> Errors { get; private set; }

    public NotificationException(Notificable notificable) : base(JsonSerializer.Serialize(notificable.GetNotifications()))
    {
        Errors = new Dictionary<string, IList<string>>();

        foreach (var notification in notificable.GetNotifications())
        {
            if (!Errors.ContainsKey(notification.PropertyName))
                Errors[notification.PropertyName] = new List<string>();

            Errors[notification.PropertyName].Add(notification.Message);
        }

        notificable.CleanNotifications();
    }
}
