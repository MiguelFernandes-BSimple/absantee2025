using Domain;
using Moq;

public class ColaboratorTest
{
    public static IEnumerable<object[]> ColaboratorData_ValidFields()
    {
        yield return new object[] { DateTime.Now, DateTime.Now.AddDays(1) };
        yield return new object[] { DateTime.Now.AddYears(-1), DateTime.Now.AddYears(2) };
        yield return new object[] { DateTime.Today, DateTime.Today };
    }

    [Theory]
    [MemberData(nameof(ColaboratorData_ValidFields))]
    public void WhenCreatingColaboratorWIthValidData_ThenColaboratorIsCreatedCorrectly(
        DateTime _initDate,
        DateTime? _finalDate
    )
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);
        //act
        new Colaborator(user.Object, _initDate, _finalDate);
        //assert
    }

    public static IEnumerable<object[]> ColaboratorData_InvalidFields()
    {
        yield return new object[] { DateTime.Now.AddDays(5), DateTime.Now.AddDays(1) };
        yield return new object[] { DateTime.Now.AddYears(-1), DateTime.Now.AddYears(-3) };
        yield return new object[] { DateTime.Now.AddYears(-1), DateTime.Now.AddYears(-3) };
    }

    [Theory]
    [MemberData(nameof(ColaboratorData_InvalidFields))]
    public void WhenCreatingColaboratorWIthInValidData_ThenShowTheThrowException(
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(_finalDate)).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);
        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                new Colaborator(user.Object, _initDate, _finalDate)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(ColaboratorData_ValidFields))]
    public void WhenCreatingColaboratorWhereFinalDateIsAfterDeactivationDate_ThenShouldThrowException(
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(_finalDate)).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(false);
        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                new Colaborator(user.Object, _initDate, _finalDate)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(ColaboratorData_ValidFields))]
    public void WhenCreatingColaboratorWhereUserIsDeactivated_ThenShowThrowException(
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(_finalDate)).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(true);
        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                new Colaborator(user.Object, _initDate, _finalDate)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(ColaboratorData_InvalidFields))]
    public void WhenCreatingColaboratorWhereInputsAreInvalid_ThenShouldThrowException(
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(_finalDate)).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(true);
        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                new Colaborator(user.Object, _initDate, _finalDate)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> ContainsDates_ValidDates()
    {
        yield return new object[] { new DateTime(2020, 1, 1), new DateTime(2021, 1, 1) };
        yield return new object[] { new DateTime(2020, 1, 2), new DateTime(2020, 12, 31) };
    }

    [Theory]
    [MemberData(nameof(ContainsDates_ValidDates))]
    public void WhenPassingValidDatesToContainsDates_ThenResultIsTrue(
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        // Arrange
        Mock<IUser> user = new Mock<IUser>();
        DateTime colaboratorInitDate = new DateTime(2020, 1, 1);
        DateTime colaboratorFinalDate = new DateTime(2021, 1, 1);
        Colaborator colaborator = new Colaborator(user.Object, colaboratorInitDate, colaboratorFinalDate);
        // Act
        bool result = colaborator.ContainsDates(_initDate, _finalDate);
        // Assert
        Assert.True(result);
    }

    public static IEnumerable<object[]> ContainsDates_InvalidDates()
    {
        yield return new object[] { new DateTime(2019, 1, 1), new DateTime(2021, 1, 1) };
        yield return new object[] { new DateTime(2020, 1, 2), new DateTime(2022, 12, 31) };
    }

    [Theory]
    [MemberData(nameof(ContainsDates_InvalidDates))]
    public void WhenPassingInvalidDatesToContainsDates_ThenResultIsFalse(
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        // Arrange
        Mock<IUser> user = new Mock<IUser>();
        DateTime colaboratorInitDate = new DateTime(2020, 1, 1);
        DateTime colaboratorFinalDate = new DateTime(2021, 1, 1);
        Colaborator colaborator = new Colaborator(user.Object, colaboratorInitDate, colaboratorFinalDate);
        // Act
        bool result = colaborator.ContainsDates(_initDate, _finalDate);
        // Assert
        Assert.False(result);
    }

}
