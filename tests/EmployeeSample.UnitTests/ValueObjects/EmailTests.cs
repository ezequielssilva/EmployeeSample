using EmployeeSample.Domain.ValueObjects;
using EmployeeSample.UnitTests.Fakes;

namespace EmployeeSample.UnitTests.ValueObjects;

public class EmailTests
{
    private TestNotificable _notificable;

    public EmailTests()
    {
        _notificable = new TestNotificable();
    }

    [Theory]
    [InlineData("ataide.moura@geradornv.com.br")]
    [InlineData("carlaalbergaria@geradornv.com")]
    [InlineData("fausto@geradornv.com.br")]
    public void SuccessWhenEmailIsValid(string address)
    {
        // Act
        Email email = new Email(_notificable, address);

        // Assert
        Assert.False(_notificable.HasNotification());
    }

    [Fact]
    public void ErrorWhenEmailIsEmpty()
    {
        // Act
        Email email = new Email(_notificable, "");

        // Assert
        Assert.True(_notificable.HasNotification());
        Assert.Collection(
            _notificable.GetNotifications().Select(x => x.Message).ToList(),
            notification => Assert.Contains("E-mail is required", notification)
        );
    }

    [Theory]
    [InlineData("ataide.moura.geradornv.com.br")]
    [InlineData("carlaalbergaria@geradornv")]
    [InlineData("fausto.com.br")]
    public void ErrorWhenEmailIsInvalid(string address)
    {
        // Act
        Email email = new Email(_notificable, address);

        // Assert
        Assert.True(_notificable.HasNotification());
        Assert.Collection(
            _notificable.GetNotifications().Select(x => x.Message).ToList(),
            notification => Assert.Contains("E-mail is invalid", notification)
        );
    }
}
