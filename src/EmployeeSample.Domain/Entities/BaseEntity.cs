using EmployeeSample.Domain.Abstractions.Notifications;

namespace EmployeeSample.Domain.Entities;

public abstract class BaseEntity : Notificable
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }

    public BaseEntity()
    {
        this.Id = Guid.NewGuid();
        this.CreatedAt = DateTime.Now;
    }

    protected bool EnumContains<TEnum>(char value) where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(v => Convert.ToChar(v))
                .ToList()
                .Contains(value);
    }

    public dynamic AddId(Guid id)
    {
        Id = id;
        return this;
    }
}
