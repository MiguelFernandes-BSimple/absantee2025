namespace Domain.Tests.AssociationProjectCollaboratorRepositoryTests;

using Moq;

public class Constructor {
    [Fact]
    public void WhenPassingCorrectValues_ThenObjectIsCreated()
    {
        //arrange
        Mock<IAssociationProjectCollaborator> assocMock = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> { assocMock.Object };

        //act
        new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //assert
    }
}
