using Domain.Models;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.HolidayPlanTests;

public class HolidayPlanConstructorTests
{
    [Fact]
    public void WhenPassingValidSinglePeriod_ThenHolidayPlanIsCreated()
    {
        // arrange
        var periodDouble = new Mock<IHolidayPeriod>();
        var periodList = new List<IHolidayPeriod> { periodDouble.Object };

        // act
        new HolidayPlan(It.IsAny<Guid>(), periodList);

        // assert
    } 

    // Happy Path - Testing constructor with Single HolidayPeriod
    // Can Instatiate a HolidayPlan Object successfuly
    [Fact]
    public void WhenPassingValidMultiplePeriods_ThenHolidayPlanIsCreated()
    {
        // Arrange
        // Test doubles for Holiday Period
        var holidayPeriodDouble1 = new Mock<IHolidayPeriod>();

        holidayPeriodDouble1.Setup(hp => hp.PeriodDate).Returns(It.IsAny<PeriodDate>());

        var holidayPeriodDouble2 = new Mock<IHolidayPeriod>();
        holidayPeriodDouble2.Setup(hp => hp.PeriodDate).Returns(It.IsAny<PeriodDate>());

        var holidayPeriodDouble3 = new Mock<IHolidayPeriod>();
        holidayPeriodDouble3.Setup(hp => hp.PeriodDate).Returns(It.IsAny<PeriodDate>());

        // Can't overlap with any other holiday periods
        holidayPeriodDouble1
            .Setup(hp1 => hp1.Contains(It.IsAny<IHolidayPeriod>()))
            .Returns(false);
        holidayPeriodDouble2
            .Setup(hp2 => hp2.Contains(It.IsAny<IHolidayPeriod>()))
            .Returns(false);
        holidayPeriodDouble3
            .Setup(hp3 => hp3.Contains(It.IsAny<IHolidayPeriod>()))
            .Returns(false);

        // Create Holiday Periods list
        var holidayPeriods = new List<IHolidayPeriod>
        {
            holidayPeriodDouble1.Object,
            holidayPeriodDouble2.Object,
            holidayPeriodDouble3.Object,
        };

        // Act
        HolidayPlan holidayPlan = new HolidayPlan(It.IsAny<Guid>(), holidayPeriods);

        // Assert
    }

}