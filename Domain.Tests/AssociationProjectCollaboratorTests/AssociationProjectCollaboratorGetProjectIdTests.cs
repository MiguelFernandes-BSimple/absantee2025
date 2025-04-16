using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.AssociationProjectCollaboratorTests;

public class AssociationProjectCollaboratorGetProjectIdTests
{

    [Fact]
    public void WhenAssociationIsInstatiated_ThenReturnProjectId()
    {
        // Arrange
        // Association parameters
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();
        long collabId = 1;
        long projectId = 1;

        AssociationProjectCollaborator assoc =
            new AssociationProjectCollaborator(collabId, projectId, periodDate.Object);

        // Act
        var result = assoc.GetProjectId();

        // Assert
        Assert.Equal(projectId, result);
    }
}