using Moq;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryTests;

public class FindAllByProjectAsync
{
    [Fact]
    public async Task WhenPassingProject_ThenReturnAllAssociationProjectCollaboratorAsync()
    {
        // arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        Mock<IAssociationProjectCollaborator> assocMock2 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator>
        {
            assocMock1.Object,
            assocMock2.Object
        };

        assocMock2.Setup(a => a.Equals(assocMock1.Object)).Returns(false);
        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
        assocMock2.Setup(a => a.HasProject(projectMock.Object)).Returns(true);

        List<IAssociationProjectCollaborator> expected = new List<IAssociationProjectCollaborator> { assocMock2.Object };

        AssociationProjectCollaboratorRepository assocRepo = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        // act
        IEnumerable<IAssociationProjectCollaborator> result = await assocRepo.FindAllByProjectAsync(projectMock.Object);

        // assert
        Assert.True(expected.SequenceEqual(result));
    }
}
