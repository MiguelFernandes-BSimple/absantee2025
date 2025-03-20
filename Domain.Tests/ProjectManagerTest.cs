using Domain;
using Moq;

public class ProjectManagerTest
{
    public static IEnumerable<object[]> GetProjectManager_WithValidDates()
    {
        yield return new object[] { DateTime.Now, DateTime.Now.AddDays(1) };
        yield return new object[] { DateTime.Now, DateTime.Now.AddYears(3) };
        yield return new object[] { DateTime.Now, null! };
    }

    [Theory]
    [MemberData(nameof(GetProjectManager_WithValidDates))]
    public void WhenGivenValidFields_ThenProjectManagerIsInstantiated(DateTime initDate, DateTime? endDate)
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBeforeThen(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        //act
        new ProjectManager(user.Object, initDate, endDate);

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
        user.Setup(u => u.DeactivationDateIsBeforeThen(endDate)).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new ProjectManager(user.Object, initDate, endDate));

        Assert.Equal("Invalid arguments.", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetProjectManager_WithValidDates))]
    public void WhenGivenProjectManagerFinalDateAfterUserDeactivationDate_ThenExceptionIsThrown(DateTime initDate, DateTime endDate)
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBeforeThen(endDate)).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new ProjectManager(user.Object, initDate, endDate));

        Assert.Equal("Invalid arguments.", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetProjectManager_WithValidDates))]
    public void WhenGivenInactiveUser_ThenExceptionIsThrown(DateTime initDate, DateTime endDate)
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBeforeThen(endDate)).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new ProjectManager(user.Object, initDate, endDate));

        Assert.Equal("Invalid arguments.", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetProjectManager_WithInvalidDates))]
    public void WhenGivenInvalidInputs_ThenExceptionIsThrown(DateTime initDate, DateTime endDate)
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBeforeThen(endDate)).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new ProjectManager(user.Object, initDate, endDate));

        Assert.Equal("Invalid arguments.", exception.Message);
    }
}