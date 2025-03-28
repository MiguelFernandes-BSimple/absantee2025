using Domain;

namespace Domain.Tests;

public class UserTest
{
    public static IEnumerable<object[]> GetUserData_ValidFields()
    {
        yield return new object[] { "John", "Doe", "john@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "John", "Doe", "john.doe@email.com", null! };
        yield return new object[] { "John Peter", "Doe", "john.doe.13@email.com", DateTime.Now.AddYears(1) };
        yield return new object[] { "John", "Wallace Doe", "john.doe@company.com.pt", DateTime.Now.AddYears(2) };
    }

    [Theory]
    [MemberData(nameof(GetUserData_ValidFields))]
    public void WhenCreatingUserWithValidFields_ThenNoExceptionIsThrown(string firstName, string lastName, string email, DateTime? deactivationDate)
    {
        // Act
        new User(firstName, lastName, email, deactivationDate);

        // Assert - No exception should be thrown
    }

    public static IEnumerable<object[]> GetUserData_InvalidFirstNames()
    {
        yield return new object[] { new string('a', 51), "Doe", "john@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "", "Doe", "john@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "John 13", "Doe", "john@email.com", null! };
    }

    [Theory]
    [MemberData(nameof(GetUserData_InvalidFirstNames))]
    public void WhenCreatingUserWithInvalidFirstName_ThenThrowsArgumentException(string firstName, string lastName, string email, DateTime? deactivationDate)
    {
        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new User(firstName, lastName, email, deactivationDate));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> GetUserData_InvalidLastNames()
    {
        yield return new object[] { "John", new string('a', 51), "john@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "John", "Doe 13", "john@email.com", null! };
    }

    [Theory]
    [MemberData(nameof(GetUserData_InvalidLastNames))]
    public void WhenCreatingUserWithInvalidLastName_ThenThrowsArgumentException(string firstName, string lastName, string email, DateTime? deactivationDate)
    {
        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new User(firstName, lastName, email, deactivationDate));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> GetUserData_InvalidDeactivationDate()
    {
        yield return new object[] { "John", "Doe", "john@email.com", DateTime.Now.AddDays(-1) };
    }

    [Theory]
    [MemberData(nameof(GetUserData_InvalidDeactivationDate))]
    public void WhenCreatingUserWithPastDeactivationDate_ThenThrowsArgumentException(string firstName, string lastName, string email, DateTime? deactivationDate)
    {
        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new User(firstName, lastName, email, deactivationDate));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> GetUserData_InvalidFields()
    {
        yield return new object[] { "John 13", "Doe 13", "john@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "", "Doe", "", DateTime.Now.AddDays(1) };
        yield return new object[] { "John", "", "john@email.com.", DateTime.Now.AddDays(1) };
        yield return new object[] { "", "Doe", "john@email.com", DateTime.Now.AddDays(-1) };
    }

    [Theory]
    [MemberData(nameof(GetUserData_InvalidFields))]
    public void WhenCreatingUserWithInvalidFields_ThenThrowsArgumentException(string firstName, string lastName, string email, DateTime? deactivationDate)
    {
        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new User(firstName, lastName, email, deactivationDate));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> GetDeactivationDate()
    {
        yield return new object[] { DateTime.Now.AddHours(1) };
        yield return new object[] { DateTime.Now.AddYears(1) };
    }

    [Theory]
    [MemberData(nameof(GetDeactivationDate))]
    public void WhenCurrentDateIsBeforeDeactivationDate_ThenReturnFalse(DateTime deactivationDate)
    {
        // Arrange
        User user = new User("John", "Doe", "john@email.com", deactivationDate);

        // Act
        bool result = user.IsDeactivated();

        // Assert
        Assert.False(result);
    }

    public static IEnumerable<object[]> GetDeactivationDateAndCompare()
    {
        yield return new object[] { DateTime.Now.AddHours(1), DateTime.Now.AddHours(2) };
        yield return new object[] { DateTime.Now.AddYears(1), DateTime.Now.AddYears(2) };
    }

    [Theory]
    [MemberData(nameof(GetDeactivationDateAndCompare))]
    public void WhenGivenDateIsAfterDeactivationDate_ThenReturnTrue(DateTime deactivationDate, DateTime dateCompare)
    {
        // Arrange
        User user = new User("John", "Doe", "john@email.com", deactivationDate);

        // Act
        bool result = user.DeactivationDateIsBefore(dateCompare);

        // Assert
        Assert.True(result);
    }


    [Fact]
    public void WhenDeactivatingAnActiveUser_ThenReturnTrue()
    {
        // Arrange 
        User user = new User("John", "Doe", "john.doe@email.com", DateTime.MaxValue);

        // Act
        bool result = user.DeactivateUser();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void WhenDeactivatingAnAlreadyDeactivatedUser_ThenReturnFalse()
    {
        // Arrange

        User user = new User("John", "Doe", "john.doe@email.com", DateTime.MaxValue);
        user.DeactivateUser();

        // Act
        bool result = user.DeactivateUser();

        // Assert
        Assert.False(result);

    }

    public static IEnumerable<object[]> GetDatesEqualAndBeforeDeactivatonDate()
    {
        yield return new object[] { DateTime.MaxValue };
        yield return new object[] { new DateTime(2045, 4, 1, 1, 1, 1) };
    }
    [Theory]
    [MemberData(nameof(GetDatesEqualAndBeforeDeactivatonDate))]
    public void WhenDeactivationDateIsEqualOrAfter_ThenReturnFalse(DateTime date)
    {
        //arrange
        User user = new User("John", "Doe", "john.doe@email.com", DateTime.MaxValue);

        //act
        bool result = user.DeactivationDateIsBefore(date);

        //assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("FirstName SecondName")]
    [InlineData("FirstName")]
    [InlineData("SecondName")]
    [InlineData("First")]
    [InlineData("name")]
    [InlineData("Sec")]
    [InlineData("second")]
    public void WhenHasNamesGetsCorrectName_ReturnTrue(string nameToSearch)
    {
        // Arrange
        string names = "FirstName SecondName";
        string surnames = "Surnames";
        string email = "email@domain.pt";
        DateTime deactivationDate = DateTime.Now.AddYears(1);

        // Create User instance
        User user = new User(names, surnames, email, deactivationDate);

        // Assert
        Assert.True(
            // Act
            user.HasNames(nameToSearch)
        );
    }

    [Theory]
    [InlineData("")]
    [InlineData("FirstNames")]
    [InlineData("FirstName SecondNames")]
    public void WhenHasNamesGetsWrongName_ReturnFalse(string nameToSearch)
    {
        //Arrange
        string names = "FirstName SecondName";
        string surnames = "Surnames";
        string email = "email@domain.pt";
        DateTime deactivationDate = DateTime.Now.AddYears(1);

        //User instance
        User user = new User(names, surnames, email, deactivationDate);

        //Assert
        Assert.False(
            //Act
            user.HasNames(nameToSearch)
        );
    }

    [Theory]
    [InlineData("FirstSurname SecondSurname")]
    [InlineData("FirstSurname")]
    [InlineData("SecondSurname")]
    [InlineData("First")]
    [InlineData("Name")]
    [InlineData("Sec")]
    [InlineData("second")]
    public void WhenHasSurnamesGetsACorrectSurname_ReturnTrue(string surnameToSearch)
    {
        //Arrange
        string names = "FirstName SecondName";
        string surnames = "FirstSurname SecondSurname";
        string email = "email@domain.pt";
        DateTime deactivationDate = DateTime.Now.AddYears(1);

        //User instance
        User user = new User(names, surnames, email, deactivationDate);

        //Assert
        Assert.True(
            //Act
            user.HasSurnames(surnameToSearch)
        );
    }

    [Theory]
    [InlineData("")]
    [InlineData("FirstSurnames")]
    [InlineData("FirstSurname SecondSurnames")]
    public void WhenHasSurnamesGetsWrongName_ReturnFalse(string surnameToSearch)
    {
        //Arrange
        string names = "FirstName SecondName";
        string surnames = "FirstSurname SecondSurname";
        string email = "email@domain.pt";
        DateTime deactivationDate = DateTime.Now.AddYears(1);

        //User instance
        User user = new User(names, surnames, email, deactivationDate);

        //Assert
        Assert.False(
            //Act
            user.HasSurnames(surnameToSearch)
        );
    }
}