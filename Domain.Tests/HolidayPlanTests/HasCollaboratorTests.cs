using Domain;
using Moq;

public class HasCollaboratorTests
{
    [Fact]
    public void WhenSameCollaborator_ReturnsTrue()
    {
        var collaborator = new Mock<ICollaborator>();
        var holidayPeriod = new Mock<IHolidayPeriod>();

        collaborator
            .Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);

        holidayPeriod
            .Setup(hp => hp.Contains(It.IsAny<IHolidayPeriod>()))
            .Returns(false);
        holidayPeriod.Setup(hp => hp.GetInitDate()).Returns(new DateOnly(2025, 1, 1));
        holidayPeriod.Setup(hp => hp.GetFinalDate()).Returns(new DateOnly(2025, 1, 10));

        var holidayPlan = new HolidayPlan(holidayPeriod.Object, collaborator.Object);

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

        collaborator1
            .Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);

        holidayPeriod
            .Setup(hp => hp.Contains(It.IsAny<IHolidayPeriod>()))
            .Returns(false);
        holidayPeriod.Setup(hp => hp.GetInitDate()).Returns(new DateOnly(2025, 1, 1));
        holidayPeriod.Setup(hp => hp.GetFinalDate()).Returns(new DateOnly(2025, 1, 10));

        var holidayPlan = new HolidayPlan(holidayPeriod.Object, collaborator1.Object);

        // Act
        var result = holidayPlan.HasCollaborator(collaborator2.Object);

        // Assert
        Assert.False(result);
    }
}