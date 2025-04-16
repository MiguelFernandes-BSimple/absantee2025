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
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();
        long collabId = 1;
        long projectId = 1;

        // Id to change into
        long newAssocId = 2;

        AssociationProjectCollaborator assoc =
            new AssociationProjectCollaborator(collabId, projectId, periodDate.Object);

        // Act
        assoc.SetId(newAssocId);

        // Assert
        Assert.Equal(newAssocId, assoc.GetId());
    }
}