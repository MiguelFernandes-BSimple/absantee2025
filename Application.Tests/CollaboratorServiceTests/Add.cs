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
    public class Add
    {
        [Fact]
        public async Task WhenAddingValidCollaborator_ThenShouldReturnTrue()
        {
            //arrange
            Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();

            var assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            var holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();

            var userDouble = new Mock<IUser>();
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(u => u.GetById(It.IsAny<long>())).Returns(userDouble.Object);
            userDouble.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
            userDouble.Setup(u => u.IsDeactivated()).Returns(false);

            var collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.isRepeated(It.IsAny<ICollaborator>())).ReturnsAsync(false);
            collabRepository.Setup(c => c.AddAsync(It.IsAny<ICollaborator>())).ReturnsAsync(It.IsAny<ICollaborator>());

            var collabFactory = new Mock<ICollaboratorFactory>();
            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, collabFactory.Object);

            //act
            var result = await collaboratorService.Add(It.IsAny<long>(), It.IsAny<IPeriodDateTime>());

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task WhenCollaboratorCreationThrows_ThenShouldReturnFalse()
        {
            //arrange
            Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();

            var assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            var holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();

            var userDouble = new Mock<IUser>();
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(u => u.GetById(It.IsAny<long>())).Returns(userDouble.Object);
            //throws exception
            userDouble.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(true);

            var collabRepository = new Mock<ICollaboratorRepository>();

            var collabFactory = new Mock<ICollaboratorFactory>();
            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, collabFactory.Object);

            //act
            var result = await collaboratorService.Add(It.IsAny<long>(), It.IsAny<IPeriodDateTime>());

            //assert
            Assert.False(result);
        }

    }
}
