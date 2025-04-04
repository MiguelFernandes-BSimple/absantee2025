namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryTests;

using Moq;
using Domain.Interfaces;
using Infrastructure.Repositories;

public class FindAllByProject
{
    [Fact]
    public void WhenPassingProject_ThenReturnAllAssociationProjectCollaborator()
    {
        //arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        Mock<IAssociationProjectCollaborator> assocMock2 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
            assocMock2.Object
        };

        assocMock2.Setup(a => a.Equals(assocMock1.Object)).Returns(false);

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false);

        assocMock2.Setup(a => a.HasProject(projectMock.Object)).Returns(true);

        List<IAssociationProjectCollaborator> expected = new List<IAssociationProjectCollaborator> { assocMock2.Object };

        var assocRepo = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //act
        var result = assocRepo.FindAllByProject(projectMock.Object);

        //assert
        Assert.True(expected.SequenceEqual(result));
    }
}
