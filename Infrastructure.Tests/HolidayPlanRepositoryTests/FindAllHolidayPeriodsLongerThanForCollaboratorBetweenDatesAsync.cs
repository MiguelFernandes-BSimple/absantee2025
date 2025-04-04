using Infrastructure.Repositories;
using Domain.Interfaces;
using Moq;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync
{
    [Fact]
    public async Task WhenGivenBadCollaboratorAndDatesAndLengthAsync_ThenReturnEmptyLists()
    {
        //arrange
        var collab = new Mock<ICollaborator>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(collab.Object)).Returns(true);

        IEnumerable<IHolidayPeriod> ret = new List<IHolidayPeriod>();
        holidayPlan.Setup(a => a.FindAllHolidayPeriodsBetweenDatesLongerThan(It.IsAny<IPeriodDate>(), 4)).Returns(ret);

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        //act
        var result = await hpRepo.FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(collab.Object, It.IsAny<IPeriodDate>(), 4);

        //assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task WhenGivenGoodCollaboratorAndDatesAndLengthAsync_ThenReturnPeriods()
    {
        //arrange
        var collab = new Mock<ICollaborator>();

        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(collab.Object)).Returns(true);

        IEnumerable<IHolidayPeriod> ret = new List<IHolidayPeriod>() { holidayPeriod.Object };
        holidayPlan.Setup(a => a.FindAllHolidayPeriodsBetweenDatesLongerThan(It.IsAny<IPeriodDate>(), 4)).Returns(ret);

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        //act
        var result = await hpRepo.FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(collab.Object, It.IsAny<IPeriodDate>(), 4);

        //assert
        Assert.Equal(ret, result);
    }
}