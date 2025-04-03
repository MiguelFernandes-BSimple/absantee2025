using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPeriodTests;

public class ContainsWeekend
{
    [Fact]
    public void WhenPassingPeriodThatContainsWeekend_ThenReturnTrue()
    {
        //arrange
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();
        periodDate.Setup(pd => pd.ContainsWeekend()).Returns(true);

        HolidayPeriod holidayPlan = new HolidayPeriod(periodDate.Object);

        //act
        bool result = holidayPlan.ContainsWeekend();

        //assert
        Assert.True(result);
    }

    [Fact]
    public void WhenPassingPeriodThatDontContainWeekend_ThenReturnFalse()
    {
        //arrange
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();
        periodDate.Setup(pd => pd.ContainsWeekend()).Returns(false);

        HolidayPeriod holidayPlan = new HolidayPeriod(periodDate.Object);

        //act
        bool result = holidayPlan.ContainsWeekend();

        //assert
        Assert.False(result);
    }
}