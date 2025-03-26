using Domain;
using Moq;

namespace Domain.Tests;
public class HolidayPlanRepositoryTest
{
    [Fact]
    public void WhenCalculatingHolidayDaysOfCollaboratorInAProject_ThenReturnCorrectValue()
    {
        //arrange
        Mock<IAssociationProjectColaborator> associationDouble = new Mock<IAssociationProjectColaborator>();
        Mock<IHolidayPlan> holidayPlanDouble = new Mock<IHolidayPlan>();
        Mock<IColaborator> collaboratorDouble = new Mock<IColaborator>();

        DateOnly initDate = new DateOnly(2025, 6, 1);
        DateOnly finalDate = new DateOnly(2025, 6, 10);

        associationDouble.Setup(a => a.GetCollaborator()).Returns(collaboratorDouble.Object);
        associationDouble.Setup(a => a.GetInitDate()).Returns(initDate);
        associationDouble.Setup(a => a.GetFinalDate()).Returns(finalDate);

        holidayPlanDouble.Setup(hp => hp.GetCollaborator()).Returns(collaboratorDouble.Object);
        holidayPlanDouble.Setup(hp => hp.GetNumberOfHolidayDaysBetween(initDate, finalDate)).Returns(5);

        IHolidayPlanRepository repository = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble.Object });

        //act
        int result = repository.GetHolidayDaysOfCollaboratorInProject(associationDouble.Object);

        //assert
        Assert.Equal(5, result);

    }


    [Fact]
    public void WhenNoHolidayPlanIsFound_ThenReturnsZero()
    {
        //arrange
        Mock<IAssociationProjectColaborator> associationDouble = new Mock<IAssociationProjectColaborator>();
        Mock<IColaborator> collaboratorDouble = new Mock<IColaborator>();

        associationDouble.Setup(a => a.GetColaborator()).Returns(collaboratorDouble.Object);

        List<IHolidayPlan> emptyList = new List<IHolidayPlan>();
        IHolidayPlanRepository repository = new HolidayPlanRepository(emptyList);

        //act
        int result = repository.GetHolidayDaysOfCollaboratorInProject(associationDouble.Object);

        //assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void WhenPassingCorrectValues_ThenObjectIsCreated()
    {
        //arrange
        Mock<IHolidayPlan> holidayPlanDouble = new Mock<IHolidayPlan>();
        List<IHolidayPlan> holidayPlans = new List<IHolidayPlan> { holidayPlanDouble.Object };

        //act
        new HolidayPlanRepository(holidayPlans);

        //assert
    }

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