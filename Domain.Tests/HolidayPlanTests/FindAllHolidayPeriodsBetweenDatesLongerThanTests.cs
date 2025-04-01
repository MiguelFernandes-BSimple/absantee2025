using Domain;
using Moq;

public class FindAllHolidayPeriodsBetweenDatesLongerThanTests
{
    [Fact]
    public void WhenGivenValidDatesAndLength_ThenReturnPeriods()
    {
        //arrange
        var collab = new Mock<ICollaborator>();
        DateOnly ini = new DateOnly(2020, 1, 1);
        DateOnly end = ini.AddDays(5);
        collab.Setup(a => a.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);
        DateOnly ini2 = ini.AddMonths(1);
        DateOnly end2 = end.AddMonths(1);

        var holidayPeriod1 = new Mock<IHolidayPeriod>();
        holidayPeriod1.Setup(a => a.ContainedBetween(ini, end)).Returns(true);
        holidayPeriod1.Setup(a => a.GetDuration()).Returns(5);

        var holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(a => a.ContainedBetween(ini2, end2)).Returns(false);
        holidayPeriod2.Setup(a => a.GetDuration()).Returns(5);

        var holidayPeriod3 = new Mock<IHolidayPeriod>();
        holidayPeriod3.Setup(a => a.ContainedBetween(ini, ini)).Returns(true);
        holidayPeriod3.Setup(a => a.GetDuration()).Returns(4);

        var holidayPeriod4 = new Mock<IHolidayPeriod>();
        holidayPeriod4.Setup(a => a.ContainedBetween(ini, ini)).Returns(true);
        holidayPeriod4.Setup(a => a.GetDuration()).Returns(3);

        var holidayPeriods = new List<IHolidayPeriod> { holidayPeriod1.Object, holidayPeriod2.Object, holidayPeriod3.Object, holidayPeriod4.Object };
        HolidayPlan holidayPlan = new HolidayPlan(holidayPeriods, collab.Object);

        //act
        var result = holidayPlan.FindAllHolidayPeriodsBetweenDatesLongerThan(ini, end, 4);

        //assert
        Assert.Single(result);
    }
}