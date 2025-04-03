using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.CollaboratorTests;
public class Constructor
{
    public static IEnumerable<object[]> CollaboratorData_ValidFields()
    {
        yield return new object[] { DateTime.Now, DateTime.Now.AddDays(1) };
        yield return new object[] { DateTime.Now.AddYears(-1), DateTime.Now.AddYears(2) };
        yield return new object[] { DateTime.Today, DateTime.Today };
    }

    [Theory]
    [MemberData(nameof(CollaboratorData_ValidFields))]
    public void WhenCreatingCollaboratorWIthValidData_ThenCollaboratorIsCreatedCorrectly(
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetInitDate()).Returns(_initDate);
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(_finalDate);

        //act
        new Collaborator(user.Object, periodDateTime.Object);
        //assert
    }

    [Theory]
    [MemberData(nameof(CollaboratorData_ValidFields))]
    public void WhenCreatingCollaboratorWhereFinalDateIsAfterDeactivationDate_ThenShouldThrowException(
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(_finalDate)).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetInitDate()).Returns(_initDate);
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(_finalDate);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                new Collaborator(user.Object, periodDateTime.Object)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(CollaboratorData_ValidFields))]
    public void WhenCreatingCollaboratorWhereUserIsDeactivated_ThenShowThrowException(
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(_finalDate)).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(true);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetInitDate()).Returns(_initDate);
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(_finalDate);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                new Collaborator(user.Object, periodDateTime.Object)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(CollaboratorData_ValidFields))]
    public void WhenCreatingCollaboratorWhereFinalDateIsAfterDeactivationDateAndUserIsDeactivated_ThenShouldThrowException(
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(_finalDate)).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(true);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetInitDate()).Returns(_initDate);
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(_finalDate);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                new Collaborator(user.Object, periodDateTime.Object)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }
}

