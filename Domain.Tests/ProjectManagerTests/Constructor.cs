using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectManagerTests;

public class Constructor
{
    [Fact]
    public void WhenGivenValidFields_ThenProjectManagerIsInstantiated()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.IsFinalDateUndefined()).Returns(false);

        //act
        new ProjectManager(user.Object, periodDateTime.Object);

        //assert
    }

    [Fact]
    public void WhenDeactivationDateIsNull_ThenProjectManagerIsInstantiated()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.IsFinalDateUndefined()).Returns(true);

        //act
        new ProjectManager(user.Object, periodDateTime.Object);

        //assert
    }

    [Fact]
    public void WhenGivenProjectManagerFinalDateAfterUserDeactivationDate_ThenExceptionIsThrown()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.IsFinalDateUndefined()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new ProjectManager(user.Object, periodDateTime.Object));

        Assert.Equal("Invalid Arguments.", exception.Message);
    }

    [Fact]
    public void WhenGivenInactiveUser_ThenExceptionIsThrown()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(true);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.IsFinalDateUndefined()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new ProjectManager(user.Object, periodDateTime.Object));

        Assert.Equal("Invalid Arguments.", exception.Message);
    }
}
