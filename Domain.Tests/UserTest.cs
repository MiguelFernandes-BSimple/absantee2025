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

    public static IEnumerable<object[]> GetCorrectSearchNames()
    {
        yield return new object[] {
            "FirstName SecondName",
            "Surnames",
            "email@domain.pt",
            DateTime.Now.AddYears(1),
            "FirstName SecondName" };

        yield return new object[] {
            "FirstName SecondName",
            "Surnames",
            "email@domain.pt",
            DateTime.Now.AddYears(1),
            "FirstName" };

        yield return new object[] {
            "FirstName SecondName",
            "Surnames",
            "email@domain.pt",
            DateTime.Now.AddYears(1),
            "SecondName" };
    }

    [Theory]
    [MemberData(nameof(GetCorrectSearchNames))]
    public void WhenHasNamesGetsCorrectName_ReturnTrue(
        string names,
        string surnames,
        string email,
        DateTime deactivationDate,
        string nameToSearch)
    {
        // Arrange
        // Create User instance
        User user = new User(names, surnames, email, deactivationDate);

        // Assert
        Assert.True(
            // Act
            user.HasNames(nameToSearch)
        );
    }

    public static IEnumerable<object[]> GetWrongSearchNames()
    {
        yield return new object[] {
            "FirstName SecondName",
            "Surnames",
            "email@domain.pt",
            DateTime.Now.AddYears(1),
            "" };

        yield return new object[] {
            "FirstName SecondName",
            "Surnames",
            "email@domain.pt",
            DateTime.Now.AddYears(1),
            "FirstNames" };

        yield return new object[] {
            "FirstName SecondName",
            "Surnames",
            "email@domain.pt",
            DateTime.Now.AddYears(1),
            "FirstName SecondNames" };
    }

    [Theory]
    [MemberData(nameof(GetWrongSearchNames))]
    public void WhenHasNamesGetsWrongName_ReturnFalse(
        string names,
        string surnames,
        string email,
        DateTime deactivationDate,
        string nameToSearch)
    {
        //Arrange
        //User instance
        User user = new User(names, surnames, email, deactivationDate);

        //Assert
        Assert.False(
            //Act
            user.HasNames(nameToSearch)
        );
    }

    public static IEnumerable<object[]> GetCorrectSearchSurnames()
    {
        yield return new object[] {
            "FirstName SecondName",
            "FirstSurname SecondSurname",
            "email@domain.pt",
            DateTime.Now.AddYears(1),
            "FirstSurname SecondSurname" };

        yield return new object[] {
            "FirstName SecondName",
            "FirstSurname SecondSurname",
            "email@domain.pt",
            DateTime.Now.AddYears(1),
            "FirstSurname" };

        yield return new object[] {
            "FirstName SecondName",
            "FirstSurname SecondSurname",
            "email@domain.pt",
            DateTime.Now.AddYears(1),
            "SecondSurname" };
    }

    [Theory]
    [MemberData(nameof(GetCorrectSearchSurnames))]
    public void WhenHasSurnamesGetsACorrectSurname_ReturnTrue(
        string names,
        string surnames,
        string email,
        DateTime deactivationDate,
        string surnameToSearch)
    {
        //Arrange
        //User instance
        User user = new User(names, surnames, email, deactivationDate);

        //Assert
        Assert.True(
            //Act
            user.HasSurnames(surnameToSearch)
        );
    }

    public static IEnumerable<object[]> GetWrongSearchSurnames()
    {
        yield return new object[] {
            "FirstName SecondName",
            "Surnames",
            "email@domain.pt",
            DateTime.Now.AddYears(1),
            "" };

        yield return new object[] {
            "FirstName SecondName",
            "Surnames",
            "email@domain.pt",
            DateTime.Now.AddYears(1),
            "Surname" };

        yield return new object[] {
            "FirstName SecondName",
            "FirstSurname SecondSurname",
            "email@domain.pt",
            DateTime.Now.AddYears(1),
            "FirstSurname SecondSurnames" };
    }

    [Theory]
    [MemberData(nameof(GetWrongSearchSurnames))]
    public void WhenHasSurnamesGetsWrongName_ReturnFalse(
        string names,
        string surnames,
        string email,
        DateTime deactivationDate,
        string surnameToSearch)
    {
        //Arrange
        //User instance
        User user = new User(names, surnames, email, deactivationDate);

        //Assert
        Assert.False(
            //Act
            user.HasSurnames(surnameToSearch)
        );
    }
}