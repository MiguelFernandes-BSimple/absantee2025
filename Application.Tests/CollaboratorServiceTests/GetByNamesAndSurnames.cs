﻿using System.Linq.Expressions;
using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Moq;

namespace Application.Tests.CollaboratorServiceTests
{
    public class GetByNamesAndSurnames
    {
        [Fact]
        public async Task WhenSearchingByNamesAndSurnamesThatExists_ThenReturnsExpectedResult()
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
            userRepo.Setup(u => u.GetByNamesAndSurnames(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(userList);
            userDouble1.Setup(u => u.GetId()).Returns(It.IsAny<long>());
            userDouble2.Setup(u => u.GetId()).Returns(It.IsAny<long>());
            var collabFactory = new Mock<ICollaboratorFactory>();

            var expected = new List<ICollaborator>()
            {
                new Mock<ICollaborator>().Object,
                new Mock<ICollaborator>().Object
            };

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.Find(It.IsAny<Expression<Func<ICollaborator, bool>>>()))
                            .Returns(expected);

            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, collabFactory.Object);

            //act
            var result = await collaboratorService.GetByNamesAndSurnames(It.IsAny<string>(), It.IsAny<string>());

            //assert
            Assert.True(expected.SequenceEqual(result));
        }

        [Fact]
        public async Task WhenSearchingByNamesAndSurnamesThatDontExists_ThenReturnsEmptyList()
        {
            //arrange
            Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();
            collaboratorDouble.Setup(c => c.GetUserId()).Returns(It.IsAny<long>());

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();

            var userList = new List<IUser>();
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(u => u.GetByNamesAndSurnames(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(userList);
            var collabFactory = new Mock<ICollaboratorFactory>();

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.Find(It.IsAny<Expression<Func<ICollaborator, bool>>>()))
                            .Returns(new List<ICollaborator>());

            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, collabFactory.Object);

            //act
            var result = await collaboratorService.GetByNamesAndSurnames(It.IsAny<string>(), It.IsAny<string>());

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
            userRepo.Setup(u => u.GetByNamesAndSurnames(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(userList);
            userDouble1.Setup(u => u.GetId()).Returns(It.IsAny<long>());
            userDouble2.Setup(u => u.GetId()).Returns(It.IsAny<long>());
            var collabFactory = new Mock<ICollaboratorFactory>();

            var expected = new List<ICollaborator>();

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.Find(It.IsAny<Expression<Func<ICollaborator, bool>>>()))
                            .Returns(expected);

            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, collabFactory.Object);

            //act
            var result = await collaboratorService.GetByNamesAndSurnames(It.IsAny<string>(), It.IsAny<string>());

            //assert
            Assert.Empty(result);
        }
    }
}
