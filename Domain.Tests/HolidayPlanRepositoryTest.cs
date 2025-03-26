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

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(colab.Object)).Returns(true);
        holidayPlan.Setup(a => a.GetHolidayPeriodContainingDate(date)).Returns(holidayPeriod.Object);

        var hpRepo = new HolidayPlanRepository(holidayPlan.Object);

        //act
        var result = hpRepo.GetHolidayPeriodContainingDate(colab.Object, date);

        //assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenGivenCollaboratorAndBadDate_ThenReturnEmpty() {
        //arrange
        DateOnly date = DateOnly.FromDateTime(DateTime.Parse("1 Jan 2020"));
        var colab = new Mock<IColaborator>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(colab.Object)).Returns(true);
        holidayPlan.Setup(a => a.GetHolidayPeriodContainingDate(date)).Returns((IHolidayPeriod?)null);

        var hpRepo = new HolidayPlanRepository(holidayPlan.Object);

        //act
        var result = hpRepo.GetHolidayPeriodContainingDate(colab.Object, date);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void WhenGivenGoodCollaboratorAndDatesAndLength_ThenReturnPeriods() {
        //arrange
        DateOnly ini = DateOnly.FromDateTime(DateTime.Parse("1 Jan 2020"));
        DateOnly end = DateOnly.FromDateTime(DateTime.Parse("1 Mar 2020"));
        var colab = new Mock<IColaborator>();

        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(colab.Object)).Returns(true);

        IEnumerable<IHolidayPeriod> ret = new List<IHolidayPeriod>() {holidayPeriod.Object};
        holidayPlan.Setup(a => a.FindAllHolidayPeriodsBetweenDatesLongerThan(ini, end, 4)).Returns(ret);

        var hpRepo = new HolidayPlanRepository(holidayPlan.Object);

        //act
        var result = hpRepo.FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(colab.Object, ini, end, 4);

        //assert
        Assert.Equal(ret, result);
    }

    [Fact]
    public void WhenGivenBadCollaboratorAndDatesAndLength_ThenReturnPeriods() {
        //arrange
        DateOnly ini = DateOnly.FromDateTime(DateTime.Parse("1 Jan 2020"));
        DateOnly end = DateOnly.FromDateTime(DateTime.Parse("1 Mar 2020"));
        var colab = new Mock<IColaborator>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(colab.Object)).Returns(true);

        IEnumerable<IHolidayPeriod> ret = new List<IHolidayPeriod>();
        holidayPlan.Setup(a => a.FindAllHolidayPeriodsBetweenDatesLongerThan(ini, end, 4)).Returns(ret);

        var hpRepo = new HolidayPlanRepository(holidayPlan.Object);

        //act
        var result = hpRepo.FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(colab.Object, ini, end, 4);

        //assert
        Assert.Empty(result);
    }
}