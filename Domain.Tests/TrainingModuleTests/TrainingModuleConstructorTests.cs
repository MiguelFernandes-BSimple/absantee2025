using Domain.Models;
using Domain.Interfaces;

namespace Domain.Tests.TrainingModuleTests;

public class TrainingModuleConstructorTests {
    [Fact]
    public void WhenReceivesGoodArguments_ThenObjectIsInstantiated() {
        // Arrange
        var period = new PeriodDateTime(new DateTime(2045, 1, 1), new DateTime(2045, 1, 1));
        var periods = new List<PeriodDateTime> {period};

        // Act
        new TrainingModule(1, periods);

        // Assert
    }
}
