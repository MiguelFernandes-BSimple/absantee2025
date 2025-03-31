using Moq;

namespace Domain.Tests
{
    public class CollaboratorServiceTest
    {
        [Fact]
        public void WhenFindingCollaboratorsByProject_ThenReturnAllAssociatedCollaborators()
        {
            //arrange
            Mock<IProject> projectMock = new Mock<IProject>();

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();

            Mock<IAssociationProjectCollaborator> assoc1 = new Mock<IAssociationProjectCollaborator>();
            Mock<IAssociationProjectCollaborator> assoc2 = new Mock<IAssociationProjectCollaborator>();

            List<IAssociationProjectCollaborator> associations = new List<IAssociationProjectCollaborator>()
            {
                assoc1.Object,
                assoc2.Object,
            };

            Mock<ICollaborator> collab1 = new Mock<ICollaborator>();
            Mock<ICollaborator> collab2 = new Mock<ICollaborator>();

            assoc1.Setup(a => a.GetCollaborator()).Returns(collab1.Object);
            assoc2.Setup(a => a.GetCollaborator()).Returns(collab2.Object);

            List<ICollaborator> expected = new List<ICollaborator>()
            {
                collab1.Object,
                collab2.Object
            };

            assocRepoMock.Setup(a => a.FindAllByProject(projectMock.Object)).Returns(associations);

            var assoc = new CollaboratorService(assocRepoMock.Object);

            //act
            var result = assoc.FindAllByProject(projectMock.Object);

            //assert
            Assert.True(expected.SequenceEqual(result));
        }

        [Fact]
        public void WhenProjectHasNoCollaborators_ThenReturnEmptyList()
        {
            // Arrange
            Mock<IProject> projectMock = new Mock<IProject>();

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();

            // No associations for the project
            List<IAssociationProjectCollaborator> emptyAssociations = new List<IAssociationProjectCollaborator>();

            assocRepoMock.Setup(a => a.FindAllByProject(projectMock.Object)).Returns(emptyAssociations);

            var assoc = new CollaboratorService(assocRepoMock.Object);

            // Act
            var result = assoc.FindAllByProject(projectMock.Object);

            // Assert
            Assert.Empty(result);
        }


        [Fact]
        public void WhenProjectHasCollaboratorsInPeriod_ThenReturnAllCollaborators()
        {
            // Arrange
            Mock<IProject> projectMock = new Mock<IProject>();
            DateOnly initDate = new DateOnly(2023, 1, 1);
            DateOnly finalDate = new DateOnly(2023, 12, 31);

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();

            Mock<IAssociationProjectCollaborator> assoc1 = new Mock<IAssociationProjectCollaborator>();
            Mock<IAssociationProjectCollaborator> assoc2 = new Mock<IAssociationProjectCollaborator>();

            List<IAssociationProjectCollaborator> associations = new List<IAssociationProjectCollaborator>()
            {
                assoc1.Object,
                assoc2.Object,
            };

            Mock<ICollaborator> collab1 = new Mock<ICollaborator>();
            Mock<ICollaborator> collab2 = new Mock<ICollaborator>();

            assoc1.Setup(a => a.GetCollaborator()).Returns(collab1.Object);
            assoc2.Setup(a => a.GetCollaborator()).Returns(collab2.Object);

            List<ICollaborator> expected = new List<ICollaborator>()
            {
                collab1.Object,
                collab2.Object
            };

            assocRepoMock.Setup(a => a.FindAllByProjectAndBetweenPeriod(projectMock.Object, initDate, finalDate)).Returns(associations);

            var service = new CollaboratorService(assocRepoMock.Object);

            // Act
            var result = service.FindAllByProjectAndBetweenPeriod(projectMock.Object, initDate, finalDate);

            // Assert
            Assert.True(expected.SequenceEqual(result));
        }


        [Fact]
        public void WhenProjectHasNoCollaboratorsInPeriod_ThenReturnEmptyList()
        {
            // Arrange
            Mock<IProject> projectMock = new Mock<IProject>();
            DateOnly initDate = new DateOnly(2023, 1, 1);
            DateOnly finalDate = new DateOnly(2023, 12, 31);

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();

            // No associations for the project in the period
            List<IAssociationProjectCollaborator> emptyAssociations = new List<IAssociationProjectCollaborator>();

            assocRepoMock.Setup(a => a.FindAllByProjectAndBetweenPeriod(projectMock.Object, initDate, finalDate)).Returns(emptyAssociations);

            var service = new CollaboratorService(assocRepoMock.Object);

            // Act
            var result = service.FindAllByProjectAndBetweenPeriod(projectMock.Object, initDate, finalDate);

            // Assert
            Assert.Empty(result);
        }
    }
}
