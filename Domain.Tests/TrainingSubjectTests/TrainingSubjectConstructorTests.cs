using Domain.Models;

namespace Domain.Tests.TrainingSubjectTests;

public class TrainingSubjectConstructorTests
{
    [Theory]
    [InlineData("title", "description")]
    public void WhenPassingValidParameters_ThenInstatiateObject(string title, string description)
    {
        // Arrange

        // Act
        var result = new TrainingSubject(title, description);

        // Assert
        Assert.NotNull(result);
    }

    [Theory]
    [InlineData("", "description")]
    [InlineData("akdysfaisdfiuadhfiouahdoiufhaousdihfuadfksauydgfuayd", "Description")]
    public void WhenPassingInvalidTitle_ThenInstatiateObject(string title, string description)
    {
        // Arrange

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new TrainingSubject(title, description));
    }

    [Theory]
    [InlineData("title", "")]
    [InlineData("title", "akdysfaisdfiuadhfiouahdoiufhaousdihfuadfksauydgfuaydakdysfaisdfiuadhfiouahdoiufhaousdihfuadfksauydgfuaydakdysfaisdfiuadhfiouahdoiufhaousdihfuadfksauydgfuaydakdysfaisdfiuadhfiouahdoiufhaousdihfuadfksauydgfuayd")]
    public void WhenPassingInvalidDescription_ThenInstatiateObject(string title, string description)
    {
        // Arrange

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new TrainingSubject(title, description));
    }
}