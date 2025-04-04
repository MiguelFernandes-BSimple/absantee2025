using Domain.Models;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.HolidayPlanTests;

public class FindAllHolidayPeriodsBetweenDatesLongerThan
{
    [Fact]
    public void WhenGivenValidDatesAndLength_ThenReturnPeriods()
    {
        //arrange
        var collab = new Mock<ICollaborator>();
        collab.Setup(a => a.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        DateOnly ini = new DateOnly(2020, 1, 1);
        DateOnly end = ini.AddDays(5);
        var periodDate1 = new Mock<IPeriodDate>();
        periodDate1.Setup(pd => pd.GetInitDate()).Returns(ini);
        periodDate1.Setup(pd => pd.GetFinalDate()).Returns(end);

        DateOnly ini2 = ini.AddMonths(1);
        DateOnly end2 = end.AddMonths(1);
        var periodDate2 = new Mock<IPeriodDate>();
        periodDate2.Setup(pd => pd.GetInitDate()).Returns(ini2);
        periodDate2.Setup(pd => pd.GetFinalDate()).Returns(end2);

        var periodDate3 = new Mock<IPeriodDate>();
        periodDate3.Setup(pd => pd.GetInitDate()).Returns(ini);
        periodDate3.Setup(pd => pd.GetFinalDate()).Returns(ini);

        var holidayPeriod1 = new Mock<IHolidayPeriod>();
        holidayPeriod1.Setup(hp => hp.GetPeriodDate()).Returns(periodDate1.Object);
        holidayPeriod1.Setup(hp => hp.Contains(periodDate1.Object)).Returns(true);
        holidayPeriod1.Setup(hp => hp.GetDuration()).Returns(5);

        var holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(hp => hp.GetPeriodDate()).Returns(periodDate2.Object);
        holidayPeriod2.Setup(hp => hp.Contains(periodDate1.Object)).Returns(false);
        holidayPeriod2.Setup(a => a.GetDuration()).Returns(5);

        var holidayPeriod3 = new Mock<IHolidayPeriod>();
        holidayPeriod3.Setup(hp => hp.GetPeriodDate()).Returns(periodDate1.Object);
        holidayPeriod3.Setup(hp => hp.Contains(periodDate1.Object)).Returns(true);
        holidayPeriod3.Setup(a => a.GetDuration()).Returns(4);

        var holidayPeriod4 = new Mock<IHolidayPeriod>();
        holidayPeriod4.Setup(hp => hp.GetPeriodDate()).Returns(periodDate1.Object);
        holidayPeriod4.Setup(hp => hp.Contains(periodDate1.Object)).Returns(true);
        holidayPeriod4.Setup(a => a.GetDuration()).Returns(3);

        var holidayPeriods = new List<IHolidayPeriod> { holidayPeriod1.Object, holidayPeriod2.Object, holidayPeriod3.Object, holidayPeriod4.Object };
        HolidayPlan holidayPlan = new HolidayPlan(holidayPeriods, collab.Object);

        var expected = new List<IHolidayPeriod>() { holidayPeriod1.Object };
        //act
        var result = holidayPlan.FindAllHolidayPeriodsBetweenDatesLongerThan(periodDate1.Object, 4);

        //assert
        Assert.True(expected.SequenceEqual(result));
    }
}