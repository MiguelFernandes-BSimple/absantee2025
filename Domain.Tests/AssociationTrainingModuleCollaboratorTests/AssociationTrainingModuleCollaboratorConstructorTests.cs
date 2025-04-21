using Domain.Models;
using Domain.Interfaces;

namespace Domain.Tests.TrainingModuleTests;

public class AssociationTrainingModuleCollaboratorConstructorTests {
    [Fact]
    public void WhenReceivesGoodArguments_ThenObjectIsInstantiated() {
        // Arrange

        // Act
        new AssociationTrainingModuleCollaborator(1, 1);

        // Assert
    }
}
