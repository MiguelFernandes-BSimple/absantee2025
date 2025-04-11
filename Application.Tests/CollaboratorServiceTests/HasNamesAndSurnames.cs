using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Moq;

namespace Application.Tests.CollaboratorServiceTests
{
    public class HasNamesAndSurnames
    {
        [Fact]
        public async Task WhenUserHasMatchingNamesAndNamesAndSurnames_ThenReturnsTrue()
        {
            //arrange
            Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();
            collaboratorDouble.Setup(c => c.GetUserId()).Returns(It.IsAny<long>());

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.GetById(It.IsAny<long>())).Returns(collaboratorDouble.Object);

            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(u => u.HasNamesAndSurnames(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            var collabFactory = new Mock<ICollaboratorFactory>();
            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, collabFactory.Object);

            //act
            var result = await collaboratorService.HasNamesAndSurnames(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>());

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task WhenUserDoesNotHaveMatchingNamesAndSurnames_ThenReturnsFalse()
        {
            //arrange
            Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();
            collaboratorDouble.Setup(c => c.GetUserId()).Returns(It.IsAny<long>());

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.GetById(It.IsAny<long>())).Returns(collaboratorDouble.Object);

            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(u => u.HasNamesAndSurnames(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            var collabFactory = new Mock<ICollaboratorFactory>();
            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, collabFactory.Object);

            //act
            var result = await collaboratorService.HasNamesAndSurnames(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>());

            //assert
            Assert.False(result);
        }

        [Fact]
        public async Task WhenCollaboratorDoesNotExists_ThenReturnsFalse()
        {
            //arrange
            Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();
            collaboratorDouble.Setup(c => c.GetUserId()).Returns(It.IsAny<long>());

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.GetById(It.IsAny<long>())).Returns((ICollaborator?)null);

            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(u => u.HasNamesAndSurnames(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            var collabFactory = new Mock<ICollaboratorFactory>();
            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, collabFactory.Object);

            //act
            var result = await collaboratorService.HasNamesAndSurnames(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>());

            //assert
            Assert.False(result);
        }
    }
}
