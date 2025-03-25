using Domain;
using Moq;

namespace Domain.Tests;

public class HolidayPlanRepositoryTest
{

    [Fact]
    public void WhenGivenCollaboratorAndGoodDate_ThenReturnPeriod() {
        //arrange
        DateOnly date = DateOnly.FromDateTime(DateTime.Parse("1 Jan 2020"));
        var colab = new Mock<IColaborator>();
        
        var holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(a => a.ContainsDate(date)).Returns(true);

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(colab.Object)).Returns(true);
        holidayPlan.Setup(a => a.GetHolidayPeriodContainingDate(date)).Returns(holidayPeriod.Object);

        var hpRepo = new HolidayPlanRepository(holidayPlan.Object);

        //act
        var result = hpRepo.GetHolidayPeriodContainingDate(colab.Object, date);
        var count = result.Count();

        //assert
        Assert.Single(result);
    }
}