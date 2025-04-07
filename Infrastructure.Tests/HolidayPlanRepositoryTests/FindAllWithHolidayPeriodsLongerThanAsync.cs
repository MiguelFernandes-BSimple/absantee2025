using Infrastructure.Repositories;
using Domain.Interfaces;
using Domain.IRepository;
using Moq;
using Domain.IRepository;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class FindAllWithHolidayPeriodsLongerThanAsync
{
    [Fact]
    public async Task WhenFindingHolidayPlansWithPeriodsLongerThanAsync_ReturnsCorrectList()
    {
        //arrange
        int days = 5;

        Mock<ICollaborator> doubleCollab1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollab2 = new Mock<ICollaborator>();

        Mock<IHolidayPlan> holidayPlanDouble1 = new Mock<IHolidayPlan>();
        holidayPlanDouble1.Setup(p => p.HasPeriodLongerThan(days)).Returns(true);
        holidayPlanDouble1.Setup(p => p.GetCollaborator()).Returns(doubleCollab1.Object);

        Mock<IHolidayPlan> holidayPlanDouble2 = new Mock<IHolidayPlan>();
        holidayPlanDouble2.Setup(p => p.HasPeriodLongerThan(days)).Returns(false);
        holidayPlanDouble2.Setup(p => p.GetCollaborator()).Returns(doubleCollab2.Object);

        IHolidayPlanRepository holidayPlanRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble1.Object, holidayPlanDouble2.Object });

        //act
        var result = await holidayPlanRepo.FindAllWithHolidayPeriodsLongerThanAsync(days);

        //assert
        Assert.Contains(holidayPlanDouble1.Object, result);
        Assert.DoesNotContain(holidayPlanDouble2.Object, result);
    }
}