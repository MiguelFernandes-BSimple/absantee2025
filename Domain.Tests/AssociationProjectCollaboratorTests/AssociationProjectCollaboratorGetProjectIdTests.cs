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
        PeriodDate periodDate = new PeriodDate(It.IsAny<DateOnly>(), It.IsAny<DateOnly>());
        long collabId = 1;
        long projectId = 1;

        AssociationProjectCollaborator assoc =
            new AssociationProjectCollaborator(collabId, projectId, periodDate);

        // Act
        var result = assoc.GetProjectId();

        // Assert
        Assert.Equal(projectId, result);
    }
}