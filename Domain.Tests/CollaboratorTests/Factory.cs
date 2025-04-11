using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.CollaboratorTests;
public class Factory
{
    [Fact]
    public void WhenCreatingCollaboratorWithValidPeriod_ThenCollaboratorIsCreatedCorrectly()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        var collabRepo = new Mock<ICollaboratorRepository>();
        collabRepo.Setup(c => c.IsRepeated(It.IsAny<ICollaborator>())).Returns(false);
        var userRepo = new Mock<IUserRepository>();
        userRepo.Setup(u => u.GetById(It.IsAny<int>())).Returns(user.Object);

        var collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);
        
        //act
        var result = collabFactory.Create(It.IsAny<long>(), periodDateTime.Object);

        //assert
    }

    [Fact]
    public void WhenCreatingCollaboratorWhereFinalDateIsAfterDeactivationDate_ThenShouldThrowException()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(true);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        var collabRepo = new Mock<ICollaboratorRepository>();
        collabRepo.Setup(c => c.IsRepeated(It.IsAny<ICollaborator>())).Returns(false);
        var userRepo = new Mock<IUserRepository>();
        userRepo.Setup(u => u.GetById(It.IsAny<int>())).Returns(user.Object);

        var collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);
        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                collabFactory.Create(It.IsAny<long>(), periodDateTime.Object)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenCreatingCollaboratorWhereUserIsDeactivated_ThenShowThrowException()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(true);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        var collabRepo = new Mock<ICollaboratorRepository>();
        collabRepo.Setup(c => c.IsRepeated(It.IsAny<ICollaborator>())).Returns(false);
        var userRepo = new Mock<IUserRepository>();
        userRepo.Setup(u => u.GetById(It.IsAny<int>())).Returns(user.Object);

        var collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);
        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                collabFactory.Create(It.IsAny<long>(), periodDateTime.Object)
        );
        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenCreatingCollaboratorWhereUserDontExists_ThenShowThrowException()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        var collabRepo = new Mock<ICollaboratorRepository>();
        collabRepo.Setup(c => c.IsRepeated(It.IsAny<ICollaborator>())).Returns(false);
        var userRepo = new Mock<IUserRepository>();
        userRepo.Setup(u => u.GetById(It.IsAny<int>())).Returns((IUser?)null);

        var collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);
        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                collabFactory.Create(It.IsAny<long>(), periodDateTime.Object)
        );
        Assert.Equal("User dont exists", exception.Message);
    }

    [Fact]
    public void WhenCreatingCollaboratorWhereCollaboratorAlreadyExists_ThenShowThrowException()
    {
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        var collabRepo = new Mock<ICollaboratorRepository>();
        collabRepo.Setup(c => c.IsRepeated(It.IsAny<ICollaborator>())).Returns(true);
        var userRepo = new Mock<IUserRepository>();
        userRepo.Setup(u => u.GetById(It.IsAny<int>())).Returns(user.Object);

        var collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);
        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                collabFactory.Create(It.IsAny<long>(), periodDateTime.Object)
        );
        Assert.Equal("Collaborator already exists", exception.Message);
    }

    [Fact]
    public void WhenCreatingCollaboratorWithICollaboratorVisitor_ThenCollaboratorIsCreatedCorrectly()
    {
        //arrange
        var visitor = new Mock<ICollaboratorVisitor>();
        visitor.Setup(v => v.Id).Returns(It.IsAny<int>() );
        visitor.Setup(v => v.UserID).Returns(It.IsAny<int>() );
        visitor.Setup(v => v.PeriodDateTime).Returns(It.IsAny<PeriodDateTime>() );

        var collabRepo = new Mock<ICollaboratorRepository>();
        var userRepo = new Mock<IUserRepository>();
        var collabFactory = new CollaboratorFactory(collabRepo.Object, userRepo.Object);

        //act
        var result = collabFactory.Create(visitor.Object);

        //assert
    }
}

