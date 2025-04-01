namespace Domain.Tests.CollaboratorRepositoryTests;

using Moq;

public class AddCollaborator {
    /**
    * Method to add a collaborator to repo
    * Happy Path
    */
    [Fact]
    public void WhenAddingCorrectCollaboratorToRepository_ThenReturnTrue()
    {
        // Arrange 
        // Double for holidayPlan
        Mock<ICollaborator> doubleCollab = new Mock<ICollaborator>();

        // Instatiate repository
        CollaboratorRepository repo = new CollaboratorRepository();

        // Act
        // add holiday plan to repository
        bool result = repo.AddCollaborator(doubleCollab.Object);

        // Assert
        Assert.True(result);
    }

    /**
    * Method to add a collaborator to repo
    * There is an collaborator with the same email 
    * -> can't add it
    */
    [Fact]
    public void WhenAddingCollaboratorWithRepeatedEmailToRepository_ThenReturnFalse()
    {
        // Arrange 
        // Double for Colllaborators
        Mock<ICollaborator> doubleCollab = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollabToAdd = new Mock<ICollaborator>();

        string email = "sameEmail@email.com";

        // Collabs have the same email
        doubleCollab.Setup(c1 => c1.GetEmail()).Returns(email);
        doubleCollabToAdd.Setup(c2 => c2.GetEmail()).Returns(email);

        // Instatiate repository
        CollaboratorRepository repo = new CollaboratorRepository(new List<ICollaborator> { doubleCollab.Object });

        // Act
        // add holiday plan to repository
        bool result = repo.AddCollaborator(doubleCollabToAdd.Object);

        // Assert
        Assert.False(result);
    }
}
