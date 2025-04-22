using Domain.Models;

namespace Domain.Tests.TrainingSubjectTests;

public class TrainingSubjectConstructorTests
{
    // Happy path: valid title and description
    [Theory]
    [InlineData("ValidTitle1", "This is a valid description.")]
    [InlineData("AnotherValid", "Short desc.")]
    [InlineData("Sub123", "Lorem ipsum dolor sit amet.")]
    public void WhenPassingValidInput_ThenInstatiateObject(string title, string description)
    {
        // Act
        var subject = new TrainingSubject(title, description);

        // Assert
        Assert.Equal(title, subject.Title);
        Assert.Equal(description, subject.Description);
    }

    // Invalid: null or whitespace title
    [Theory]
    [InlineData(null, "Valid description")]
    [InlineData("", "Valid description")]
    [InlineData("   ", "Valid description")]
    public void WhenPassingInvalidTitle_ThenThrowException(string title, string description)
    {
        // Arrange

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new TrainingSubject(title, description));
    }

    // Invalid: null or whitespace description
    [Theory]
    [InlineData("ValidTitle", null)]
    [InlineData("ValidTitle", "")]
    [InlineData("ValidTitle", "    ")]
    public void WhenPassingNullOrWhiteSpaceDescription_ThenThrowException(string title, string description)
    {
        // Arrange

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new TrainingSubject(title, description));
    }

    // Invalid: title too long
    [Fact]
    public void WhenPassingLongTitle_ThenThrowException()
    {
        var title = new string('a', 21);
        var description = "Valid description";
        Assert.Throws<ArgumentException>(() => new TrainingSubject(title, description));
    }

    // Invalid: description too long
    [Fact]
    public void WhenPassingLongDescription_ThenThrowsException()
    {
        var title = "ValidTitle";
        var description = new string('d', 101);
        Assert.Throws<ArgumentException>(() => new TrainingSubject(title, description));
    }

    // Invalid: title or description matches regex (only alphanumeric)
    [Theory]
    [InlineData("Title123", "Valid description")]
    [InlineData("ValidTitle", "Valid123")]
    public void WhenPassingTitleOrDescriptionNotAlphanumeric_ThenThrowsException(string title, string description)
    {
        Assert.Throws<ArgumentException>(() => new TrainingSubject(title, description));
    }
}