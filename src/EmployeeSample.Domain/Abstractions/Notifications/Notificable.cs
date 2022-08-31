namespace EmployeeSample.Domain.Abstractions.Notifications;

public abstract class Notificable
{
    private IList<Notification> _Notifications = new List<Notification>();

    public void AddNotification(string propertyName, string message)
        => _Notifications.Add(new Notification(propertyName, message));

    public IReadOnlyList<Notification> GetNotifications()
        => _Notifications.ToList();

    public void CleanNotifications()
        => _Notifications.Clear();

    public bool HasNotification()
        => _Notifications.Count() > 0;
}
