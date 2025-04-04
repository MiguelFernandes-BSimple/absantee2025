using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectManagerTests;

public class Constructor
{
    [Fact]
    public void WhenCreatingProjectManagerWithValidPeriod_ThenProjectManagerIsCreatedCorrectly()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        //act
        new ProjectManager(user.Object, periodDateTime.Object);
        //assert
    }

    [Fact]
    public void WhenCreatingProjectManagerWithValidInitDate_ThenProjectManagerIsCreatedCorrectly()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        //act
        new ProjectManager(user.Object, It.IsAny<DateTime>());
        //assert
    }

    [Fact]
    public void WhenCreatingProjectManagerWhereFinalDateIsAfterDeactivationDate_ThenShouldThrowException()
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
                new ProjectManager(user.Object, periodDateTime.Object)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenCreatingProjectManagerWhereUserIsDeactivated_ThenShowThrowException()
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
                new ProjectManager(user.Object, periodDateTime.Object)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }
}
