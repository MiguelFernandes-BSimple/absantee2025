using Moq;
using Application.Services;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Factory;

namespace Application.Tests.CollaboratorServiceTests
{
    public class CollaboratorServiceGetActiveCollaboratorsWithoutFormationInTests
    {
        [Fact]
        public async Task WhenGettingCollaboratorsWithoutFormation_ThenReturnsCorrectList()
        {
            //arrange
            var subject = new Mock<IFormationSubject>();
            subject.Setup(s => s.GetId()).Returns(1);

            var module = new Mock<IFormationModule>();
            module.Setup(m => m.GetId()).Returns(10);

            var assoc1 = new Mock<IAssociationFormationModuleCollaborator>();
            assoc1.Setup(a => a.GetCollaboratorId()).Returns(1);

            var collab1 = new Mock<ICollaborator>();
            var collab2 = new Mock<ICollaborator>();
            collab1.Setup(c => c.GetId()).Returns(1);
            collab2.Setup(c => c.GetId()).Returns(2);

            var subjectRepo = new Mock<IFormationSubjectRepository>();
            var moduleRepo = new Mock<IFormationModuleRepository>();
            var assocRepo = new Mock<IAssociationFormationModuleCollaboratorRepository>();
            var collabRepo = new Mock<ICollaboratorRepository>();
            var apcRepo = new Mock<IAssociationProjectCollaboratorRepository>();
            var holidayRepo = new Mock<IHolidayPlanRepository>();
            var userRepo = new Mock<IUserRepository>();
            var factory = new Mock<ICollaboratorFactory>();

            subjectRepo.Setup(r => r.GetByTitleAsync(It.IsAny<string>())).ReturnsAsync(subject.Object);
            moduleRepo.Setup(r => r.GetBySubjectId(1)).ReturnsAsync(module.Object);
            assocRepo.Setup(r => r.FindAllByFormationModuleAsync(10)).ReturnsAsync(new List<IAssociationFormationModuleCollaborator> { assoc1.Object });
            collabRepo.Setup(r => r.GetActiveCollaboratorsAsync()).ReturnsAsync(new List<ICollaborator> { collab1.Object, collab2.Object });

            var service = new CollaboratorService(apcRepo.Object, holidayRepo.Object, collabRepo.Object, userRepo.Object, factory.Object, moduleRepo.Object, subjectRepo.Object, assocRepo.Object);

            //act
            var result = await service.GetActiveCollaboratorsWithoutFormationIn("NODEJS");

            //assert
            Assert.Contains(collab2.Object, result);
        }

        [Fact]
        public async Task WhenSubjectDoesNotExist_ThenThrows()
        {
            //arrange
            var subjectRepo = new Mock<IFormationSubjectRepository>();
            subjectRepo.Setup(r => r.GetByTitleAsync(It.IsAny<string>())).ReturnsAsync((IFormationSubject?)null);

            var moduleRepo = new Mock<IFormationModuleRepository>();
            var assocRepo = new Mock<IAssociationFormationModuleCollaboratorRepository>();
            var collabRepo = new Mock<ICollaboratorRepository>();
            var apcRepo = new Mock<IAssociationProjectCollaboratorRepository>();
            var holidayRepo = new Mock<IHolidayPlanRepository>();
            var userRepo = new Mock<IUserRepository>();
            var factory = new Mock<ICollaboratorFactory>();

            var service = new CollaboratorService(apcRepo.Object, holidayRepo.Object, collabRepo.Object, userRepo.Object, factory.Object, moduleRepo.Object, subjectRepo.Object, assocRepo.Object);

            //assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                //act
                service.GetActiveCollaboratorsWithoutFormationIn("NODEJS"));
        }
    }
}
