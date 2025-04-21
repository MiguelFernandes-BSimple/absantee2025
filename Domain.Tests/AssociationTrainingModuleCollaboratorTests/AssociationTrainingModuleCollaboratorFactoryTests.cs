using Domain.Models;
using Domain.Interfaces;
using Domain.Factory;

namespace Domain.Tests.TrainingModuleTests;

public class AssociationTrainingModuleCollaboratorFactoryTests {
    [Fact]
    public async Task WhenReceivesGoodArguments_ThenObjectIsInstantiated() {
        // Arrange
        var amcFactory = new AssociationTrainingModuleCollaboratorFactory();

        // Act
        await amcFactory.Create(1, 1);

        // Assert
    }
}
