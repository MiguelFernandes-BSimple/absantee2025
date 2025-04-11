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

        var periodDate1 = new Mock<IPeriodDate>();

        var periodDate2 = new Mock<IPeriodDate>();

        var periodDate3 = new Mock<IPeriodDate>();

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
        HolidayPlan holidayPlan = new HolidayPlan(It.IsAny<long>(), holidayPeriods);

        var expected = new List<IHolidayPeriod>() { holidayPeriod1.Object };
        //act
        var result = holidayPlan.FindAllHolidayPeriodsBetweenDatesLongerThan(periodDate1.Object, 4);

        //assert
        Assert.True(expected.SequenceEqual(result));
    }
}