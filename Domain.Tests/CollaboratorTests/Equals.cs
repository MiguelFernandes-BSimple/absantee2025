using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.CollaboratorTests;

public class Equals
{
    [Fact]
    public void WhenPassingValidObjectWithSameEmailAndIntersectingPeriod_ReturnTrue()
    {
        //Arrange
        Mock<IUser> doubleUser = new Mock<IUser>();
        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();

        Collaborator collaborator = new Collaborator(doubleUser.Object, periodDateTime.Object);

        doubleUser.Setup(u => u.Equals(doubleUser.Object)).Returns(true);
        periodDateTime.Setup(u => u.Intersects(periodDateTime.Object)).Returns(true);

        //Act
        bool result = collaborator.Equals(collaborator);

        // Assert
        Assert.True(result);
    }


    [Fact]
    public void WhenPassingValidObjectWithDifferentEmail_ReturnFalse()
    {
        //Arrange
        Mock<IUser> doubleUser1 = new Mock<IUser>();
        Mock<IUser> doubleUser2 = new Mock<IUser>();
        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();

        Collaborator collaborator1 = new Collaborator(doubleUser1.Object, periodDateTime.Object);
        Collaborator collaborator2 = new Collaborator(doubleUser2.Object, periodDateTime.Object);

        doubleUser1.Setup(u => u.Equals(doubleUser2.Object)).Returns(false);
        periodDateTime.Setup(u => u.Intersects(periodDateTime.Object)).Returns(true);

        //Act
        bool result = collaborator1.Equals(collaborator2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void WhenPassingValidObjectWithNotIntersectedPeriods_ReturnFalse()
    {
        //Arrange
        Mock<IUser> doubleUser1 = new Mock<IUser>();
        Mock<IUser> doubleUser2 = new Mock<IUser>();
        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();

        Collaborator collaborator1 = new Collaborator(doubleUser1.Object, periodDateTime.Object);
        Collaborator collaborator2 = new Collaborator(doubleUser2.Object, periodDateTime.Object);

        doubleUser1.Setup(u => u.Equals(doubleUser2.Object)).Returns(true);
        periodDateTime.Setup(u => u.Intersects(periodDateTime.Object)).Returns(false);

        //Act
        bool result = collaborator1.Equals(collaborator2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void WhenPassingValidObjectWithDifferentUserAndPeriod_ReturnFalse()
    {
        //Arrange
        Mock<IUser> doubleUser1 = new Mock<IUser>();
        Mock<IUser> doubleUser2 = new Mock<IUser>();
        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();

        Collaborator collaborator1 = new Collaborator(doubleUser1.Object, periodDateTime.Object);
        Collaborator collaborator2 = new Collaborator(doubleUser2.Object, periodDateTime.Object);

        doubleUser1.Setup(u => u.Equals(doubleUser2.Object)).Returns(false);
        periodDateTime.Setup(u => u.Intersects(periodDateTime.Object)).Returns(false);

        //Act
        bool result = collaborator1.Equals(collaborator2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void WhenPassingInvalidObject_ReturnFalse()
    {
        //Arrange
        Mock<IUser> doubleUser = new Mock<IUser>();
        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();

        Collaborator collaborator = new Collaborator(doubleUser.Object, periodDateTime.Object);

        doubleUser.Setup(u => u.Equals("")).Returns(false);

        //Act
        bool result = collaborator.Equals("");

        // Assert
        Assert.False(result);
    }
}