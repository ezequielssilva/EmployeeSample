using EmployeeSample.Domain.ValueObjects;
using EmployeeSample.UnitTests.Fakes;

namespace EmployeeSample.UnitTests.ValueObjects;

public class DocumentTests
{
    private TestNotificable _notificable;

    public DocumentTests()
    {
        _notificable = new TestNotificable();
    }

    [Theory]
    [InlineData("21453804005")]
    [InlineData("43281194001")]
    [InlineData("61610998022")]
    [InlineData("40486532020")]
    [InlineData("44533744028")]
    public void SuccessWhenDocumentValid(string value)
    {
        // Act
        Document document = new Document(_notificable, value);

        // Assert
        Assert.False(_notificable.HasNotification());
    }

    [Fact]
    public void ErrorWhenDocumentIsEmpty()
    {
        // Act
        Document document = new Document(_notificable, "");

        // Assert
        Assert.True(_notificable.HasNotification());
        Assert.Collection(
            _notificable.GetNotifications().Select(x => x.Message).ToList(),
            notification => Assert.Contains("Document is required", notification)
        );
    }

    [Theory]
    [InlineData("8773021806")]
    [InlineData("3137423007")]
    [InlineData("5218516805")]
    [InlineData("2260114300")]
    [InlineData("5571816309")]
    public void ErrorWhenDocumentLessThan11Characters(string value)
    {
        // Act
        Document document = new Document(_notificable, value);

        // Assert
        Assert.True(_notificable.HasNotification());
        Assert.Collection(
            _notificable.GetNotifications().Select(x => x.Message).ToList(),
            notification => Assert.Contains("Document less than 11 characters", notification)
        );
    }

    [Theory]
    [InlineData("8773@218069")]
    [InlineData("3137B23007)")]
    [InlineData("ABCDEFGHIJK")]
    [InlineData("4(533K44@2$")]
    public void ErrorWhenDocumentThereAtNotJustNumbers(string value)
    {
        // Act
        Document document = new Document(_notificable, value);

        // Assert
        Assert.True(_notificable.HasNotification());
        Assert.Collection(
            _notificable.GetNotifications().Select(x => x.Message).ToList(),
            notification => Assert.Contains("Fill only with numbers", notification)
        );
    }

    [Theory]
    [InlineData("00000000000")]
    [InlineData("11111111111")]
    [InlineData("22222222222")]
    [InlineData("33333333333")]
    [InlineData("44444444444")]
    [InlineData("55555555555")]
    [InlineData("66666666666")]
    [InlineData("77777777777")]
    [InlineData("88888888888")]
    [InlineData("99999999999")]
    [InlineData("01234567890")]
    [InlineData("21453704005")]
    [InlineData("43281894001")]
    [InlineData("61620998922")]
    [InlineData("40486132025")]
    [InlineData("41533704028")]
    public void ErrorWhenCPFInvalid(string value)
    {
        // Act
        Document document = new Document(_notificable, value);

        // Assert
        Assert.True(_notificable.HasNotification());
        Assert.Collection(
            _notificable.GetNotifications().Select(x => x.Message).ToList(),
            notification => Assert.Contains("CPF is invalid", notification)
        );
    }
}
