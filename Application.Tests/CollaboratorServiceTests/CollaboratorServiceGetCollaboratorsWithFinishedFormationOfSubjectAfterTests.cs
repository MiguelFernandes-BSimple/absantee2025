using Moq;
using Xunit;
using Application.Services;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Factory;

namespace Application.Tests.CollaboratorServiceTests
{
    public class CollaboratorServiceGetCollaboratorsWithFinishedFormationOfSubjectAfterTests
    {
        [Fact]
        public async Task WhenFormationIsAfterGivenDate_ThenReturnsCorrectCollaborators()
        {
            //arrange
            var date = new DateOnly(2023, 1, 1);

            var subject = new Mock<IFormationSubject>();
            subject.Setup(s => s.GetId()).Returns(1);

            var module = new Mock<IFormationModule>();
            module.Setup(m => m.GetId()).Returns(10);

            var period = new PeriodDate(DateOnly.FromDateTime(DateTime.Today.AddDays(-5)), DateOnly.FromDateTime(DateTime.Today.AddDays(5)));
            var assoc = new Mock<IAssociationFormationModuleCollaborator>();
            assoc.Setup(a => a.GetCollaboratorId()).Returns(42);
            assoc.Setup(a => a._periodDate).Returns(period);

            var collab = new Mock<ICollaborator>();
            collab.Setup(c => c.GetId()).Returns(42);

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
            assocRepo.Setup(r => r.FindAllByFormationModuleAsync(10)).ReturnsAsync(new List<IAssociationFormationModuleCollaborator> { assoc.Object });
            collabRepo.Setup(r => r.GetByIdsAsync(It.IsAny<IEnumerable<long>>())).ReturnsAsync(new List<ICollaborator> { collab.Object });

            var service = new CollaboratorService(apcRepo.Object, holidayRepo.Object, collabRepo.Object, userRepo.Object, factory.Object, moduleRepo.Object, subjectRepo.Object, assocRepo.Object);

            //act
            var result = await service.GetCollaboratorsWithFinishedFormationOfSubjectAfter("NODEJS", date);

            //assert
            Assert.Contains(collab.Object, result);
        }

        [Fact]
        public async Task WhenModuleDoesNotExist_ThenThrows()
        {
            //arrange
            var subject = new Mock<IFormationSubject>();
            subject.Setup(s => s.GetId()).Returns(1);

            var subjectRepo = new Mock<IFormationSubjectRepository>();
            subjectRepo.Setup(r => r.GetByTitleAsync(It.IsAny<string>())).ReturnsAsync(subject.Object);

            var moduleRepo = new Mock<IFormationModuleRepository>();
            moduleRepo.Setup(m => m.GetBySubjectId(It.IsAny<long>())).ReturnsAsync((IFormationModule?)null);

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
                service.GetCollaboratorsWithFinishedFormationOfSubjectAfter("NODEJS", new DateOnly(2023, 10, 10)));
        }
    }
}
