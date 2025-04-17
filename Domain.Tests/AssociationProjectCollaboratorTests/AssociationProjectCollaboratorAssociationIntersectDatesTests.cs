using Moq;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Tests.AssociationProjectCollaboratorTests;

public class AssociationProjectCollaboratorAssociationIntersectDatesTests
{

    [Fact]
    public void WhenAssociationIntersectDatesReceivesIntersectingDate_ThenReturnTrue()
    {
        // Arrange
        var periodDate = new PeriodDate(new DateOnly(2024, 10, 10), new DateOnly(2024, 10, 20));
        long collabId = 1;
        long projectId = 1;

        // PeriodDAte to intersect
        var periodDateToIntersect = new PeriodDate(new DateOnly(2024, 10, 15), new DateOnly(2024, 10, 25));

        AssociationProjectCollaborator assoc =
            new AssociationProjectCollaborator(collabId, projectId, periodDate);

        //Act
        bool result = assoc.AssociationIntersectPeriod(periodDateToIntersect);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void WhenAssociationIntersectDatesReceivesNotIntersectingDate_ThenReturnFalse()
    {
        // Arrange
        // Association parameters
        var periodDate = new PeriodDate(new DateOnly(2024, 10, 10), new DateOnly(2024, 10, 20));
        long collabId = 1;
        long projectId = 1;

        // PeriodDAte to intersect
        var periodDateToIntersect = new PeriodDate(new DateOnly(2024, 10, 25), new DateOnly(2024, 10, 29));

        AssociationProjectCollaborator assoc =
            new AssociationProjectCollaborator(collabId, projectId, periodDate);

        //Act
        bool result = assoc.AssociationIntersectPeriod(periodDateToIntersect);

        //Assert
        Assert.False(result);
    }
}
