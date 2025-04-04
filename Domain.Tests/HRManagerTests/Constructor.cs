using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HRManagerTests;

public class Constructor
{
    // tests for constructor with user and period

    [Fact]
    public void WhenCreatingHRManagerWithValidData_ThenShouldBeInstantiated()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();

        //act
        new HRManager(user.Object, periodDateTime.Object);

        //assert
    }

    [Fact]
    public void WhenCreatingHRManagerWithEndDateAfterDeactivationDate_ThenThrowsException()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new HRManager(user.Object, periodDateTime.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenCreatingHRManagerWithInactiveUser_ThenThrowsException()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(true);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new HRManager(user.Object, periodDateTime.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }


    // tests for constructor with user and datetime
    [Fact]
    public void WhenPassingValidArguments_ThenCreatesNewHRManager()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        //act
        new HRManager(user.Object, It.IsAny<DateTime>());

        //assert
    }

    [Fact]
    public void WhenEndDateAfterDeactivationDate_ThenThrowsException()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new HRManager(user.Object, It.IsAny<DateTime>()));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenInactiveUser_ThenThrowsException()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(true);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new HRManager(user.Object, It.IsAny<DateTime>()));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

}


