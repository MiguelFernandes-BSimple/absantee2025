using Domain.Models;
using Domain.Interfaces;

namespace Domain.Tests.TrainingSubjectTests;

public class TrainingSubjectConstructorTests {
    [Theory]
    [InlineData("a", "a")]
    [InlineData("title", "description")]
    [InlineData("12345678901234567890", "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
    public void WhenReceivesGoodArguments_ThenObjectIsInstantiated(string title, string description) {
        // Arrange

        // Act
        new TrainingSubject(title, description);

        // Assert
    }
}
