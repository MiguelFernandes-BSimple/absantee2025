using Infrastructure.Repositories;
using Domain.Interfaces;
using Moq;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class GetHolidayPeriodContainingDate
{


    [Fact]
    public void WhenGivenCollaboratorAndGoodDate_ThenReturnPeriod()
    {
        //arrange
        DateOnly date = new DateOnly(2020, 1, 1);
        var collab = new Mock<ICollaborator>();

        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(collab.Object)).Returns(true);
        holidayPlan.Setup(a => a.GetHolidayPeriodContainingDate(date)).Returns(holidayPeriod.Object);

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        //act
        var result = hpRepo.GetHolidayPeriodContainingDate(collab.Object, date);

        //assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenGivenCollaboratorAndBadDate_ThenReturnEmpty()
    {
        //arrange
        DateOnly date = new DateOnly(2020, 1, 1);
        var collab = new Mock<ICollaborator>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(collab.Object)).Returns(true);
        holidayPlan.Setup(a => a.GetHolidayPeriodContainingDate(date)).Returns((IHolidayPeriod?)null);

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        //act
        var result = hpRepo.GetHolidayPeriodContainingDate(collab.Object, date);

        //assert
        Assert.Null(result);
    }
}