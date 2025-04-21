using Domain.Models;

namespace Domain.Tests.UserTests;

public class FormationSubjectConstructorTests
{
    [Fact]
    public void WhenCreatingFormationSubjectWithValidFields_ThenNoExceptionIsThrown()
    {
        // Act && Assert
        new FormationSubject("Dotnet", "Formation about dotnet");
    }

    [Fact]
    public void WhenCreatingUserWithInvalidTitle_ThenThrowsArgumentException()
    {
        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
        // Act
        new FormationSubject(".Net*", "Formation about dotnet"));

        Assert.Equal("Invalid title", exception.Message);
    }

    [Fact]
    public void WhenCreatingUserWithInvalidDescription_ThenThrowsArgumentException()
    {
        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
        // Act
        new FormationSubject("Dotnet", "Formation about *.Net"));

        Assert.Equal("Invalid description", exception.Message);
    }
}