using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.AssociationProjectCollaboratorTests;

public class AssociationProjectCollaboratorGetPeriodDateTests
{

    [Fact]
    public void WhenAssociationIsInstatiated_ThenReturnPeriodDate()
    {
        // Arrange
        // Association parameters
        Mock<PeriodDate> periodDate = new Mock<PeriodDate>();
        long collabId = 1;
        long projectId = 1;

        AssociationProjectCollaborator assoc =
            new AssociationProjectCollaborator(collabId, projectId, periodDate.Object);

        // Act
        var result = assoc.GetPeriodDate();

        // Assert
        Assert.Equal(periodDate.Object, result);
    }
}