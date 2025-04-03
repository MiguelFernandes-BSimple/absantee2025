using Domain.Models;

namespace Domain.Tests.UserTests;

public class Equals
{
    public static IEnumerable<object[]> GetUserSameEmail_ValidFields()
    {
        yield return new object[] { "Jane", "Doe", "john@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "John Peter", "Doe", "john@email.com", DateTime.Now.AddYears(1) };
        yield return new object[] { "John", "Wallace Doe", "john@email.com", DateTime.Now.AddYears(2) };
    }

    [Theory]
    [MemberData(nameof(GetUserSameEmail_ValidFields))]
    public void WhenPassingValidObjectWithSameEmail_ReturnTrue(string names, string surnames, string email, DateTime deactivationDate)
    {
        // Arrange
        // Names/surnames/deactivationDate don't matter for the context of this test
        string referenceNames = "example names";
        string referenceSurnames = "example surnames";
        DateTime referenceDeactivationDate = DateTime.Now.AddYears(2);

        // Email is the property that defines if two users are equal
        string referenceEmail = "john@email.com";

        // Reference user
        User referenceUser = new User(referenceNames, referenceSurnames, referenceEmail, referenceDeactivationDate);

        // User to Test
        User testUser = new User(names, surnames, email, deactivationDate);

        // Act
        bool result = referenceUser.Equals(testUser);

        // Assert
        Assert.True(result);
    }

    public static IEnumerable<object[]> GetUserDifferentEmail_ValidFields()
    {
        yield return new object[] { "John", "Doe", "john@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "John Peter", "Doe", "john.doe.13@email.com", DateTime.Now.AddYears(1) };
        yield return new object[] { "John", "Wallace Doe", "john.doe@company.com.pt", DateTime.Now.AddYears(2) };
    }

    [Theory]
    [MemberData(nameof(GetUserDifferentEmail_ValidFields))]
    public void WhenPassingValidObjectWithDifferentEmail_ReturnFalse(string names, string surnames, string email, DateTime deactivationDate)
    {
        // Arrange
        // Names/surnames/deactivationDate don't matter for the context of this test
        string referenceNames = "example names";
        string referenceSurnames = "example surnames";
        DateTime referenceDeactivationDate = DateTime.Now.AddYears(2);

        // Email is the property that defines if two users are equal
        string referenceEmail = "example@email.com";

        // Reference user
        User referenceUser = new User(referenceNames, referenceSurnames, referenceEmail, referenceDeactivationDate);

        // User to Test
        User testUser = new User(names, surnames, email, deactivationDate);

        // Act
        bool result = referenceUser.Equals(testUser);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void WhenPassingNullObject_ReturnFalse()
    {
        // Arrange 
        // Names/surnames/deactivationDate/email don't matter for the context of this test
        string referenceNames = "example names";
        string referenceSurnames = "example surnames";
        string referenceEmail = "john@email.com";
        DateTime referenceDeactivationDate = DateTime.Now.AddYears(2);

        // Reference user
        User referenceUser = new User(referenceNames, referenceSurnames, referenceEmail, referenceDeactivationDate);

        // Act
        // The object passed is NULL
        bool result = referenceUser.Equals(null);

        // Assert
        Assert.False(result);
    }

    public static IEnumerable<object[]> GetObjects_DifferentFromUser()
    {
        yield return new object[] { "test" };
        yield return new object[] { DateTime.Now.AddYears(1) };
    }

    [Theory]
    [MemberData(nameof(GetObjects_DifferentFromUser))]
    public void WhenPassingObjectOfDifferentType_ThenReturnFalse(Object obj)
    {
        // Arrange 
        // Names/surnames/deactivationDate/email don't matter for the context of this test
        string referenceNames = "example names";
        string referenceSurnames = "example surnames";
        string referenceEmail = "john@email.com";
        DateTime referenceDeactivationDate = DateTime.Now.AddYears(2);

        // Reference user
        User referenceUser = new User(referenceNames, referenceSurnames, referenceEmail, referenceDeactivationDate);

        // Act
        // The object passed is NULL
        bool result = referenceUser.Equals(obj);

        // Assert
        Assert.False(result);
    }
}