using Infrastructure.Repositories;
using Domain.Interfaces;
using Moq;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class FindHolidayPlanByCollaborator
{
    [Fact]
    public void WhenFindingHolidayPlanByCollaborator_ThenReturnsCorrectCollaborator()
    {
        //arrange
        var collaboratorDouble = new Mock<ICollaborator>();

        var holidayPlanDouble1 = new Mock<IHolidayPlan>();
        holidayPlanDouble1.Setup(hp => hp.HasCollaborator(collaboratorDouble.Object)).Returns(true);

        var holidayPlanDouble2 = new Mock<IHolidayPlan>();

        var repo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble1.Object, holidayPlanDouble2.Object });

        //act
        var result = repo.FindHolidayPlanByCollaborator(collaboratorDouble.Object);

        //assert
        Assert.Equal(holidayPlanDouble1.Object, result);
    }

    [Fact]
    public void WhenCollaboratorDontHaveHolidayPlan_ThenReturnsNull()
    {
        //arrange
        var collaboratorDouble1 = new Mock<ICollaborator>();

        var repo = new HolidayPlanRepository();

        //act
        var result = repo.FindHolidayPlanByCollaborator(collaboratorDouble1.Object);

        //assert
        Assert.Null(result);
    }
}