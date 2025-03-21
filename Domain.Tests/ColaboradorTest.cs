using Domain;
using Moq;

public class ColaboradorTest
{
    public static IEnumerable<object[]> MethodGetToTestIfDateFieldsAreValidToColaborator()
    {
        yield return new object[] { DateTime.Now, DateTime.Now.AddDays(1) };
        yield return new object[] { DateTime.Now.AddYears(-1), DateTime.Now.AddYears(2) };
        yield return new object[] { DateTime.Now.AddYears(-1), null! };
    }

    [Theory]
    [MemberData(nameof(MethodGetToTestIfDateFieldsAreValidToColaborator))]
    public void WhenCreatingColaboratorWIthValidData_ThenSeeIfColaboratorIsCreatedCorrectly(
        DateTime _initDate,
        DateTime? _finalDate
    )
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBeforeThen(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);
        //act
        new Colaborator(user.Object, _initDate, _finalDate);
        //assert
    }

    public static IEnumerable<object[]> MethodGetToTestIfDateFieldsAreInvalidToColaborator()
    {
        yield return new object[] { DateTime.Now.AddDays(5), DateTime.Now.AddDays(1) };
        yield return new object[] { DateTime.Now.AddYears(-1), DateTime.Now.AddYears(-3) };
    }

    [Theory]
    [MemberData(nameof(MethodGetToTestIfDateFieldsAreInvalidToColaborator))]
    public void WhenCreatingColaboratorWIthInValidData_ThenSeeIfShowTheThrowException(
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBeforeThen(_finalDate)).Returns(false);
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
    [MemberData(nameof(MethodGetToTestIfDateFieldsAreValidToColaborator))]
    public void WhenCreatingColaboratorWhereFinalDateIsAfterDeactivationDate_ThenShouldThrowException(
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBeforeThen(_finalDate)).Returns(true);
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
    [MemberData(nameof(MethodGetToTestIfDateFieldsAreValidToColaborator))]
    public void WhenCreatingColaboratorWhereUserIsDeactivated_ThenShowThrowException(
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBeforeThen(_finalDate)).Returns(false);
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
    [MemberData(nameof(MethodGetToTestIfDateFieldsAreInvalidToColaborator))]
    public void WhenCreatingColaboratorWhereInputsAreInvalid_ThenShouldThrowException(
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBeforeThen(_finalDate)).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(true);
        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                new Colaborator(user.Object, _initDate, _finalDate)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> ValidRangeDatesToColaborator()
    {
        yield return new object[] { new DateTime(2023, 6, 1), new DateTime(2024, 6, 1) };
        yield return new object[] { new DateTime(2022, 12, 31), new DateTime(2023, 1, 1) };
        yield return new object[] { new DateTime(2025, 1, 1), new DateTime(2025, 12, 31) };
        yield return new object[] { new DateTime(2023, 1, 1), new DateTime(2025, 1, 1) };
        yield return new object[] { new DateTime(2024, 1, 1), new DateTime(2024, 12, 31) };
        yield return new object[] { new DateTime(2024, 1, 1), new DateTime(2024, 1, 1) };
    }

    [Theory]
    [MemberData(nameof(ValidRangeDatesToColaborator))]
    public void WhenComparingInitDateWithFinalDate_ThenSeeIfTheResultIsSucess(
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        // Arrange
        Mock<IUser> user = new Mock<IUser>();
        Colaborator colaborator = new Colaborator(user.Object, _initDate, _finalDate);
        // Act
        bool result = colaborator.IsInside(_initDate, _finalDate);
        // Assert
        Assert.True(result);
    }

    public static IEnumerable<object[]> InvalidDateRangesToColaborator()
    {
        yield return new object[] { new DateTime(2027, 6, 1), new DateTime(2024, 6, 1) };
        yield return new object[] { new DateTime(2023, 12, 30), new DateTime(2022, 12, 31) };
    }

    [Theory]
    [MemberData(nameof(InvalidDateRangesToColaborator))]
    public void WhenComparingInvalidInitDateWithFinalDate_ThenShouldThrowException(
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        // Arrange
        Mock<IUser> user = new Mock<IUser>();
        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => new Colaborator(user.Object, _initDate, _finalDate)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }
}
