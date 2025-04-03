namespace Infrastructure.Tests.CollaboratorRepositoryTests;

using Moq;
using Domain.Interfaces;
using Infrastructure.Repositories;

public class Constructor
{
    /**
    * Test for empty constructor
    */
    [Fact]
    public void WhenNotPassingAnyArguments_ThenObjectIsCreated()
    {
        //Arrange

        //Act
        new CollaboratorRepository();

        //Assert
    }

    /**
    * Test for constructor that receives a list of ICollaborator
    */
    [Fact]
    public void WhenPassingCorrectCollaboratorList_ThenObjectIsCreated()
    {
        // Arrange
        // Collaborator doubles - stubs
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator3 = new Mock<ICollaborator>();

        // To be correct, all collaborators must be distinct - Distinct
        // Email is different from the others
        doubleCollaborator1.Setup(c1 => c1.Equals(doubleCollaborator2.Object)).Returns(false);
        doubleCollaborator2.Setup(c2 => c2.Equals(doubleCollaborator1.Object)).Returns(false);

        // Create Collaborator List
        List<ICollaborator> collabList =
            new List<ICollaborator> { doubleCollaborator1.Object, doubleCollaborator2.Object };

        // Act
        new CollaboratorRepository(collabList);

        // Assert
    }

    /**
    * Test for constructor that receives a list of ICollaborator that is not valid
    * -> Should throw an exception 
    *
    * One element being invalid is enough for the whole list to be invalid too
    */
    [Fact]
    public void WhenPassingIncorrectCollaboratorList_ThenThrowException()
    {
        // Arrange
        // Collaborator doubles - stubs
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();

        // Set up two collaborators with the same email
        doubleCollaborator1.Setup(c1 => c1.Equals(doubleCollaborator2.Object)).Returns(true);
        doubleCollaborator2.Setup(c2 => c2.Equals(doubleCollaborator1.Object)).Returns(true);

        // Create Collaborator List
        List<ICollaborator> collabList =
            new List<ICollaborator> { doubleCollaborator1.Object, doubleCollaborator2.Object };

        // Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new CollaboratorRepository(collabList));

        Assert.Equal("Arguments are not valid!", exception.Message);
    }
}
