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

    [Fact]
    public void WhenHasNamesGetsCorrectName_ReturnTrue()
    {
        //Arrange
        // User double 
        Mock<IUser> doubleUser = new Mock<IUser>();

        // Name of the user
        string names = "First Name";
        // The user has the name defined 
        doubleUser.Setup(u => u.HasNames(names)).Returns(true);

        DateTime colaboratorInitDate = new DateTime(2020, 1, 1);
        DateTime colaboratorFinalDate = new DateTime(2021, 1, 1);

        Colaborator colaborator = new Colaborator(doubleUser.Object, colaboratorInitDate, colaboratorFinalDate);

        // Assert 
        Assert.True(
            //Act 
            colaborator.HasNames(names)
        );
    }

    [Fact]
    public void WhenHasNamesGetsWrongName_ReturnFalse()
    {
        //Arrange
        // User double 
        Mock<IUser> doubleUser = new Mock<IUser>();

        // Name of the user
        string names = "First Name";
        // The user has the name defined 
        doubleUser.Setup(u => u.HasNames(names)).Returns(false);

        DateTime colaboratorInitDate = new DateTime(2020, 1, 1);
        DateTime colaboratorFinalDate = new DateTime(2021, 1, 1);

        Colaborator colaborator = new Colaborator(doubleUser.Object, colaboratorInitDate, colaboratorFinalDate);

        // Assert 
        Assert.False(
            //Act 
            colaborator.HasNames(names)
        );
    }

    [Fact]
    public void WhenHasSurnamesGetsCorrectSurname_ReturnTrue()
    {
        //Arrange
        // User double 
        Mock<IUser> doubleUser = new Mock<IUser>();

        // Name of the user
        string surnames = "Surnames";
        // The user has the name defined 
        doubleUser.Setup(u => u.HasSurnames(surnames)).Returns(true);

        DateTime colaboratorInitDate = new DateTime(2020, 1, 1);
        DateTime colaboratorFinalDate = new DateTime(2021, 1, 1);

        Colaborator colaborator = new Colaborator(doubleUser.Object, colaboratorInitDate, colaboratorFinalDate);

        // Assert 
        Assert.True(
            //Act 
            colaborator.HasSurnames(surnames)
        );
    }

    [Fact]
    public void WhenHasSurnamesGetsWrongName_ReturnFalse()
    {
        //Arrange
        // User double 
        Mock<IUser> doubleUser = new Mock<IUser>();

        // Name of the user
        string names = "Surnames";
        // The user has the name defined 
        doubleUser.Setup(u => u.HasSurnames(names)).Returns(false);

        DateTime colaboratorInitDate = new DateTime(2020, 1, 1);
        DateTime colaboratorFinalDate = new DateTime(2021, 1, 1);

        Colaborator colaborator = new Colaborator(doubleUser.Object, colaboratorInitDate, colaboratorFinalDate);

        // Assert 
        Assert.False(
            //Act 
            colaborator.HasSurnames(names)
        );
    }
}
