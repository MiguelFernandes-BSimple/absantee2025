using Domain.Factory;
using Domain.Models;

namespace Domain.Tests.TrainingModuleTests;

public class TrainingModuleFactoryTests {
    [Fact]
    public async Task WhenReceivesGoodArguments_ThenObjectIsInstantiated() {
        // Arrange
        var period = new PeriodDateTime(new DateTime(2045, 1, 1), new DateTime(2045, 1, 1));
        var periods = new List<PeriodDateTime> {period};

        var moduleFactory = new TrainingModuleFactory();

        // Act
        moduleFactory.Create(1, periods);

        // Assert
    }
    
    [Fact]
    public async Task WhenReceivesInterceptingPeriods_ThenExceptionIsThrown() {
        // Arrange
        var p1 = new PeriodDateTime(new DateTime(2045, 1, 1), new DateTime(2045, 1, 5));
        var p2 = new PeriodDateTime(new DateTime(2045, 1, 1), new DateTime(2045, 1, 5));
        var periods = new List<PeriodDateTime> {p1, p2};

        var moduleFactory = new TrainingModuleFactory();
 
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
            () => 
                // Act
                moduleFactory.Create(1, periods)
        );

        // Assert
        Assert.Equal("Periods intercept", exception.Message);
    }

    [Fact]
    public async Task WhenReceivesEmptyPeriods_ThenExceptionIsThrown() {
        // Arrange
        var periods = new List<PeriodDateTime>();
        
        var moduleFactory = new TrainingModuleFactory();
 
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
            () =>
                // Act
                moduleFactory.Create(1, periods)
        );

        // Assert
        Assert.Equal("No periods given", exception.Message);
    }
}
