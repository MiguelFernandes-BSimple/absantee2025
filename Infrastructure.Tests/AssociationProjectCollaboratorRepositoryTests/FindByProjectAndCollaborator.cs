using Domain.Interfaces;
using Infrastructure.Repositories;
using Moq;

namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryTests
{
    public class FindByProjectAndCollaborator
    {
        [Fact]
        public void WhenPassingProjectAndCollaborator_ThenReturnExpectedAssociation()
        {
            //arrange
            Mock<IProject> projectMock = new Mock<IProject>();

            Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
            Mock<IAssociationProjectCollaborator> assocMock2 = new Mock<IAssociationProjectCollaborator>();
            List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
                assocMock1.Object,
                assocMock2.Object
            };

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

            //act
            var result = assoc.FindByProjectAndCollaborator(projectMock.Object, collabMock2.Object);

            //assert
            Assert.Equal(expected, result);
        }


        [Fact]
        public void WhenNoAssociationMatchesProject_ThenReturnsNull()
        {
            // Arrange
            var projectMock = new Mock<IProject>();
            var assocMock = new Mock<IAssociationProjectCollaborator>();
            var collabMock = new Mock<ICollaborator>();

            assocMock.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
            assocMock.Setup(a => a.HasCollaborator(collabMock.Object)).Returns(true);

            var repo = new AssociationProjectCollaboratorRepository(new List<IAssociationProjectCollaborator> { assocMock.Object });

            // Act
            var result = repo.FindByProjectAndCollaborator(projectMock.Object, collabMock.Object);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void WhenNoAssociationMatchesCollaborator_ThenReturnsNull()
        {
            // Arrange
            var projectMock = new Mock<IProject>();
            var assocMock = new Mock<IAssociationProjectCollaborator>();
            var collabMock = new Mock<ICollaborator>();

            assocMock.Setup(a => a.HasProject(projectMock.Object)).Returns(true);
            assocMock.Setup(a => a.HasCollaborator(collabMock.Object)).Returns(false);

            var repo = new AssociationProjectCollaboratorRepository(new List<IAssociationProjectCollaborator> { assocMock.Object });

            // Act
            var result = repo.FindByProjectAndCollaborator(projectMock.Object, collabMock.Object);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void WhenNoAssociationMatchesCollaboratorAndProject_ThenReturnsNull()
        {
            // Arrange
            var projectMock = new Mock<IProject>();
            var assocMock = new Mock<IAssociationProjectCollaborator>();
            var collabMock = new Mock<ICollaborator>();

            assocMock.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
            assocMock.Setup(a => a.HasCollaborator(collabMock.Object)).Returns(false);

            var repo = new AssociationProjectCollaboratorRepository(new List<IAssociationProjectCollaborator> { assocMock.Object });

            // Act
            var result = repo.FindByProjectAndCollaborator(projectMock.Object, collabMock.Object);

            // Assert
            Assert.Null(result);
        }
    }
}
