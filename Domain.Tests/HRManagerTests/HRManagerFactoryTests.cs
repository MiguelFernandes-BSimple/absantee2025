using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.HRManagerTests;

public class HRManagerFactoryTests
{
    [Fact]
    public async Task WhenPassingValidUserIdAndPeriodDateTime_ThenCreatesHRManager()
    {
        //arrange
        var userDouble = new Mock<IUser>();
        userDouble.Setup(ud => ud.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        userDouble.Setup(ud => ud.IsDeactivated()).Returns(false);

        PeriodDateTime periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

        var userRepoDouble = new Mock<IUserRepository>();
        userRepoDouble.Setup(urd => urd.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(userDouble.Object);

        var hrFactory = new HRManagerFactory(userRepoDouble.Object);

        //act
        var result = await hrFactory.Create(It.IsAny<long>(), periodDateTime);
        //assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task WhenUserIdDoesntExist_ThenThrowsException()
    {
        var userRepoDouble = new Mock<IUserRepository>();
        userRepoDouble.Setup(urd => urd.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((IUser?)null);

        var hrFactory = new HRManagerFactory(userRepoDouble.Object);

        // assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
        () =>
            //act
            hrFactory.Create(It.IsAny<long>(), It.IsAny<PeriodDateTime>())
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
        userRepoDouble.Setup(urd => urd.GetByIdAsync(1)).ReturnsAsync(userDouble.Object);

        var hrFactory = new HRManagerFactory(userRepoDouble.Object);

        // assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
        () =>
            //act
            hrFactory.Create(1, periodDateTime)
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

        var hrFactory = new HRManagerFactory(userRepoDouble.Object);

        // assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
        () =>
            //act
            hrFactory.Create(It.IsAny<long>(), periodDateTime)
        );
        Assert.Equal("User is deactivated", exception.Message);
    }
    [Fact]
    public async Task WhenPassingValidUserIdAndInitDate_ThenCreatesHRManager()
    {
        // arrange
        var userDouble = new Mock<IUser>();
        userDouble.Setup(ud => ud.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        userDouble.Setup(ud => ud.IsDeactivated()).Returns(false);

        var userRepoDouble = new Mock<IUserRepository>();
        userRepoDouble.Setup(urd => urd.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(userDouble.Object);

        var hrFactory = new HRManagerFactory(userRepoDouble.Object);

        // act
        var result = await hrFactory.Create(It.IsAny<long>(), It.IsAny<DateTime>());

        // assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenCreatingHRManagerWithIHRManagerVisitor_ThenCreatesHRManager()
    {
        //arrange
        var hrManagerVisitor = new Mock<IHRManagerVisitor>();

        hrManagerVisitor.Setup(hr => hr.Id).Returns(It.IsAny<long>());
        hrManagerVisitor.Setup(hr => hr.UserId).Returns(It.IsAny<long>());
        hrManagerVisitor.Setup(hr => hr.PeriodDateTime).Returns(It.IsAny<PeriodDateTime>());

        var userRepoDouble = new Mock<IUserRepository>();

        var hrFactory = new HRManagerFactory(userRepoDouble.Object);

        //act
        var result = hrFactory.Create(hrManagerVisitor.Object);

        //assert
        Assert.NotNull(result);
    }



}