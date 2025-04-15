using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.UserTests;

public class Factory
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
    public async Task WhenCreatingUserWithValidFields_ThenNoExceptionIsThrown(string firstName, string lastName, string email, DateTime? deactivationDate)
    {
        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(repo => repo.GetByEmailAsync(email)).ReturnsAsync((IUser?)null);

        UserFactory userFactory = new UserFactory(userRepository.Object);

        // Act
        await userFactory.Create(firstName, lastName, email, deactivationDate);

        // Assert - No exception should be thrown
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
    public async Task WhenCreatingUserWithInvalidFirstName_ThenThrowsArgumentException(string firstName, string lastName, string email, DateTime? deactivationDate)
    {
        // Assert
        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(repo => repo.GetByEmailAsync(email)).ReturnsAsync((IUser?)null);

        UserFactory userFactory = new UserFactory(userRepository.Object);

        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            userFactory.Create(firstName, lastName, email, deactivationDate));

        Assert.Equal("Names or surnames are invalid.", exception.Message);
    }

    public static IEnumerable<object[]> GetUserData_InvalidDeactivationDate()
    {
        yield return new object[] { "John", "Doe", "john@email.com", DateTime.Now.AddDays(-1) };
    }

    [Theory]
    [MemberData(nameof(GetUserData_InvalidDeactivationDate))]
    public async Task WhenCreatingUserWithPastDeactivationDate_ThenThrowsArgumentException(string firstName, string lastName, string email, DateTime? deactivationDate)
    {
        // Assert
        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(repo => repo.GetByEmailAsync(email)).ReturnsAsync((IUser?)null);

        UserFactory userFactory = new UserFactory(userRepository.Object);

        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            userFactory.Create(firstName, lastName, email, deactivationDate));

        Assert.Equal("Deactivaton date can't be in the past.", exception.Message);
    }

    [Theory]
    [InlineData("john")]
    [InlineData("@email.com")]
    [InlineData("john@.com")]
    [InlineData("john@email,com")]
    [InlineData("john@doe@email.com")]
    [InlineData("john@.email.com")]
    public async Task WhenCreatingUserWithInvalidEmail_ThenThrowsArgumentException(string email)
    {
        // Assert
        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(repo => repo.GetByEmailAsync(email)).ReturnsAsync((IUser?)null);

        UserFactory userFactory = new UserFactory(userRepository.Object);

        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            userFactory.Create("John", "Doe", email, null));

        Assert.Equal("Email is invalid.", exception.Message);
    }

    [Fact]
    public async Task WhenCreatingUserWithAnRepeatedEmail_ThenThrowsArgumentException()
    {
        //arrange
        string email = "test@email.com";
        var existingUser = new Mock<IUser>();

        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(repo => repo.GetByEmailAsync(email)).ReturnsAsync(existingUser.Object);

        UserFactory userFactory = new UserFactory(userRepository.Object);

        //Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                    // Act
                    userFactory.Create("John", "Doe", email, null));

        Assert.Equal("An user with this email already exists.", exception.Message);
    }

    [Fact]
    public void WhenCreatingUserDomainFromDataModel_ThenUserIsCreated()
    {
        //Arrange
        var userVisitor = new Mock<IUserVisitor>();

        userVisitor.Setup(u => u.Id).Returns(1);
        userVisitor.Setup(u => u.Names).Returns("John");
        userVisitor.Setup(u => u.Surnames).Returns("Doe");
        userVisitor.Setup(u => u.Email).Returns("john.doe@email.com");
        userVisitor.Setup(u => u.PeriodDateTime).Returns(new PeriodDateTime(DateTime.Now.AddDays(-10), DateTime.Now.AddYears(1)));

        var userRepository = new Mock<IUserRepository>();
        var userFactory = new UserFactory(userRepository.Object);

        //Act
        var result = userFactory.Create(userVisitor.Object);

        //Assert
        Assert.NotNull(result);
    }

}