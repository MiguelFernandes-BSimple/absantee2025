using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.AssociationProjectCollaboratorTests;

public class AssociationProjectCollaboratorSetIdTests
{

    [Fact]
    public void WhenAssociationIsInstatiated_ThenAlterAssociationId()
    {
        // Arrange
        // Association parameters
        PeriodDate periodDate = new PeriodDate(It.IsAny<DateOnly>(), It.IsAny<DateOnly>());
        long collabId = 1;
        long projectId = 1;

        // Id to change into
        long newAssocId = 2;

        AssociationProjectCollaborator assoc =
            new AssociationProjectCollaborator(collabId, projectId, periodDate);

        // Act
        assoc.SetId(newAssocId);

        // Assert
        Assert.Equal(newAssocId, assoc.GetId());
    }
}