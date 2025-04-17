using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.ProjectManagerTests
{
    public class ProjectManagerFactoryTests
    {
        // tests for creating with user id and period date time
        [Fact]
        public async Task WhenPassingValidUserIdAndPeriodDateTime_ThenCreatesProjectManager()
        {
            // arrange
            var userDouble = new Mock<IUser>();
            userDouble.Setup(ud => ud.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
            userDouble.Setup(ud => ud.IsDeactivated()).Returns(false);

            PeriodDateTime periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

            var userRepoDouble = new Mock<IUserRepository>();
            userRepoDouble.Setup(urd => urd.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(userDouble.Object);

            var pmFactory = new ProjectManagerFactory(userRepoDouble.Object);

            // act
            var result = await pmFactory.Create(It.IsAny<long>(), periodDateTime);

            // assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task WhenUserIdDoesntExist_ThenThrowsException()
        {
            var userRepoDouble = new Mock<IUserRepository>();
            userRepoDouble.Setup(urd => urd.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((IUser?)null);

            PeriodDateTime periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

            var pmFactory = new ProjectManagerFactory(userRepoDouble.Object);

            // assert
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
            () =>
                //act
                pmFactory.Create(It.IsAny<long>(), periodDateTime)
            );
            Assert.Equal("User does not exist", exception.Message);
        }

        [Fact]
        public async Task WhenDeactivationDateIsBeforeInitDate_ThenThrowsException()
        {
            // arrange
            var userDouble = new Mock<IUser>();
            userDouble.Setup(ud => ud.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(true);
            userDouble.Setup(ud => ud.IsDeactivated()).Returns(false);

            PeriodDateTime periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

            var userRepoDouble = new Mock<IUserRepository>();
            userRepoDouble.Setup(urd => urd.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(userDouble.Object);

            var pmFactory = new ProjectManagerFactory(userRepoDouble.Object);

            // assert
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
            () =>
                //act
                pmFactory.Create(It.IsAny<long>(), periodDateTime)
            );
            Assert.Equal("Deactivation date is before init date", exception.Message);
        }

        [Fact]
        public async Task WhenUserIsDeativated_ThenThrowsException()
        {
            // arrange
            var userDouble = new Mock<IUser>();
            userDouble.Setup(ud => ud.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
            userDouble.Setup(ud => ud.IsDeactivated()).Returns(true);

            PeriodDateTime periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

            var userRepoDouble = new Mock<IUserRepository>();
            userRepoDouble.Setup(urd => urd.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(userDouble.Object);

            var pmFactory = new ProjectManagerFactory(userRepoDouble.Object);

            // assert
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
            () =>
                //act
                pmFactory.Create(It.IsAny<long>(), periodDateTime)
            );
            Assert.Equal("User is deactivated", exception.Message);
        }



        // tests for creating with user id and dateTime
        [Fact]
        public async Task WhenPassingValidUserIdAndInitDate_ThenCreatesProjectManager()
        {
            // arrange
            var userDouble = new Mock<IUser>();
            userDouble.Setup(ud => ud.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
            userDouble.Setup(ud => ud.IsDeactivated()).Returns(false);

            PeriodDateTime periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

            var userRepoDouble = new Mock<IUserRepository>();
            userRepoDouble.Setup(urd => urd.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(userDouble.Object);

            var pmFactory = new ProjectManagerFactory(userRepoDouble.Object);

            // act
            var result = await pmFactory.Create(It.IsAny<long>(), periodDateTime);

            // assert
            Assert.NotNull(result);
        }

        // test for visitor
        [Fact]
        public void WhenPassingVisitor_ThenCreatesProjectManager()
        {
            // arrange
            var visitorDouble = new Mock<IProjectManagerVisitor>();
            var userRepoDouble = new Mock<IUserRepository>();
            var pmFactory = new ProjectManagerFactory(userRepoDouble.Object);

            // act
            var result = pmFactory.Create(visitorDouble.Object);

            //assert
            Assert.NotNull(result);
        }
    }
}