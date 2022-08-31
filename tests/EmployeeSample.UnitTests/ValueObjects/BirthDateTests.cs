using EmployeeSample.Domain.ValueObjects;
using EmployeeSample.UnitTests.Fakes;

namespace EmployeeSample.UnitTests.ValueObjects;

public class BirthDateTests
{
    [Theory]
    [InlineData("1998-01-12")]
    [InlineData("2004-05-19")]
    public void SuccessWhenValidBirthDate(string date)
    {
        // Arrange
        TestNotificable notificable = new TestNotificable();

        // Act
        BirthDate birthDate = new BirthDate(notificable, date);

        // Assert
        Assert.False(notificable.HasNotification());
    }

    [Fact]
    public void ErrorWhenDateFieldIsEmpty()
    {
        // Arrange
        TestNotificable notificable = new TestNotificable();

        // Act
        BirthDate birthDate = new BirthDate(notificable, "");

        // Assert
        Assert.True(notificable.HasNotification());
        Assert.Collection(
            notificable.GetNotifications().Select(x => x.Message).ToList(),
            notification => Assert.Contains("BirthDate is required", notification)
        );
    }

    [Theory]
    [InlineData("0001-01-01")]
    public void ErrorWhenDateIsInvalid(string date)
    {
        // Arrange
        TestNotificable notificable = new TestNotificable();

        // Act
        BirthDate birthDate = new BirthDate(notificable, date);

        // Assert
        Assert.True(notificable.HasNotification());
        Assert.Collection(
            notificable.GetNotifications().Select(x => x.Message).ToList(),
            notification => Assert.Contains("BirthDate is invalid", notification)
        );
    }

    [Theory]
    [InlineData("2005-06-20")]
    [InlineData("2013-11-25")]
    public void ErrorWhenAgeUnder18(string date)
    {
        // Arrange
        TestNotificable notificable = new TestNotificable();

        // Act
        BirthDate birthDate = new BirthDate(notificable, date);

        // Assert
        Assert.True(notificable.HasNotification());
        Assert.Collection(
            notificable.GetNotifications().Select(x => x.Message).ToList(),
            notification => Assert.Contains("Age under 18", notification)
        );
    }
}
