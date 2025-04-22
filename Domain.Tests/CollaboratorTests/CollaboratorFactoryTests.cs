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
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        collabRepo.Setup(c => c.IsRepeated(It.IsAny<ICollaborator>())).ReturnsAsync(false);

        Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
        userRepo.Setup(u => u.GetById(It.IsAny<int>())).Returns(user.Object);

        var collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);

        // Act
        var result = collabFactory.Create(It.IsAny<long>(), It.IsAny<PeriodDateTime>());

        // Assert
    }

    [Fact]
    public async Task WhenCreatingCollaboratorWhereFinalDateIsAfterDeactivationDate_ThenShouldThrowException()
    {
        // Arrange
        long userId = 1;

        PeriodDateTime period = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(period.GetFinalDate())).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(false);
        user.Setup(u => u.GetId()).Returns(userId);

        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        collabRepo.Setup(c => c.IsRepeated(It.IsAny<ICollaborator>())).ReturnsAsync(false);

        Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
        userRepo.Setup(u => u.GetById(userId)).Returns(user.Object);

        CollaboratorFactory collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
            () =>
                // Act
                collabFactory.Create(userId, period)
        );
        Assert.Equal("User deactivation date is before collaborator contract end date.", exception.Message);
    }

    [Fact]
    public async Task WhenCreatingCollaboratorWhereUserIsDeactivated_ThenShowThrowException()
    {
        // Arrange
        long userId = 1;

        PeriodDateTime period = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(period.GetFinalDate())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(true);
        user.Setup(u => u.GetId()).Returns(userId);

        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        collabRepo.Setup(c => c.IsRepeated(It.IsAny<ICollaborator>())).ReturnsAsync(false);

        Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
        userRepo.Setup(u => u.GetById(userId)).Returns(user.Object);

        CollaboratorFactory collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);
        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
            () =>
                // Act
                collabFactory.Create(userId, period)
        );
        Assert.Equal("User is deactivated.", exception.Message);
    }

    [Fact]
    public async Task WhenCreatingCollaboratorWhereUserDontExists_ThenShowThrowException()
    {
        // Arrange
        PeriodDateTime period = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(period.GetFinalDate())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        collabRepo.Setup(c => c.IsRepeated(It.IsAny<ICollaborator>())).ReturnsAsync(false);

        Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
        userRepo.Setup(u => u.GetById(It.IsAny<int>())).Returns((IUser?)null);

        CollaboratorFactory collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);

        //Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
            () =>
                // Act
                collabFactory.Create(It.IsAny<long>(), period)
        );
        Assert.Equal("User dont exists", exception.Message);
    }

    [Fact]
    public async Task WhenCreatingCollaboratorWhereCollaboratorAlreadyExists_ThenShowThrowException()
    {
        // Arrange
        long userId = 1;

        PeriodDateTime period = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(period.GetFinalDate())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);
        user.Setup(u => u.GetId()).Returns(userId);

        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        collabRepo.Setup(c => c.IsRepeated(It.IsAny<ICollaborator>())).ReturnsAsync(true);

        Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
        userRepo.Setup(u => u.GetById(userId)).Returns(user.Object);

        CollaboratorFactory collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
            () =>
                // Act
                collabFactory.Create(userId, period)
        );
        Assert.Equal("Collaborator already exists", exception.Message);
    }

    [Fact]
    public void WhenCreatingCollaboratorWithICollaboratorVisitor_ThenCollaboratorIsCreatedCorrectly()
    {
        // Arrange
        var visitor = new Mock<ICollaboratorVisitor>();
        visitor.Setup(v => v.Id).Returns(It.IsAny<int>());
        visitor.Setup(v => v.UserId).Returns(It.IsAny<int>());
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

