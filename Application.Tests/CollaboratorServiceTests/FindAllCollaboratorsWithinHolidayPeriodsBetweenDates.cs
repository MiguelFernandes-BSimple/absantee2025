using Moq;
using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using System.Linq.Expressions;
using Domain.Factory;
using System.Threading.Tasks;

namespace Application.Tests.CollaboratorServiceTests;

public class FindAllCollaboratorsWithinHolidayPeriodsBetweenDates
{
    [Fact]
    public async Task WhenCollaboratorHasHolidayPeriodWithinDateRange_ThenReturnsCollaborator()
    {
        // arrange
        var collaboratorDouble = new Mock<ICollaborator>();
        collaboratorDouble.Setup(c => c.GetId()).Returns(1);

        var periodDateDouble = new Mock<IPeriodDate>();

        var holidayPlanDouble = new Mock<IHolidayPlan>();
        holidayPlanDouble.Setup(hpd => hpd.GetCollaboratorId()).Returns(1);

        var holidayPlanRepoDouble = new Mock<IHolidayPlanRepository>();
        holidayPlanRepoDouble.Setup(hprd => hprd.FindHolidayPlansWithinPeriodAsync(periodDateDouble.Object))
                             .ReturnsAsync(new List<IHolidayPlan> { holidayPlanDouble.Object });

        var apcDouble = new Mock<IAssociationProjectCollaboratorRepository>();

        var collabRepoDouble = new Mock<ICollaboratorRepository>();
        collabRepoDouble.Setup(c => c.Find(It.IsAny<Expression<Func<ICollaborator, bool>>>()))
                        .Returns(new List<ICollaborator> { collaboratorDouble.Object });

        var userRepo = new Mock<IUserRepository>();
        var collabFactory = new Mock<ICollaboratorFactory>();
        var colabService = new CollaboratorService(apcDouble.Object, holidayPlanRepoDouble.Object, collabRepoDouble.Object, userRepo.Object, collabFactory.Object);

        // act  
        var result = await colabService.FindAllWithHolidayPeriodsBetweenDates(periodDateDouble.Object);

        // assert
        Assert.Single(result);
        Assert.Contains(collaboratorDouble.Object, result);
    }



    [Fact]
    public async Task WhenMultipleCollaboratorsHaveHolidayPeriodsWithinDateRange_ThenReturnsAllCollaborators()
    {

        // arrange
        var collaboratorDouble1 = new Mock<ICollaborator>();
        collaboratorDouble1.Setup(c => c.GetId()).Returns(1);

        var collaboratorDouble2 = new Mock<ICollaborator>();
        collaboratorDouble2.Setup(c => c.GetId()).Returns(1);

        var periodDateDouble = new Mock<IPeriodDate>();

        var holidayPlanDouble1 = new Mock<IHolidayPlan>();
        holidayPlanDouble1.Setup(hpd => hpd.GetCollaboratorId()).Returns(1);

        var holidayPlanDouble2 = new Mock<IHolidayPlan>();
        holidayPlanDouble2.Setup(hpd => hpd.GetCollaboratorId()).Returns(2);

        var holidayPlanRepoDouble = new Mock<IHolidayPlanRepository>();
        holidayPlanRepoDouble.Setup(hprd => hprd.FindHolidayPlansWithinPeriodAsync(periodDateDouble.Object)).ReturnsAsync(new List<IHolidayPlan> { holidayPlanDouble1.Object, holidayPlanDouble2.Object });

        var apcDouble = new Mock<IAssociationProjectCollaboratorRepository>();

        var collabRepoDouble = new Mock<ICollaboratorRepository>();
        collabRepoDouble.Setup(c => c.Find(It.IsAny<Expression<Func<ICollaborator, bool>>>())).Returns(new List<ICollaborator> { collaboratorDouble1.Object, collaboratorDouble2.Object });

        var userRepo = new Mock<IUserRepository>();
        var collabFactory = new Mock<ICollaboratorFactory>();
        var colabService = new CollaboratorService(apcDouble.Object, holidayPlanRepoDouble.Object, collabRepoDouble.Object, userRepo.Object, collabFactory.Object);

        // act  
        var result = await colabService.FindAllWithHolidayPeriodsBetweenDates(periodDateDouble.Object);

        // assert
        Assert.Equal(2, result.Count());
        Assert.Contains(collaboratorDouble1.Object, result);
        Assert.Contains(collaboratorDouble2.Object, result);
    }

    [Fact]
    public async Task WhenNoCollaboratorsHaveHolidayPeriodsInDateRange_ThenReturnsEmptyList()
    {
        // arrange
        var collaboratorDouble = new Mock<ICollaborator>();
        collaboratorDouble.Setup(c => c.GetId()).Returns(1);

        var periodDateDouble = new Mock<IPeriodDate>();

        var holidayPlanDouble = new Mock<IHolidayPlan>();
        holidayPlanDouble.Setup(hpd => hpd.GetCollaboratorId()).Returns(1);

        var holidayPlanRepoDouble = new Mock<IHolidayPlanRepository>();
        holidayPlanRepoDouble.Setup(hprd => hprd.FindHolidayPlansWithinPeriodAsync(periodDateDouble.Object)).ReturnsAsync(new List<IHolidayPlan>());

        var apcDouble = new Mock<IAssociationProjectCollaboratorRepository>();

        var collabRepoDouble = new Mock<ICollaboratorRepository>();
        collabRepoDouble.Setup(c => c.Find(It.IsAny<Expression<Func<ICollaborator, bool>>>())).Returns(new List<ICollaborator>());

        var userRepo = new Mock<IUserRepository>();
        var collabFactory = new Mock<ICollaboratorFactory>();
        var colabService = new CollaboratorService(apcDouble.Object, holidayPlanRepoDouble.Object, collabRepoDouble.Object, userRepo.Object, collabFactory.Object);

        // act  
        var result = await colabService.FindAllWithHolidayPeriodsBetweenDates(periodDateDouble.Object);

        // assert
        Assert.Empty(result);
    }


}

