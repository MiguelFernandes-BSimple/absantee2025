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
        collab.Setup(a => a.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        var holidayPeriod = new Mock<IHolidayPeriod>();
        DateOnly date = new DateOnly(2020, 1, 1);
        holidayPeriod.Setup(a => a.ContainsDate(date)).Returns(true);

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
        collab.Setup(a => a.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        var holidayPeriod = new Mock<IHolidayPeriod>();
        DateOnly date = new DateOnly(2020, 1, 1);
        holidayPeriod.Setup(a => a.ContainsDate(date)).Returns(false);

        var holidayPlan = new HolidayPlan(holidayPeriod.Object, collab.Object);

        //act
        var result = holidayPlan.GetHolidayPeriodContainingDate(date);

        //assert
        Assert.Null(result);
    }
}