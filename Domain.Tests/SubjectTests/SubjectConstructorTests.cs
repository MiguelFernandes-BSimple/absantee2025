using Domain.Models;
using Moq;

namespace Domain.Tests.SubjectTests;

public class SubjectConstructorTests
{
    [Fact]
    public void WhenPassingArguments_ThenClassIsInstatiated()
    {
        // Arrange

        // Act
        var result = new Subject("qwe", "rty");

        // Assert
        Assert.NotNull(result);
    }

    [Theory]
    [InlineData("qweqweqweqweqweqweqweqweqweqweqweqwe")]
    [InlineData("")]
    public void WhenPassingInvalidTitle_ThenThrowException(string title)
    {
        // Arrange

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new Subject(title, "qwe"));

        Assert.Equal("Invalid title.", exception.Message);
    }

    [Theory]
    [InlineData("qweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqweqwe")]
    [InlineData("")]
    public void WhenPassingInvalidDescription_ThenThrowException(string description)
    {
        // Arrange

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new Subject("qwe", description));

        Assert.Equal("Invalid description.", exception.Message);
    }



}