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
        Guid collabId = new Guid();
        Guid projectId = new Guid();

        AssociationProjectCollaborator assoc =
            new AssociationProjectCollaborator(collabId, projectId, periodDate);

        // Act
        var result = assoc.ProjectId;

        // Assert
        Assert.Equal(projectId, result);
    }
}