using Domain.Models;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.HolidayPlanTests;

public class GetDurationInDays
{

    [Fact]
    public void WhenCalculatingHolidayDuration_ThenReturnsCorrectValue()
    {
        // Arrange
        var collaboratorMock = new Mock<ICollaborator>();
        var holidayPeriod1Mock = new Mock<IHolidayPeriod>();
        var holidayPeriod2Mock = new Mock<IHolidayPeriod>();
        var periodDateMock1 = new Mock<IPeriodDate>();
        var periodDateMock2 = new Mock<IPeriodDate>();
        var periodDateMock3 = new Mock<IPeriodDate>();

        collaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        holidayPeriod1Mock.Setup(hp => hp.GetPeriodDate()).Returns(periodDateMock1.Object);
        holidayPeriod1Mock.Setup(hp => hp.GetInterceptionDurationInDays(It.IsAny<IPeriodDate>())).Returns(5);

        holidayPeriod2Mock.Setup(hp => hp.GetPeriodDate()).Returns(periodDateMock2.Object);
        holidayPeriod2Mock.Setup(hp => hp.GetInterceptionDurationInDays(It.IsAny<IPeriodDate>())).Returns(7);

        var holidayPlan = new HolidayPlan(new List<IHolidayPeriod> { holidayPeriod1Mock.Object, holidayPeriod2Mock.Object }, collaboratorMock.Object);

        // Act
        int totalDays = holidayPlan.GetDurationInDays(periodDateMock3.Object);

        // Assert
        Assert.Equal(12, totalDays);
    }

}