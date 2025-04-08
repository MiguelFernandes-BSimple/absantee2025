using Domain.Models;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.HolidayPlanTests;

public class HasCollaborator
{
    [Fact]
    public void WhenSameCollaborator_ReturnsTrue()
    {
        var collaborator = new Mock<ICollaborator>();
        var holidayPeriod = new Mock<IHolidayPeriod>();
        var periodDateDouble = new Mock<IPeriodDate>();

        collaborator.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        holidayPeriod
            .Setup(hp => hp.Contains(It.IsAny<IHolidayPeriod>()))
            .Returns(false);

        holidayPeriod.Setup(hp => hp.GetPeriodDate()).Returns(periodDateDouble.Object);

        collaborator.Setup(c => c.Equals(collaborator.Object)).Returns(true);

        var holidayPlan = new HolidayPlan(collaborator.Object, holidayPeriod.Object);

        // Act
        var result = holidayPlan.HasCollaborator(collaborator.Object);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void WhenDifferentCollaborator_ReturnsFalse()
    {
        // Arrange
        var collaborator1 = new Mock<ICollaborator>();
        var collaborator2 = new Mock<ICollaborator>();
        var holidayPeriod = new Mock<IHolidayPeriod>();
        var periodDateDouble = new Mock<IPeriodDate>();

        collaborator1.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);


        holidayPeriod
            .Setup(hp => hp.Contains(It.IsAny<IHolidayPeriod>()))
            .Returns(false);

        holidayPeriod.Setup(hp => hp.GetPeriodDate()).Returns(periodDateDouble.Object);

        var holidayPlan = new HolidayPlan(collaborator1.Object, holidayPeriod.Object);

        // Act
        var result = holidayPlan.HasCollaborator(collaborator2.Object);

        // Assert
        Assert.False(result);
    }
}