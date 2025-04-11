using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HRManagerTests;

public class Constructor
{
    [Fact]
    public void WhenCreatingHRManagerWithValidPeriod_ThenHRManagerIsCreatedCorrectly()
    {

        long id = 1;

        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        //act
        new HRManager(id, periodDateTime.Object);
        //assert
    }

    [Fact]
    public void WhenCreatingHRManagerWithValidInitDate_ThenHRManagerIsCreatedCorrectly()
    {
        long id = 2;
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        //act
        new HRManager(id, periodDateTime.Object);
        //assert
    }

    [Fact]
    public void WhenCreatingHRManagerWhereFinalDateIsAfterDeactivationDate_ThenShouldThrowException()
    {

        long id = 3;
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
                new HRManager(id, periodDateTime.Object)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenCreatingHRManagerWhereUserIsDeactivated_ThenShowThrowException()
    {

        long id = 4;
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
                new HRManager(id, periodDateTime.Object)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }

}


