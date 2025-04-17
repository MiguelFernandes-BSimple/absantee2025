using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.AssociationProjectCollaboratorTests;

public class AssociationProjectCollaboratorGetCollaboratorIdTests
{

    [Fact]
    public void WhenAssociationIsInstatiated_ThenReturnCollaboratorId()
    {
        // Arrange
        // Association parameters
        PeriodDate periodDate = new PeriodDate(It.IsAny<DateOnly>(), It.IsAny<DateOnly>());
        long collabId = 1;
        long projectId = 1;

        AssociationProjectCollaborator assoc =
            new AssociationProjectCollaborator(collabId, projectId, periodDate);

        // Act
        var result = assoc.GetCollaboratorId();

        // Assert
        Assert.Equal(collabId, result);
    }
}