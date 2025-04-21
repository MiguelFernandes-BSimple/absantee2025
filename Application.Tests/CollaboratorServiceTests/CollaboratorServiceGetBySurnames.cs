using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Moq;

namespace Application.Tests.CollaboratorServiceTests
{
    public class CollaboratorServiceGetBySurnames
    {
        [Fact]
        public async Task WhenSearchingBySurnamesThatExists_ThenReturnsExpectedResult()
        {
            //arrange
            Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();
            collaboratorDouble.Setup(c => c.GetUserId()).Returns(It.IsAny<long>());

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();

            var userDouble1 = new Mock<IUser>();
            var userDouble2 = new Mock<IUser>();
            var userList = new List<IUser>()
            {
                userDouble1.Object, userDouble2.Object
            };
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(u => u.GetBySurnamesAsync(It.IsAny<string>())).ReturnsAsync(userList);
            userDouble1.Setup(u => u.GetId()).Returns(It.IsAny<long>());
            userDouble2.Setup(u => u.GetId()).Returns(It.IsAny<long>());
            var collabFactory = new Mock<ICollaboratorFactory>();

            var expected = new List<ICollaborator>()
            {
                new Mock<ICollaborator>().Object,
                new Mock<ICollaborator>().Object
            };

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.GetByIdsAsync(It.IsAny<IEnumerable<long>>())).ReturnsAsync(expected);

            var assocFormationCollaboratorRepoDouble = new Mock<IAssociationFormationModuleCollaboratorRepository>();
            var formationModuleRepoDouble = new Mock<IFormationModuleRepository>();
            var formationSubjectRepoDouble = new Mock<IFormationSubjectRepository>();

            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, collabFactory.Object, formationModuleRepoDouble.Object, formationSubjectRepoDouble.Object, assocFormationCollaboratorRepoDouble.Object);
            //act
            var result = await collaboratorService.GetBySurnames(It.IsAny<string>());

            //assert
            Assert.True(expected.SequenceEqual(result));
        }

        [Fact]
        public async Task WhenSearchingBySurnamesThatDontExists_ThenReturnsEmptyList()
        {
            //arrange
            Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();
            collaboratorDouble.Setup(c => c.GetUserId()).Returns(It.IsAny<long>());

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();

            var userList = new List<IUser>();
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(u => u.GetBySurnamesAsync(It.IsAny<string>())).ReturnsAsync(userList);
            var collabFactory = new Mock<ICollaboratorFactory>();

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.GetByIdsAsync(It.IsAny<IEnumerable<long>>())).ReturnsAsync(new List<ICollaborator>());

            var assocFormationCollaboratorRepoDouble = new Mock<IAssociationFormationModuleCollaboratorRepository>();
            var formationModuleRepoDouble = new Mock<IFormationModuleRepository>();
            var formationSubjectRepoDouble = new Mock<IFormationSubjectRepository>();

            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, collabFactory.Object, formationModuleRepoDouble.Object, formationSubjectRepoDouble.Object, assocFormationCollaboratorRepoDouble.Object);
            //act
            var result = await collaboratorService.GetBySurnames(It.IsAny<string>());

            //assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task WhenUsersAreFoundButNoCollaboratorsExist_ThenReturnsEmptyList()
        {
            //arrange
            Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();
            collaboratorDouble.Setup(c => c.GetUserId()).Returns(It.IsAny<long>());

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();

            var userDouble1 = new Mock<IUser>();
            var userDouble2 = new Mock<IUser>();
            var userList = new List<IUser>()
            {
                userDouble1.Object, userDouble2.Object
            };
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(u => u.GetBySurnamesAsync(It.IsAny<string>())).ReturnsAsync(userList);
            userDouble1.Setup(u => u.GetId()).Returns(It.IsAny<long>());
            userDouble2.Setup(u => u.GetId()).Returns(It.IsAny<long>());
            var collabFactory = new Mock<ICollaboratorFactory>();

            var expected = new List<ICollaborator>();

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.GetByIdsAsync(It.IsAny<IEnumerable<long>>())).ReturnsAsync(expected);

            var assocFormationCollaboratorRepoDouble = new Mock<IAssociationFormationModuleCollaboratorRepository>();
            var formationModuleRepoDouble = new Mock<IFormationModuleRepository>();
            var formationSubjectRepoDouble = new Mock<IFormationSubjectRepository>();

            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, collabFactory.Object, formationModuleRepoDouble.Object, formationSubjectRepoDouble.Object, assocFormationCollaboratorRepoDouble.Object);
            //act
            var result = await collaboratorService.GetBySurnames(It.IsAny<string>());

            //assert
            Assert.Empty(result);
        }
    }
}
