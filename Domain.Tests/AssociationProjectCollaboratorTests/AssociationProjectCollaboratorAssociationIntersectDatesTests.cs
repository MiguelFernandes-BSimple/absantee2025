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
        // Association parameters
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();
        long collabId = 1;
        long projectId = 1;

        // PeriodDAte to intersect
        Mock<IPeriodDate> periodDateToIntersect = new Mock<IPeriodDate>();

        AssociationProjectCollaborator assoc =
            new AssociationProjectCollaborator(collabId, projectId, periodDate.Object);

        periodDate.Setup(pd => pd.Intersects(periodDateToIntersect.Object)).Returns(true);

        //Act
        bool result = assoc.AssociationIntersectPeriod(periodDateToIntersect.Object);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void WhenAssociationIntersectDatesReceivesNotIntersectingDate_ThenReturnFalse()
    {
        // Arrange
        // Association parameters
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();
        long collabId = 1;
        long projectId = 1;

        // PeriodDAte to intersect
        Mock<IPeriodDate> periodDateToIntersect = new Mock<IPeriodDate>();

        AssociationProjectCollaborator assoc =
            new AssociationProjectCollaborator(collabId, projectId, periodDate.Object);

        periodDate.Setup(pd => pd.Intersects(periodDateToIntersect.Object)).Returns(false);

        //Act
        bool result = assoc.AssociationIntersectPeriod(periodDateToIntersect.Object);

        //Assert
        Assert.False(result);
    }
}
