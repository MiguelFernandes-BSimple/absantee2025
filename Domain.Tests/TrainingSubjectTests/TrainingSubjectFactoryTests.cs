using Domain.Factory;

namespace Domain.Tests.TrainingSubjectTests;

public class TrainingSubjectFactoryTests {
    [Theory]
    [InlineData("a", "a")]
    [InlineData("title", "description")]
    [InlineData("12345678901234567890", "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
    public async Task WhenReceivesGoodArguments_ThenObjectIsInstantiated(string title, string description) {
        // Arrange
        var subjectFactory = new TrainingSubjectFactory();

        // Act
        await subjectFactory.Create(title, description);

        // Assert
    }

    [Theory]
    [InlineData("", "a")]
    [InlineData("#@ushd)", "a")]
    [InlineData("123456789012345678901", "a")]
    public async Task WhenReceivesBadTitle_ThenExceptionIsThrown(string title, string description) {
        // Arrange
        var subjectFactory = new TrainingSubjectFactory();

        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                // Act
                subjectFactory.Create(title, description)
            );

        // Assert
        Assert.Equal("Invalid Title", exception.Message);
    }

    
    [Theory]
    [InlineData("a", "")]
    [InlineData("a", "#@ushd)")]
    [InlineData("a", "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901")]
    public async Task WhenReceivesBadDescription_ThenExceptionIsThrown(string title, string description) {
        // Arrange
        var subjectFactory = new TrainingSubjectFactory();

        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                // Act
                subjectFactory.Create(title, description)
            );

        // Assert
        Assert.Equal("Invalid Description", exception.Message);
    }
}
