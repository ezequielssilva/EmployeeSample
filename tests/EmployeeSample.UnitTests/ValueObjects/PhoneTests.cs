using EmployeeSample.Domain.ValueObjects;
using EmployeeSample.UnitTests.Fakes;

namespace EmployeeSample.UnitTests.ValueObjects;

public class PhoneTests
{
    private TestNotificable _notificable;

    public PhoneTests()
    {
        _notificable = new TestNotificable();
    }

    [Theory]
    [InlineData("87997302180")]
    [InlineData("31937423001")]
    [InlineData("5218516805")]
    [InlineData("2260114306")]
    [InlineData("5571816301")]
    public void SuccessWhenPhoneIsValid(string value)
    {
        // Act
        Phone phone = new Phone(_notificable, value);

        // Assert
        Assert.False(_notificable.HasNotification());
    }

    [Fact]
    public void ErrorWhenPhoneIsEmpty()
    {
        // Act
        Phone phone = new Phone(_notificable, "");

        // Assert
        Assert.True(_notificable.HasNotification());
        Assert.Collection(
            _notificable.GetNotifications().Select(x => x.Message).ToList(),
            notification => Assert.Contains("Phone is required", notification)
        );
    }

    [Theory]
    [InlineData("877302180")]
    [InlineData("313742300")]
    [InlineData("521851680")]
    [InlineData("226011430")]
    [InlineData("557181630")]
    public void ErrorWhenPhoneLessThan10Characters(string value)
    {
        // Act
        Phone phone = new Phone(_notificable, value);

        // Assert
        Assert.True(_notificable.HasNotification());
        Assert.Collection(
            _notificable.GetNotifications().Select(x => x.Message).ToList(),
            notification => Assert.Contains("Phone less than 10 characters", notification)
        );
    }

    [Theory]
    [InlineData("8773@218069")]
    [InlineData("3137B23007)")]
    [InlineData("ABCDEFGHIJK")]
    [InlineData("4(533K44@2$")]
    public void ErrorWhenPhoneThereAtNotJustNumbers(string value)
    {
        // Act
        Phone phone = new Phone(_notificable, value);

        // Assert
        Assert.True(_notificable.HasNotification());
        Assert.Collection(
            _notificable.GetNotifications().Select(x => x.Message).ToList(),
            notification => Assert.Contains("Fill only with numbers", notification)
        );
    }

    [Theory]
    [InlineData("883754638301748320591")]
    public void ErrorWhenPhoneOver20Characters(string value)
    {
        // Act
        Phone phone = new Phone(_notificable, value);

        // Assert
        Assert.True(_notificable.HasNotification());
        Assert.Collection(
            _notificable.GetNotifications().Select(x => x.Message).ToList(),
            notification => Assert.Contains("Phone must a maximum of 20 characters", notification)
        );
    }
}
