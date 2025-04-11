using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.ProjectManagerTests
{
    public class Factory
    {
        // tests for creating with user id and period date time
        [Fact]
        public void WhenPassingValidUserIdAndPeriodDateTime_ThenCreatesProjectManager()
        {
            // arrange
            var userDouble = new Mock<IUser>();
            userDouble.Setup(ud => ud.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
            userDouble.Setup(ud => ud.IsDeactivated()).Returns(false);

            var periodDateTimeDouble = new Mock<IPeriodDateTime>();
            periodDateTimeDouble.Setup(pdtd => pdtd.GetInitDate()).Returns(It.IsAny<DateTime>());

            var userRepoDouble = new Mock<IUserRepository>();
            userRepoDouble.Setup(urd => urd.GetById(It.IsAny<long>())).Returns(userDouble.Object);

            var pmFactory = new ProjectManagerFactory(userRepoDouble.Object);

            // act
            var result = pmFactory.Create(It.IsAny<long>(), periodDateTimeDouble.Object);

            // assert
        }

        [Fact]
        public void WhenUserIdDoesntExist_ThenThrowsException()
        {
            var userRepoDouble = new Mock<IUserRepository>();
            userRepoDouble.Setup(urd => urd.GetById(It.IsAny<long>())).Returns((IUser?)null);

            var pmFactory = new ProjectManagerFactory(userRepoDouble.Object);

            // assert
            ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                pmFactory.Create(It.IsAny<long>(), It.IsAny<IPeriodDateTime>())
            );
            Assert.Equal("User does not exist", exception.Message);
        }

        [Fact]
        public void WhenDeactivationDateIsBeforeInitDate_ThenThrowsException()
        {
            // arrange
            var userDouble = new Mock<IUser>();
            userDouble.Setup(ud => ud.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(true);
            userDouble.Setup(ud => ud.IsDeactivated()).Returns(false);

            var periodDateTimeDouble = new Mock<IPeriodDateTime>();
            periodDateTimeDouble.Setup(pdtd => pdtd.GetInitDate()).Returns(It.IsAny<DateTime>());

            var userRepoDouble = new Mock<IUserRepository>();
            userRepoDouble.Setup(urd => urd.GetById(It.IsAny<long>())).Returns(userDouble.Object);

            var pmFactory = new ProjectManagerFactory(userRepoDouble.Object);

            // assert
            ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                pmFactory.Create(It.IsAny<long>(), periodDateTimeDouble.Object)
            );
            Assert.Equal("Deactivation date is before init date", exception.Message);
        }

        [Fact]
        public void WhenUserIsDeativated_ThenThrowsException()
        {
            // arrange
            var userDouble = new Mock<IUser>();
            userDouble.Setup(ud => ud.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
            userDouble.Setup(ud => ud.IsDeactivated()).Returns(true);

            var periodDateTimeDouble = new Mock<IPeriodDateTime>();
            periodDateTimeDouble.Setup(pdtd => pdtd.GetInitDate()).Returns(It.IsAny<DateTime>());

            var userRepoDouble = new Mock<IUserRepository>();
            userRepoDouble.Setup(urd => urd.GetById(It.IsAny<long>())).Returns(userDouble.Object);

            var pmFactory = new ProjectManagerFactory(userRepoDouble.Object);

            // assert
            ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                pmFactory.Create(It.IsAny<long>(), periodDateTimeDouble.Object)
            );
            Assert.Equal("User is deactivated", exception.Message);
        }



        // tests for creating with user id and dateTime
        [Fact]
        public void WhenPassingValidUserIdAndInitDate_ThenCreatesProjectManager()
        {
            // arrange
            var userDouble = new Mock<IUser>();
            userDouble.Setup(ud => ud.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
            userDouble.Setup(ud => ud.IsDeactivated()).Returns(false);

            var userRepoDouble = new Mock<IUserRepository>();
            userRepoDouble.Setup(urd => urd.GetById(It.IsAny<long>())).Returns(userDouble.Object);

            var pmFactory = new ProjectManagerFactory(userRepoDouble.Object);

            // act
            var result = pmFactory.Create(It.IsAny<long>(), It.IsAny<DateTime>());

            // assert
        }

        [Fact]
        public void WhenUserIdDoesNotExist_ThenThrowsException()
        {
            var userRepoDouble = new Mock<IUserRepository>();
            userRepoDouble.Setup(urd => urd.GetById(It.IsAny<long>())).Returns((IUser?)null);

            var pmFactory = new ProjectManagerFactory(userRepoDouble.Object);

            // assert
            ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                pmFactory.Create(It.IsAny<long>(), It.IsAny<DateTime>())
            );
            Assert.Equal("User does not exist", exception.Message);
        }

        [Fact]
        public void WhenDeactivationDateIsBeforeInitDate_ThenThrowException()
        {
            // arrange
            var userDouble = new Mock<IUser>();
            userDouble.Setup(ud => ud.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(true);
            userDouble.Setup(ud => ud.IsDeactivated()).Returns(false);

            var userRepoDouble = new Mock<IUserRepository>();
            userRepoDouble.Setup(urd => urd.GetById(It.IsAny<long>())).Returns(userDouble.Object);

            var pmFactory = new ProjectManagerFactory(userRepoDouble.Object);

            // assert
            ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                pmFactory.Create(It.IsAny<long>(), It.IsAny<DateTime>())
            );
            Assert.Equal("Deactivation date is before init date", exception.Message);
        }

        [Fact]
        public void WhenUserIsDeativated_ThenThrowException()
        {
            // arrange
            var userDouble = new Mock<IUser>();
            userDouble.Setup(ud => ud.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
            userDouble.Setup(ud => ud.IsDeactivated()).Returns(true);

            var userRepoDouble = new Mock<IUserRepository>();
            userRepoDouble.Setup(urd => urd.GetById(It.IsAny<long>())).Returns(userDouble.Object);

            var pmFactory = new ProjectManagerFactory(userRepoDouble.Object);

            // assert
            ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                pmFactory.Create(It.IsAny<long>(), It.IsAny<DateTime>())
            );
            Assert.Equal("User is deactivated", exception.Message);
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
        }
    }
}