using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HRManagerTests;

public class Constructor
{
    [Fact]
    public void WhenCreatingHRManagerWithValidPeriod_ThenHRManagerIsCreatedCorrectly()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        //act
        new HRManager(user.Object, periodDateTime.Object);
        //assert
    }

    [Fact]
    public void WhenCreatingHRManagerWithValidInitDate_ThenHRManagerIsCreatedCorrectly()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        //act
        new HRManager(user.Object, It.IsAny<DateTime>());
        //assert
    }

    [Fact]
    public void WhenCreatingHRManagerWhereFinalDateIsAfterDeactivationDate_ThenShouldThrowException()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                new HRManager(user.Object, periodDateTime.Object)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenCreatingHRManagerWhereUserIsDeactivated_ThenShowThrowException()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(true);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                new HRManager(user.Object, periodDateTime.Object)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }

}


