using Domain;
using Moq;

public class HRManagerTest{
    public static IEnumerable<object[]> GetHRManagerData_ValidData()
    {
        yield return new object[] { DateTime.Now, DateTime.Now.AddDays(10) };
        yield return new object[] { DateTime.Now.AddDays(10), DateTime.Now.AddDays(20) };
        yield return new object[] { DateTime.Now.AddYears(1), DateTime.Now.AddYears(2) };
        yield return new object[] { DateTime.Now, null! };
        yield return new object[] { DateTime.Now, DateTime.Now };
        yield return new object[] { DateTime.Now, DateTime.MaxValue };
        yield return new object[] { DateTime.MinValue, DateTime.Now };
        yield return new object[] { DateTime.MinValue, DateTime.MaxValue };
    }

    public static IEnumerable<object[]> GetHRManagerData_InvalidData()
    {
        yield return new object[] { DateTime.Now, DateTime.Now.AddDays(-10) };
        yield return new object[] { DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-20) };
        yield return new object[] { DateTime.Now.AddYears(-1), DateTime.Now.AddYears(-2) };
        yield return new object[] { DateTime.MaxValue, DateTime.MinValue };
        yield return new object[] { DateTime.MinValue, DateTime.MinValue };
        yield return new object[] { DateTime.MaxValue, DateTime.MaxValue };
        yield return new object[] { DateTime.MaxValue, null! };
    }

    [Theory]
    [MemberData(nameof(GetHRManagerData_ValidData))]
    public void WhenCreatingHRManagerWithValidData_ThenShouldBeInstantiated(DateTime dataInicio, DateTime? dataFim){
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBeforeThen(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        //act
        new RHManager(user.Object, dataInicio, dataFim);

        //assert
    }

    [Theory]
    [MemberData(nameof(GetHRManagerData_InvalidData))]
    public void WhenCreatingHRManagerWithInvalidData_ThenThrowsException(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBeforeThen(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new RHManager(user.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetHRManagerData_ValidData))]
    [MemberData(nameof(GetHRManagerData_InvalidData))]
    public void WhenCreatingHRManagerWithEndDateAfterDeactivationData_ThenThrowsException(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBeforeThen(dataFim)).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new RHManager(user.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetHRManagerData_ValidData))]
    [MemberData(nameof(GetHRManagerData_InvalidData))]
    public void WhenCreatingRHManagerWithInactiveUser_ThenThrowsException(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBeforeThen(dataFim)).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new RHManager(user.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetHRManagerData_ValidData))]
    [MemberData(nameof(GetHRManagerData_InvalidData))]
    public void WhenCreatingHRManagerWithEndDateAfterDeactivationDataAndInactiveUser_ThenThrowsException(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBeforeThen(dataFim)).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new RHManager(user.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    } 

}
