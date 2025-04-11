using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Moq;

namespace Application.Tests.CollaboratorServiceTests
{
    public class HasNames
    {
        [Fact]
        public async Task WhenUserHasMatchingNames_ThenReturnsTrue()
        {
            //arrange
            Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();
            collaboratorDouble.Setup(c => c.GetUserId()).Returns(It.IsAny<long>());

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.GetById(It.IsAny<long>())).Returns(collaboratorDouble.Object);

            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(u => u.HasNames(It.IsAny<long>(), It.IsAny<string>())).ReturnsAsync(true);
            var collabFactory = new Mock<ICollaboratorFactory>();
            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, collabFactory.Object);

            //act
            var result = await collaboratorService.HasNames(It.IsAny<long>(), It.IsAny<string>());

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task WhenUserDoesNotHaveMatchingNames_ThenReturnsFalse()
        {
            //arrange
            Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();
            collaboratorDouble.Setup(c => c.GetUserId()).Returns(It.IsAny<long>());

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.GetById(It.IsAny<long>())).Returns(collaboratorDouble.Object);

            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(u => u.HasNames(It.IsAny<long>(), It.IsAny<string>())).ReturnsAsync(false);
            var collabFactory = new Mock<ICollaboratorFactory>();
            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, collabFactory.Object);

            //act
            var result = await collaboratorService.HasNames(It.IsAny<long>(), It.IsAny<string>());

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
            userRepo.Setup(u => u.HasNames(It.IsAny<long>(), It.IsAny<string>())).ReturnsAsync(true);
            var collabFactory = new Mock<ICollaboratorFactory>();
            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, collabFactory.Object);

            //act
            var result = await collaboratorService.HasNames(It.IsAny<long>(), It.IsAny<string>());

            //assert
            Assert.False(result);
        }
    }
}
