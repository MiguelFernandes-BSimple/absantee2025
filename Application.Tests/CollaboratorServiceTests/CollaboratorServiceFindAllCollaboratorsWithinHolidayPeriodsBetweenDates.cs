using Moq;
using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using System.Linq.Expressions;
using Domain.Factory;
using Domain.Models;
using System.Threading.Tasks;

namespace Application.Tests.CollaboratorServiceTests;

public class CollaboratorServiceFindAllCollaboratorsWithinHolidayPeriodsBetweenDates
{
    [Fact]
    public async Task WhenCollaboratorHasHolidayPeriodWithinDateRange_ThenReturnsCollaborator()
    {
        // arrange
        var collaboratorDouble = new Mock<ICollaborator>();
        collaboratorDouble.Setup(c => c.GetId()).Returns(1);
        var collabsIds = new List<long>() { 1 };

        var holidayPlanDouble = new Mock<IHolidayPlan>();
        holidayPlanDouble.Setup(hpd => hpd.GetCollaboratorId()).Returns(1);

        var holidayPlanRepoDouble = new Mock<IHolidayPlanRepository>();
        holidayPlanRepoDouble.Setup(hprd => hprd.FindHolidayPlansWithinPeriodAsync(It.IsAny<PeriodDate>()))
                             .ReturnsAsync(new List<IHolidayPlan> { holidayPlanDouble.Object });

        var apcDouble = new Mock<IAssociationProjectCollaboratorRepository>();

        var expected = new List<ICollaborator>() { collaboratorDouble.Object };

        var collabRepoDouble = new Mock<ICollaboratorRepository>();
        collabRepoDouble.Setup(c => c.GetByIdsAsync(collabsIds)).ReturnsAsync(expected);

        var userRepo = new Mock<IUserRepository>();
        var collabFactory = new Mock<ICollaboratorFactory>();
        var assocFormationCollaboratorRepoDouble = new Mock<IAssociationFormationModuleCollaboratorRepository>();
        var formationModuleRepoDouble = new Mock<IFormationModuleRepository>();
        var formationSubjectRepoDouble = new Mock<IFormationSubjectRepository>();
        CollaboratorService collaboratorService = new CollaboratorService(apcDouble.Object, holidayPlanRepoDouble.Object, collabRepoDouble.Object, userRepo.Object, collabFactory.Object, formationModuleRepoDouble.Object, formationSubjectRepoDouble.Object, assocFormationCollaboratorRepoDouble.Object);
        // act  
        var result = await collaboratorService.FindAllWithHolidayPeriodsBetweenDates(It.IsAny<PeriodDate>());

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

        var holidayPlanDouble1 = new Mock<IHolidayPlan>();
        holidayPlanDouble1.Setup(hpd => hpd.GetCollaboratorId()).Returns(1);

        var holidayPlanDouble2 = new Mock<IHolidayPlan>();
        holidayPlanDouble2.Setup(hpd => hpd.GetCollaboratorId()).Returns(2);

        var holidayPlanRepoDouble = new Mock<IHolidayPlanRepository>();
        holidayPlanRepoDouble.Setup(hprd => hprd.FindHolidayPlansWithinPeriodAsync(It.IsAny<PeriodDate>())).ReturnsAsync(new List<IHolidayPlan> { holidayPlanDouble1.Object, holidayPlanDouble2.Object });

        var apcDouble = new Mock<IAssociationProjectCollaboratorRepository>();

        var collabRepoDouble = new Mock<ICollaboratorRepository>();
        collabRepoDouble.Setup(c => c.GetByIdsAsync(new List<long> { 1, 2 })).ReturnsAsync(new List<ICollaborator> { collaboratorDouble1.Object, collaboratorDouble2.Object });

        var userRepo = new Mock<IUserRepository>();
        var collabFactory = new Mock<ICollaboratorFactory>();
        var assocFormationCollaboratorRepoDouble = new Mock<IAssociationFormationModuleCollaboratorRepository>();
        var formationModuleRepoDouble = new Mock<IFormationModuleRepository>();
        var formationSubjectRepoDouble = new Mock<IFormationSubjectRepository>();
        CollaboratorService collaboratorService = new CollaboratorService(apcDouble.Object, holidayPlanRepoDouble.Object, collabRepoDouble.Object, userRepo.Object, collabFactory.Object, formationModuleRepoDouble.Object, formationSubjectRepoDouble.Object, assocFormationCollaboratorRepoDouble.Object);
        // act  
        var result = await collaboratorService.FindAllWithHolidayPeriodsBetweenDates(It.IsAny<PeriodDate>());

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

        var holidayPlanDouble = new Mock<IHolidayPlan>();
        holidayPlanDouble.Setup(hpd => hpd.GetCollaboratorId()).Returns(1);

        var holidayPlanRepoDouble = new Mock<IHolidayPlanRepository>();
        holidayPlanRepoDouble.Setup(hprd => hprd.FindHolidayPlansWithinPeriodAsync(It.IsAny<PeriodDate>())).ReturnsAsync(new List<IHolidayPlan>());

        var apcDouble = new Mock<IAssociationProjectCollaboratorRepository>();

        var collabRepoDouble = new Mock<ICollaboratorRepository>();
        collabRepoDouble.Setup(c => c.GetByIdsAsync(new List<long> { 1 })).ReturnsAsync(new List<ICollaborator>());

        var userRepo = new Mock<IUserRepository>();
        var collabFactory = new Mock<ICollaboratorFactory>();
        var assocFormationCollaboratorRepoDouble = new Mock<IAssociationFormationModuleCollaboratorRepository>();
        var formationModuleRepoDouble = new Mock<IFormationModuleRepository>();
        var formationSubjectRepoDouble = new Mock<IFormationSubjectRepository>();
        CollaboratorService collaboratorService = new CollaboratorService(apcDouble.Object, holidayPlanRepoDouble.Object, collabRepoDouble.Object, userRepo.Object, collabFactory.Object, formationModuleRepoDouble.Object, formationSubjectRepoDouble.Object, assocFormationCollaboratorRepoDouble.Object);
        // act  
        var result = await collaboratorService.FindAllWithHolidayPeriodsBetweenDates(It.IsAny<PeriodDate>());

        // assert
        Assert.Empty(result);
    }


}

