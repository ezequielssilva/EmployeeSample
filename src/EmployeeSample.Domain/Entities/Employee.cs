using EmployeeSample.Domain.Abstractions.Validations;
using EmployeeSample.Domain.Enums;
using EmployeeSample.Domain.Exceptions;
using EmployeeSample.Domain.ValueObjects;

namespace EmployeeSample.Domain.Entities;

public class Employee : BaseEntity, IDomainValidable
{
    public Document Document { get; private set; } = default!;
    public Name Name { get; private set; } = default!;
    public Sex Sex { get; private set; } = default!;
    public MaritalStatus MaritalStatus { get; private set; } = default!;
    public EducationLevel EducationLevel { get; private set; } = default!;
    public BirthDate BirthDate { get; private set; } = default!;
    public Phone Phone { get; private set; } = default!;
    public Email Email { get; private set; } = default!;

    private Employee() { }

    public Employee(
        string document,
        string fullName,
        string socialName,
        string sex,
        int? maritalStatus,
        int? educationLevel,
        string birthDate,
        string phone,
        string email
    )
    {
        this.SetDocument(document)
            .SetName(fullName, socialName)
            .SetBirthDate(birthDate)
            .SetEmail(email)
            .SetPhone(phone)
            .SetSex(sex)
            .SetMaritalStatus(maritalStatus)
            .SetEducationLevel(educationLevel)
            .SetCreatedAt(DateTime.Now);
    }

    public void ChangeEmployee(
        string document,
        string fullName,
        string socialName,
        string sex,
        int? maritalStatus,
        int? educationLevel,
        string birthDate,
        string phone,
        string email
    )
    {
        this.SetDocument(document)
            .SetName(fullName, socialName)
            .SetBirthDate(birthDate)
            .SetEmail(email)
            .SetPhone(phone)
            .SetSex(sex)
            .SetMaritalStatus(maritalStatus)
            .SetEducationLevel(educationLevel)
            .SetUpdatedAt(DateTime.Now);
    }

    public void DomainValidate()
    {
        if (this.HasNotification())
            throw new NotificationException(this);
    }

    public Employee SetSex(string sex)
    {
        if (string.IsNullOrEmpty(sex))
            this.AddNotification("Sex", "Sex is required!");
        else if (!EnumContains<Sex>(Convert.ToChar(sex)))
            this.AddNotification("Sex", "Sex Invalid!");
        else
            this.Sex = sex switch
            {
                "M" => Sex.Masculine,
                "F" => Sex.Feminine,
                _ => Sex.Uninformed
            };

        return this;
    }

    public Employee SetMaritalStatus(int? maritalStatus)
    {
        if (maritalStatus == null)
            this.AddNotification("MaritalStatus", "Marital Status is required");
        else if (!Enum.IsDefined(typeof(MaritalStatus), maritalStatus!))
            this.AddNotification("MaritalStatus", "Marital Status Invalid");
        else
            this.MaritalStatus = (MaritalStatus)maritalStatus;

        return this;
    }

    public Employee SetEducationLevel(int? educationLevel)
    {
        if (educationLevel == null)
            this.AddNotification("EducationLevel", "Education Level is required");
        else if (!Enum.IsDefined(typeof(EducationLevel), educationLevel!))
            this.AddNotification("EducationLevel", "Education Level Invalid");
        else
            this.EducationLevel = (EducationLevel)educationLevel;

        return this;
    }

    public Employee SetDocument(string document)
    {
        this.Document = new Document(this, document);
        return this;
    }

    public Employee SetName(string fullName, string socialName)
    {
        this.Name = new Name(this, fullName, socialName);
        return this;
    }

    public Employee SetPhone(string phone)
    {
        this.Phone = new Phone(this, phone);
        return this;
    }

    public Employee SetEmail(string email)
    {
        this.Email = new Email(this, email);
        return this;
    }

    public Employee SetBirthDate(string birthDate)
    {
        this.BirthDate = new BirthDate(this, birthDate);
        return this;
    }

    public Employee SetBirthDate(DateTime birthDate)
    {
        this.BirthDate = new BirthDate(this, birthDate.ToString());
        return this;
    }

    public Employee SetCreatedAt(DateTime createdAt)
    {
        this.CreatedAt = createdAt;
        return this;
    }

    public Employee SetUpdatedAt(DateTime? updatedAt)
    {
        this.UpdatedAt = updatedAt;
        return this;
    }
}
