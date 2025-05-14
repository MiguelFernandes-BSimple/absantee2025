using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.UserTests;

public class Factory
{

    [Fact]
    public async Task WhenCreatingUser_ThenUserIsCreated()
    {
        //arrange
        string email = "test@email.com";

        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(repo => repo.GetByEmailAsync(email)).ReturnsAsync((IUser?)null);

        var userFactory = new UserFactory(userRepository.Object);

        //act
        var result = await userFactory.Create("John", "Doe", email, DateTime.MaxValue);

        Assert.NotNull(result);

    }

    [Fact]
    public async Task WhenCreatingUserWithAnRepeatedEmail_ThenThrowsArgumentException()
    {
        //arrange
        string email = "test@email.com";
        var existingUser = new Mock<User>();

        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(repo => repo.GetByEmailAsync(email)).ReturnsAsync(existingUser.Object);

        UserFactory userFactory = new UserFactory(userRepository.Object);

        //Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                    // Act
                    userFactory.Create("John", "Doe", email, DateTime.MaxValue));

        Assert.Equal("An user with this email already exists.", exception.Message);
    }

    [Fact]
    public void WhenCreatingUserDomainFromDataModel_ThenUserIsCreated()
    {
        //Arrange
        var userVisitor = new Mock<IUserVisitor>();

        userVisitor.Setup(u => u.Id).Returns(Guid.NewGuid());
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