using Domain;
using Moq;

namespace Domain.Tests;

public class HolidayPlanRepositoryTest
{
    [Fact]
    public void WhenFindingALlCollaboratorsWithHolidayPeriodsLongerThan_ThenShouldReturnCorrectCollaborators()
    {
        //arrange
        int days = 5;

        Mock<IColaborator> colaboratorDouble1 = new Mock<IColaborator>();
        Mock<IColaborator> colaboratorDouble2 = new Mock<IColaborator>();

        Mock<IHolidayPlan> holidayPlanDouble1 = new Mock<IHolidayPlan>();
        holidayPlanDouble1.Setup(p => p.HasPeriodLongerThan(days)).Returns(true);
        holidayPlanDouble1.Setup(p => p.GetColaborator()).Returns(colaboratorDouble1.Object);

        Mock<IHolidayPlan> holidayPlanDouble2 = new Mock<IHolidayPlan>();
        holidayPlanDouble2.Setup(p => p.HasPeriodLongerThan(days)).Returns(false);
        holidayPlanDouble2.Setup(p => p.GetColaborator()).Returns(colaboratorDouble2.Object);

        IHolidayPlanRepository repository = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble1.Object, holidayPlanDouble2.Object });

        //act
        var result = repository.FindAllCollaboratorsWithHolidayPeriodsLongerThan(days).ToList();

        //assert
        Assert.Contains(colaboratorDouble1.Object, result);
        Assert.DoesNotContain(colaboratorDouble2.Object, result);
    }

    [Fact]
    public void WhenNoCollaboratorHaveHolidayPeriodLongerThan_ThenShouldReturnEmptyList()
    {
        //arrange
        int days = 5;

        Mock<IColaborator> colaboratorDouble1 = new Mock<IColaborator>();
        Mock<IColaborator> colaboratorDouble2 = new Mock<IColaborator>();

        Mock<IHolidayPlan> holidayPlanDouble1 = new Mock<IHolidayPlan>();
        holidayPlanDouble1.Setup(p => p.HasPeriodLongerThan(days)).Returns(false);
        holidayPlanDouble1.Setup(p => p.GetColaborator()).Returns(colaboratorDouble1.Object);

        Mock<IHolidayPlan> holidayPlanDouble2 = new Mock<IHolidayPlan>();
        holidayPlanDouble2.Setup(p => p.HasPeriodLongerThan(days)).Returns(false);
        holidayPlanDouble2.Setup(p => p.GetColaborator()).Returns(colaboratorDouble2.Object);

        IHolidayPlanRepository repository = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble1.Object, holidayPlanDouble2.Object });

        //act
        var result = repository.FindAllCollaboratorsWithHolidayPeriodsLongerThan(days).ToList();

        //assert
        Assert.Empty(result);
    }
}