using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectTests;
public class ProjectConstructorTests
{
    [Fact]
    public void WhenPassingValidDataToConstructorWithoutGUID_ThenCreatesProject()
    {
        // Arrange
        string validTitle = "Title";
        string validAcronym = "ACR";

        // Act & Assert
        new Project(validTitle, validAcronym, It.IsAny<PeriodDate>());
    }

    [Fact]
    public void WhenPassingValidDataToConstructorWithGUID_ThenCreatesProject()
    {
        // Arrange
        string validTitle = "Title";
        string validAcronym = "ACR";

        // Act & Assert
        new Project(Guid.NewGuid(), validTitle, validAcronym, It.IsAny<PeriodDate>());
    }

    [Theory]
    [InlineData("")]
    [InlineData("Este título tem facilmente mais de cinquenta caracteres.")]
    public void WhenPassingInvalidTitleToConstructorWithoutGUID_ThenThrowsException(string title)
    {
        // Arrange
        string validAcronym = new string('a', 10);
        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new Project(title, validAcronym, It.IsAny<PeriodDate>())

        );

        Assert.Equal("Title must be between 1 and 50 characters.", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Este título tem facilmente mais de cinquenta caracteres.")]
    public void WhenPassingInvalidTitleToConstructorWithGUID_ThenThrowsException(string title)
    {
        // Arrange
        string validAcronym = new string('a', 10);
        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new Project(Guid.NewGuid(), title, validAcronym, It.IsAny<PeriodDate>())

        );

        Assert.Equal("Title must be between 1 and 50 characters.", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("pjt")]
    [InlineData("PJT@")]
    [InlineData("pjtJtjtjtjt")]
    public void WhenPassingInvalidAcronymWithGUID_ThenThrowsException(string acronym)
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new Project(Guid.NewGuid(), "Title", acronym, It.IsAny<PeriodDate>())
        );

        Assert.Equal("Acronym must be 1 to 10 characters long and contain only uppercase letters and digits.", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("pjt")]
    [InlineData("PJT@")]
    [InlineData("pjtJtjtjtjt")]
    public void WhenPassingInvalidAcronymWithoutGUID_ThenThrowsException(string acronym)
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new Project("Title", acronym, It.IsAny<PeriodDate>())
        );

        Assert.Equal("Acronym must be 1 to 10 characters long and contain only uppercase letters and digits.", exception.Message);
    }
}
