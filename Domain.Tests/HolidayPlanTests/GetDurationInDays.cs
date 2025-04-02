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
        var colaboratorMock = new Mock<ICollaborator>();
        var holidayPeriod1Mock = new Mock<IHolidayPeriod>();
        var holidayPeriod2Mock = new Mock<IHolidayPeriod>();
        var periodDateMock1 = new Mock<IPeriodDate>();
        var periodDateMock2 = new Mock<IPeriodDate>();
        var periodDateMock3 = new Mock<IPeriodDate>();

        colaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        periodDateMock1.Setup(pd => pd.GetInitDate()).Returns(new DateOnly(2024, 6, 1));
        periodDateMock1.Setup(pd => pd.GetFinalDate()).Returns(new DateOnly(2024, 6, 5));
        holidayPeriod1Mock.Setup(hp => hp.GetPeriodDate()).Returns(periodDateMock1.Object);
        holidayPeriod1Mock.Setup(hp => hp.GetInterceptionDurationInDays(It.IsAny<IPeriodDate>())).Returns(5);

        periodDateMock2.Setup(pd => pd.GetInitDate()).Returns(new DateOnly(2024, 7, 1));
        periodDateMock2.Setup(pd => pd.GetFinalDate()).Returns(new DateOnly(2024, 7, 7));
        holidayPeriod2Mock.Setup(hp => hp.GetPeriodDate()).Returns(periodDateMock2.Object);
        holidayPeriod2Mock.Setup(hp => hp.GetInterceptionDurationInDays(It.IsAny<IPeriodDate>())).Returns(7);

        periodDateMock3.Setup(pd => pd.GetInitDate()).Returns(new DateOnly(2024, 6, 1));
        periodDateMock3.Setup(pd => pd.GetFinalDate()).Returns(new DateOnly(2024, 7, 31));

        var holidayPlan = new HolidayPlan(new List<IHolidayPeriod> { holidayPeriod1Mock.Object, holidayPeriod2Mock.Object }, colaboratorMock.Object);

        // Act
        int totalDays = holidayPlan.GetDurationInDays(periodDateMock3.Object);

        // Assert
        Assert.Equal(12, totalDays);
    }

}