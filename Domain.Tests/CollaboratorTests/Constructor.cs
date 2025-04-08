using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.CollaboratorTests;
public class Constructor
{
    [Fact]
    public void WhenCreatingCollaboratorWithValidPeriod_ThenCollaboratorIsCreatedCorrectly()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        //act
        new Collaborator(user.Object, periodDateTime.Object);
        //assert
    }

    [Fact]
    public void WhenCreatingCollaboratorWithValidInitDate_ThenCollaboratorIsCreatedCorrectly()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        //act
        new Collaborator(user.Object, It.IsAny<IPeriodDateTime>());
        //assert
    }

    [Fact]
    public void WhenCreatingCollaboratorWhereFinalDateIsAfterDeactivationDate_ThenShouldThrowException()
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
                new Collaborator(user.Object, periodDateTime.Object)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenCreatingCollaboratorWhereUserIsDeactivated_ThenShowThrowException()
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
                new Collaborator(user.Object, periodDateTime.Object)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }
}

