using Domain;
using Moq;


public class FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates{


     [Fact]
    public void WhenGivenBadCollaboratorAndDatesAndLength_ThenReturnEmptyLists()
    {
        //arrange
        DateOnly ini = new DateOnly(2020, 1, 1);
        DateOnly end = new DateOnly(2020, 3, 1);
        var collab = new Mock<ICollaborator>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(collab.Object)).Returns(true);

        IEnumerable<IHolidayPeriod> ret = new List<IHolidayPeriod>();
        holidayPlan.Setup(a => a.FindAllHolidayPeriodsBetweenDatesLongerThan(ini, end, 4)).Returns(ret);

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        //act
        var result = hpRepo.FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(collab.Object, ini, end, 4);

        //assert
        Assert.Empty(result);
    }

     [Fact]
    public void WhenGivenGoodCollaboratorAndDatesAndLength_ThenReturnPeriods()
    {
        //arrange
        DateOnly ini = new DateOnly(2020, 1, 1);
        DateOnly end = new DateOnly(2020, 3, 1);
        var collab = new Mock<ICollaborator>();

        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(collab.Object)).Returns(true);

        IEnumerable<IHolidayPeriod> ret = new List<IHolidayPeriod>() { holidayPeriod.Object };
        holidayPlan.Setup(a => a.FindAllHolidayPeriodsBetweenDatesLongerThan(ini, end, 4)).Returns(ret);

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        //act
        var result = hpRepo.FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(collab.Object, ini, end, 4);

        //assert
        Assert.Equal(ret, result);
    }
}