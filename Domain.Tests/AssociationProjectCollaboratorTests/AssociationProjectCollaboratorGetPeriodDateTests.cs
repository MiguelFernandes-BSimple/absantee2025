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
        PeriodDate periodDate = new PeriodDate(It.IsAny<DateOnly>(), It.IsAny<DateOnly>());
        Guid collabId = new Guid();
        Guid projectId = new Guid();

        AssociationProjectCollaborator assoc =
            new AssociationProjectCollaborator(collabId, projectId, periodDate);

        // Act
        var result = assoc.PeriodDate;

        // Assert
        Assert.Equal(periodDate, result);
    }
}