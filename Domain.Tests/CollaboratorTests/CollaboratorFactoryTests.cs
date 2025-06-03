using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.CollaboratorTests;
public class CollaboratorFactoryTests
{
    [Fact]
    public void WhenCreatingCollaboratorWithValidPeriod_ThenCollaboratorIsCreatedCorrectly()
    {
        // Arrange
        var user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        collabRepo.Setup(c => c.IsRepeated(It.IsAny<Collaborator>())).ReturnsAsync(false);

        Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
        userRepo.Setup(u => u.GetById(It.IsAny<Guid>())).Returns(user.Object);

        var collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);

        // Act
        var result = collabFactory.Create(It.IsAny<Guid>(), It.IsAny<PeriodDateTime>());

        // Assert
    }

    [Fact]
    public async Task WhenCreatingCollaboratorWhereFinalDateIsAfterDeactivationDate_ThenShouldThrowException()
    {
        // Arrange
        Guid userId = new Guid();

        PeriodDateTime period = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

        var user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(period._finalDate)).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(false);
        user.Setup(u => u.Id).Returns(userId);

        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        collabRepo.Setup(c => c.IsRepeated(It.IsAny<Collaborator>())).ReturnsAsync(false);

        Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
        userRepo.Setup(u => u.GetById(userId)).Returns(user.Object);

        CollaboratorFactory collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
            () =>
                // Act
                collabFactory.Create(user.Object, period)
        );
        Assert.Equal("User deactivation date is before collaborator contract end date.", exception.Message);
    }

    [Fact]
    public async Task WhenCreatingCollaboratorWhereUserIsDeactivated_ThenShowThrowException()
    {
        // Arrange
        Guid userId = new Guid();

        PeriodDateTime period = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());
    
        var user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(period._finalDate)).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(true);
        user.Setup(u => u.Id).Returns(userId);

        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        collabRepo.Setup(c => c.IsRepeated(It.IsAny<Collaborator>())).ReturnsAsync(false);

        Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
        userRepo.Setup(u => u.GetById(userId)).Returns(user.Object);

        CollaboratorFactory collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);
        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
            () =>
                // Act
                collabFactory.Create(user.Object, period)
        );
        Assert.Equal("User is deactivated.", exception.Message);
    }

    [Fact]
    public async Task WhenCreatingCollaboratorWhereUserDoesNotExist_ThenShowThrowException()
    {
        // Arrange
        PeriodDateTime period = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

        var user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(period._finalDate)).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        collabRepo.Setup(c => c.IsRepeated(It.IsAny<Collaborator>())).ReturnsAsync(false);

        Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
        userRepo.Setup(u => u.GetById(It.IsAny<Guid>())).Returns((User?)null);

        CollaboratorFactory collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);

        //Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
            () =>
                // Act
                collabFactory.Create(It.IsAny<Guid>(), period)
        );
        Assert.Equal("User does not exist", exception.Message);
    }

    [Fact]
    public async Task WhenCreatingCollaboratorWhereCollaboratorAlreadyExists_ThenShowThrowException()
    {
        // Arrange
        Guid userId = new Guid();

        PeriodDateTime period = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

        var user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(period._finalDate)).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);
        user.Setup(u => u.Id).Returns(userId);

        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        collabRepo.Setup(c => c.IsRepeated(It.IsAny<Collaborator>())).ReturnsAsync(true);

        Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
        userRepo.Setup(u => u.GetById(userId)).Returns(user.Object);

        CollaboratorFactory collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
            () =>
                // Act
                collabFactory.Create(user.Object, period)
        );
        Assert.Equal("Collaborator already exists", exception.Message);
    }

    [Fact]
    public void WhenCreatingCollaboratorWithICollaboratorVisitor_ThenCollaboratorIsCreatedCorrectly()
    {
        // Arrange
        var visitor = new Mock<ICollaboratorVisitor>();
        visitor.Setup(v => v.Id).Returns(It.IsAny<Guid>());
        visitor.Setup(v => v.UserId).Returns(It.IsAny<Guid>());
        visitor.Setup(v => v.PeriodDateTime).Returns(It.IsAny<PeriodDateTime>());

        var collabRepo = new Mock<ICollaboratorRepository>();
        var userRepo = new Mock<IUserRepository>();
        var collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);

        // Act
        var result = collabFactory.Create(visitor.Object);

        // Assert
        Assert.NotNull(result);
    }
}

