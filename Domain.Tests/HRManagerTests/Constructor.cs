using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HRManagerTests;

public class Constructor
{

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
        DateTime dataInicio = DateTime.Now;
        DateTime dataFim = DateTime.Now.AddDays(10);

        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(dataFim)).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(true);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetInitDate()).Returns(dataInicio);
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(dataFim);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new HRManager(user.Object, periodDateTime.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenCreatingHRManagerWithEndDateAfterDeactivationDataAndInactiveUser_ThenThrowsException()
    {
        //arrange
        DateTime dataInicio = DateTime.Now;
        DateTime dataFim = DateTime.Now.AddDays(10);

        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(dataFim)).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(true);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetInitDate()).Returns(dataInicio);
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(dataFim);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new HRManager(user.Object, periodDateTime.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }
}


