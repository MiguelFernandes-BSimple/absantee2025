using Moq;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Tests.CollaboratorTests;

public class HasNames
{
    [Fact]
    public void WhenHasNamesGetsCorrectName_ReturnTrue()
    {
        //Arrange
        // User double 
        Mock<IUser> doubleUser = new Mock<IUser>();

        // Name of the user
        string names = "First Name";
        // The user has the name defined 
        doubleUser.Setup(u => u.HasNames(names)).Returns(true);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();

        Collaborator collaborator = new Collaborator(doubleUser.Object, periodDateTime.Object);

        // Assert 
        Assert.True(
            //Act 
            collaborator.HasNames(names)
        );
    }

    [Fact]
    public void WhenHasNamesGetsWrongName_ReturnFalse()
    {
        //Arrange
        // User double 
        Mock<IUser> doubleUser = new Mock<IUser>();

        // Name of the user
        string names = "First Name";
        // The user has the name defined 
        doubleUser.Setup(u => u.HasNames(names)).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();

        Collaborator collaborator = new Collaborator(doubleUser.Object, periodDateTime.Object);

        // Assert 
        Assert.False(
            //Act 
            collaborator.HasNames(names)
        );
    }
}
