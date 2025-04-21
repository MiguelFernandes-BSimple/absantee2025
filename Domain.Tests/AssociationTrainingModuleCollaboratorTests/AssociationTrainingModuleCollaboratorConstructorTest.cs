using Domain.Models;
using Moq;

namespace Domain.Tests.AssociationTrainingModuleCollaboratorTests;

public class AssociationTrainingModuleCollaboratorConstructorTests
{
    [Fact]
    public void WhenPassingArguments_ThenClassIsInstatiated()
    {
        // Arrange
        PeriodDateTime periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

        // Act
        var result = new AssociationTrainingModuleCollaborator(It.IsAny<long>(), It.IsAny<long>(), periodDateTime);

        // Assert
        Assert.NotNull(result);
    }
}