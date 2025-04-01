using Domain.Models;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.HolidayPlanTests;

public class GetDurationInDaysTests
{

    [Fact]
    public void WhenCalculatingHolidayDuration_ThenReturnsCorrectValue()
    {
        // Arrange
        var colaboratorMock = new Mock<ICollaborator>();
        var holidayPeriod1Mock = new Mock<IHolidayPeriod>();
        var holidayPeriod2Mock = new Mock<IHolidayPeriod>();

        colaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);


        holidayPeriod1Mock.Setup(hp => hp.GetInitDate()).Returns(new DateOnly(2024, 6, 1));
        holidayPeriod1Mock.Setup(hp => hp.GetFinalDate()).Returns(new DateOnly(2024, 6, 5));
        holidayPeriod1Mock.Setup(hp => hp.GetDurationInDays(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(5);

        holidayPeriod2Mock.Setup(hp => hp.GetInitDate()).Returns(new DateOnly(2024, 7, 1));
        holidayPeriod2Mock.Setup(hp => hp.GetFinalDate()).Returns(new DateOnly(2024, 7, 7));
        holidayPeriod2Mock.Setup(hp => hp.GetDurationInDays(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(7);

        var holidayPlan = new HolidayPlan(new List<IHolidayPeriod> { holidayPeriod1Mock.Object, holidayPeriod2Mock.Object }, colaboratorMock.Object);

        // Act
        int totalDays = holidayPlan.GetDurationInDays(new DateOnly(2024, 6, 1), new DateOnly(2024, 7, 31));

        // Assert
        Assert.Equal(12, totalDays);
    }

}