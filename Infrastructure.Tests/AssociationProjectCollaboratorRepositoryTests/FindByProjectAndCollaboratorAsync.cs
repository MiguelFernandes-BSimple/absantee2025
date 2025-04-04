using Domain.Interfaces;
using Infrastructure.Repositories;
using Moq;

namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryTests
{
    public class FindByProjectAndCollaboratorAsync
    {
        [Fact]
        public async Task WhenPassingProjectAndCollaborator_ThenReturnExpectedAssociationAsync()
        {
            // arrange
            Mock<IProject> projectMock = new Mock<IProject>();

            Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
            Mock<IAssociationProjectCollaborator> assocMock2 = new Mock<IAssociationProjectCollaborator>();
            List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
                assocMock1.Object,
                assocMock2.Object
            };

            assocMock2.Setup(a => a.Equals(assocMock1.Object)).Returns(false);

            assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(true);
            assocMock2.Setup(a => a.HasProject(projectMock.Object)).Returns(true);

            Mock<ICollaborator> collabMock1 = new Mock<ICollaborator>();
            Mock<ICollaborator> collabMock2 = new Mock<ICollaborator>();
            List<ICollaborator> collabs = new List<ICollaborator> {
                collabMock1.Object,
                collabMock2.Object
            };

            assocMock1.Setup(a => a.HasCollaborator(collabMock1.Object)).Returns(false);
            assocMock2.Setup(a => a.HasCollaborator(collabMock2.Object)).Returns(true);

            IAssociationProjectCollaborator expected = assocMock2.Object;

            var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

            // act
            var result = await assoc.FindByProjectAndCollaboratorAsync(projectMock.Object, collabMock2.Object);

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task WhenNoAssociationMatchesProject_ThenReturnsNullAsync()
        {
            // arrange
            Mock<IProject> projectMock = new Mock<IProject>();
            Mock<IAssociationProjectCollaborator> assocMock = new Mock<IAssociationProjectCollaborator>();
            Mock<ICollaborator> collabMock = new Mock<ICollaborator>();

            assocMock.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
            assocMock.Setup(a => a.HasCollaborator(collabMock.Object)).Returns(true);

            var repo = new AssociationProjectCollaboratorRepository(new List<IAssociationProjectCollaborator> { assocMock.Object });

            // act
            var result = await repo.FindByProjectAndCollaboratorAsync(projectMock.Object, collabMock.Object);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public async Task WhenNoAssociationMatchesCollaborator_ThenReturnsNullAsync()
        {
            // arrange
            Mock<IProject> projectMock = new Mock<IProject>();
            Mock<IAssociationProjectCollaborator> assocMock = new Mock<IAssociationProjectCollaborator>();
            Mock<ICollaborator> collabMock = new Mock<ICollaborator>();

            assocMock.Setup(a => a.HasProject(projectMock.Object)).Returns(true);
            assocMock.Setup(a => a.HasCollaborator(collabMock.Object)).Returns(false);

            var repo = new AssociationProjectCollaboratorRepository(new List<IAssociationProjectCollaborator> { assocMock.Object });

            // act
            var result = await repo.FindByProjectAndCollaboratorAsync(projectMock.Object, collabMock.Object);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public async Task WhenNoAssociationMatchesCollaboratorAndProject_ThenReturnsNullAsync()
        {
            // arrange
            Mock<IProject> projectMock = new Mock<IProject>();
            Mock<IAssociationProjectCollaborator> assocMock = new Mock<IAssociationProjectCollaborator>();
            Mock<ICollaborator> collabMock = new Mock<ICollaborator>();

            assocMock.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
            assocMock.Setup(a => a.HasCollaborator(collabMock.Object)).Returns(false);

            var repo = new AssociationProjectCollaboratorRepository(new List<IAssociationProjectCollaborator> { assocMock.Object });

            // act
            var result = await repo.FindByProjectAndCollaboratorAsync(projectMock.Object, collabMock.Object);

            // assert
            Assert.Null(result);
        }
    }
}
