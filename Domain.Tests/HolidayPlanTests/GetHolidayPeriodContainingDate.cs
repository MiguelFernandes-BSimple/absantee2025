using Domain.Models;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.HolidayPlanTests;

public class GetHolidayPeriodContainingDate
{
    [Fact]
    public void WhenGivenCorrectDate_ThenReturnPeriod()
    {
        //arrange
        var collab = new Mock<ICollaborator>();
        collab.Setup(a => a.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        var holidayPeriod = new Mock<IHolidayPeriod>();
        DateOnly date = new DateOnly(2020, 1, 1);
        holidayPeriod.Setup(a => a.ContainsDate(date)).Returns(true);

        var periodDate1 = new Mock<IPeriodDate>();
        periodDate1.Setup(pd => pd.GetInitDate()).Returns(It.IsAny<DateOnly>());
        periodDate1.Setup(pd => pd.GetFinalDate()).Returns(It.IsAny<DateOnly>());
        holidayPeriod.Setup(hp => hp.GetPeriodDate()).Returns(periodDate1.Object);


        var holidayPlan = new HolidayPlan(holidayPeriod.Object, collab.Object);

        //act
        var result = holidayPlan.GetHolidayPeriodContainingDate(date);

        //assert
        Assert.Equal(holidayPeriod.Object, result);
    }

    [Fact]
    public void WhenGivenIncorrectDate_ThenReturnNull()
    {
        //arrange
        var collab = new Mock<ICollaborator>();
        collab.Setup(a => a.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        var holidayPeriod = new Mock<IHolidayPeriod>();
        DateOnly date = new DateOnly(2020, 1, 1);
        holidayPeriod.Setup(a => a.ContainsDate(date)).Returns(false);

        var periodDate1 = new Mock<IPeriodDate>();
        periodDate1.Setup(pd => pd.GetInitDate()).Returns(It.IsAny<DateOnly>());
        periodDate1.Setup(pd => pd.GetFinalDate()).Returns(It.IsAny<DateOnly>());
        holidayPeriod.Setup(hp => hp.GetPeriodDate()).Returns(periodDate1.Object);

        var holidayPlan = new HolidayPlan(holidayPeriod.Object, collab.Object);

        //act
        var result = holidayPlan.GetHolidayPeriodContainingDate(date);

        //assert
        Assert.Null(result);
    }
}