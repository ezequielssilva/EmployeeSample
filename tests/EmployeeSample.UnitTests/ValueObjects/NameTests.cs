using EmployeeSample.Domain.ValueObjects;
using EmployeeSample.UnitTests.Fakes;

namespace EmployeeSample.UnitTests.ValueObjects;

public class NameTests
{
    private TestNotificable _notificable;

    public NameTests()
    {
        _notificable = new TestNotificable();
    }

    [Fact]
    public void SuccessWhenReturnSocialName()
    {
        // Act
        string fullName = "Test FullName";
        string socialName = "Test SocialName";
        Name name = new Name(_notificable, fullName, socialName);

        // Assert
        Assert.Equal(socialName, name.ToString());
    }

    [Fact]
    public void SuccessWhenReturnFullNameWhenSocialNameNotInformed()
    {
        // Act
        string fullName = "Test FullName";
        Name name = new Name(_notificable, fullName, "");

        // Assert
        Assert.Equal(fullName, name.ToString());
    }

    [Fact]
    public void ErrorWhenFullNameIsEmpty()
    {
        // Act
        Name name = new Name(_notificable, "", "");

        // Assert
        Assert.True(_notificable.HasNotification());
        Assert.Collection(
            _notificable.GetNotifications().Select(x => x.Message).ToList(),
            notification => Assert.Contains("FullName is required", notification)
        );
    }

    [Fact]
    public void ErrorWhenFullNameOver60Characters()
    {
        // Act
        Name name = new Name(_notificable, "Test Full Name Over 60 Characters Test Full Name Over 60 Char", "");

        // Assert
        Assert.True(_notificable.HasNotification());
        Assert.Collection(
            _notificable.GetNotifications().Select(x => x.Message).ToList(),
            notification => Assert.Contains("FullName must a maximum of 60 characters", notification)
        );
    }

    [Fact]
    public void ErrorWhenSocialNameOver60Characters()
    {
        // Act
        Name name = new Name(_notificable, "Test Full Name", "Test SocialName over 60 characters Test Social Name over 60 ch");

        // Assert
        Assert.True(_notificable.HasNotification());
        Assert.Collection(
            _notificable.GetNotifications().Select(x => x.Message).ToList(),
            notification => Assert.Contains("SocialName must a maximum of 60 characters", notification)
        );
    }
}
