using Moq;
using Domain.Models;
using Domain.Factory;
using Domain.Visitor;
using Domain.Factory.TrainingPeriodFactory;

namespace Domain.Tests.TrainingModuleTests;

public class TrainingModuleFactoryTests
{
    [Fact]
    public void WhenPassingValidData_ThenIsCreated()
    {
        // Arrange
        var periods = new List<PeriodDateTime>
        {
            new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>())
        };

        ITrainingModuleFactory factory = new TrainingModuleFactory();

        // Act
        TrainingModule? result = factory.Create(It.IsAny<long>(), periods);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenPassingValidDataWithEmptyList_ThenIsCreated()
    {
        // Arrange

        ITrainingModuleFactory factory = new TrainingModuleFactory();

        // Act
        TrainingModule? result = factory.Create(It.IsAny<long>(), new List<PeriodDateTime>());

        // Assert
        Assert.NotNull(result);
    }


    [Fact]
    public void WhenPassingVisitor_ThenAssociationProjectCollaboratorIsCreated()
    {
        // Arrange
        var visitor = new Mock<ITrainingModuleVisitor>();

        ITrainingModuleFactory factory = new TrainingModuleFactory();

        // Act
        var result = factory.Create(visitor.Object);

        //Assert
        Assert.NotNull(result);
    }
}
