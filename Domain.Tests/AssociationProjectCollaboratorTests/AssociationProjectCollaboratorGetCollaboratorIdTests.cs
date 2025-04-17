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
        Mock<PeriodDate> periodDate = new Mock<PeriodDate>();
        long collabId = 1;
        long projectId = 1;

        AssociationProjectCollaborator assoc =
            new AssociationProjectCollaborator(collabId, projectId, periodDate.Object);

        // Act
        var result = assoc.GetCollaboratorId();

        // Assert
        Assert.Equal(collabId, result);
    }
}