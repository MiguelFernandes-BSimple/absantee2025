using Domain.Models;

namespace Domain.Tests.UserTests;

public class UserConstructorTests
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
        // Act && Assert
        new User(firstName, lastName, email, deactivationDate);
    }

    public static IEnumerable<object[]> GetUserData_InvalidFirstNamesAndLastNames()
    {
        yield return new object[] { new string('a', 51), "Doe", "john@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "", "Doe", "john@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "John 13", "Doe", "john@email.com", null! };
        yield return new object[] { "John", new string('a', 51), "john@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "John", "Doe 13", "john@email.com", null! };
    }

    [Theory]
    [MemberData(nameof(GetUserData_InvalidFirstNamesAndLastNames))]
    public void WhenCreatingUserWithInvalidFirstName_ThenThrowsArgumentException(string firstName, string lastName, string email, DateTime? deactivationDate)
    {
        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new User(firstName, lastName, email, deactivationDate));

        Assert.Equal("Names or surnames are invalid.", exception.Message);
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

        Assert.Equal("Deactivaton date can't be in the past.", exception.Message);
    }

    [Theory]
    [InlineData("john")]
    [InlineData("@email.com")]
    [InlineData("john@.com")]
    [InlineData("john@email,com")]
    [InlineData("john@doe@email.com")]
    [InlineData("john@.email.com")]
    public void WhenCreatingUserWithInvalidEmail_ThenThrowsArgumentException(string email)
    {
        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new User("John", "Doe", email, null));

        Assert.Equal("Email is invalid.", exception.Message);
    }
}