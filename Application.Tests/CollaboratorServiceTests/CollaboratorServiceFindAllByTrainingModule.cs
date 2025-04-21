using Moq;
using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Domain.Factory;

namespace Application.Tests.CollaboratorServiceTests;

public class CollaboratorServiceFindAllByTrainingModule
{
    [Fact]
    public async Task WhenGivenValidData_ThenReturnsExpectedCollaborators()
    {
        // arrange
        var collaboratorDouble1 = new Mock<ICollaborator>();
        long collabId1 = 1;
        collaboratorDouble1.Setup(c => c.GetId()).Returns(collabId1);
        var collaboratorDouble2 = new Mock<ICollaborator>();
        long collabId2 = 2;
        collaboratorDouble1.Setup(c => c.GetId()).Returns(collabId2);
        var collabsIds = new List<long>() { collabId1, collabId2 };

        var expected = new List<ICollaborator>() { collaboratorDouble1.Object, collaboratorDouble2.Object };


        var apcRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        var holidayPlanRepo = new Mock<IHolidayPlanRepository>();
        var userRepo = new Mock<IUserRepository>();
        var collabFactory = new Mock<ICollaboratorFactory>();

        var collabRepo = new Mock<ICollaboratorRepository>();
        collabRepo.Setup(c => c.GetByIdsAsync(collabsIds)).ReturnsAsync(expected);

        long trainingModuleId = 3;

        var assocsDouble = new Mock<IAssociationTrainingModuleCollaborator>();
        assocsDouble.Setup(a => a.CollaboratorId).Returns(collabId1);
        var assocsDouble2 = new Mock<IAssociationTrainingModuleCollaborator>();
        assocsDouble2.Setup(a => a.CollaboratorId).Returns(collabId2);
        var assocsListDouble = new List<IAssociationTrainingModuleCollaborator>{
            assocsDouble.Object,
            assocsDouble2.Object
        };

        var atmcRepo = new Mock<IAssociationTrainingModuleCollaboratorRepository>();
        atmcRepo.Setup(a => a.FindAllByTrainingModuleAsync(trainingModuleId)).ReturnsAsync(assocsListDouble);

        var colabService = new CollaboratorService(apcRepo.Object, atmcRepo.Object, holidayPlanRepo.Object, collabRepo.Object, userRepo.Object, collabFactory.Object);

        // act  
        var result = await colabService.FindAllByTrainingModule(trainingModuleId);

        // assert
        Assert.Equal(2, result.Count());
        Assert.Contains(collaboratorDouble1.Object, result);
        Assert.Contains(collaboratorDouble2.Object, result);

    }

}

