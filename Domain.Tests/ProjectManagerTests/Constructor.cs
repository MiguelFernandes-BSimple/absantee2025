using Domain.Interfaces;
using Domain.Models;
using Moq;


public class Constructor
{
    public static IEnumerable<object[]> GetProjectManager_WithValidDates()
    {
        yield return new object[] { DateTime.Now, DateTime.Now.AddDays(1) };
        yield return new object[] { DateTime.Now, DateTime.Now.AddYears(3) };
        yield return new object[] { DateTime.Today, DateTime.Today };
        yield return new object[] { DateTime.Now, null! };
    }

    [Theory]
    [MemberData(nameof(GetProjectManager_WithValidDates))]
    public void WhenGivenValidFields_ThenProjectManagerIsInstantiated(DateTime initDate, DateTime endDate)
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetInitDate()).Returns(initDate);
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(endDate);

        //act
        new ProjectManager(user.Object, periodDateTime.Object);

        //assert
    }


    public static IEnumerable<object[]> GetProjectManager_WithInvalidDates()
    {
        yield return new object[] { DateTime.Now.AddDays(5), DateTime.Now.AddDays(1) };
        yield return new object[] { DateTime.Now.AddYears(-1), DateTime.Now.AddYears(-3) };
    }

    [Theory]
    [MemberData(nameof(GetProjectManager_WithInvalidDates))]
    public void WhenGivenInvalidDates_ThenExceptionIsThrown(DateTime initDate, DateTime endDate)
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(endDate)).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetInitDate()).Returns(initDate);
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(endDate);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new ProjectManager(user.Object, periodDateTime.Object));

        Assert.Equal("Invalid arguments.", exception.Message);
    }

    [Fact]
    public void WhenGivenProjectManagerFinalDateAfterUserDeactivationDate_ThenExceptionIsThrown()
    {
        //arrange
        DateTime initDate = DateTime.Now;
        DateTime endDate = DateTime.Now.AddDays(5);

        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(endDate)).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetInitDate()).Returns(initDate);
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(endDate);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new ProjectManager(user.Object, periodDateTime.Object));

        Assert.Equal("Invalid arguments.", exception.Message);
    }

    [Fact]
    public void WhenGivenInactiveUser_ThenExceptionIsThrown()
    {
        //arrange
        DateTime initDate = DateTime.Now;
        DateTime endDate = DateTime.Now.AddDays(5);

        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(endDate)).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(true);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetInitDate()).Returns(initDate);
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(endDate);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new ProjectManager(user.Object, periodDateTime.Object));

        Assert.Equal("Invalid arguments.", exception.Message);
    }
}
