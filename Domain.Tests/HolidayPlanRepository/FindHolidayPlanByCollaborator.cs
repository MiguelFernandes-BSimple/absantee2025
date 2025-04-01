using Domain;
using Moq;


public class FindHolidayPlanByCollaborator{
    [Fact]
    public void WhenFindingHolidayPlanByCollaborator_ThenReturnsCorrectCollaborator()
    {
        //arrange
        var collaboratorDouble1 = new Mock<ICollaborator>();
        var collaboratorDouble2 = new Mock<ICollaborator>();

        var holidayPlanDouble1 = new Mock<IHolidayPlan>();
        holidayPlanDouble1.Setup(hp => hp.GetCollaborator()).Returns(collaboratorDouble1.Object);

        var holidayPlanDouble2 = new Mock<IHolidayPlan>();
        holidayPlanDouble2.Setup(hp => hp.GetCollaborator()).Returns(collaboratorDouble2.Object);

        var repo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble1.Object, holidayPlanDouble2.Object });

        //act
        var result = repo.FindHolidayPlanByCollaborator(collaboratorDouble1.Object);

        //assert
        Assert.Equal(holidayPlanDouble1.Object, result);
    }

    [Fact]
    public void WhenFindingHolidayPlanBCollaborator_ThenReturnsNull()
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