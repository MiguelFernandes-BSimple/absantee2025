namespace Domain.Tests.CollaboratorRepositoryTests;

using Moq;

public class Constructor {
    [Fact]
    public void WhenPassingCorrectValues_ThenObjectIsCreated()
    {
        //Arrange
        Mock<ICollaborator> doubleCollab = new Mock<ICollaborator>();

        List<ICollaborator> listCollab = new List<ICollaborator> { doubleCollab.Object };

        //Act
        new CollaboratorRepository(listCollab);

        //Assert
    }
}
