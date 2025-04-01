using Moq;
using Domain;

namespace Application.Tests.CollaboratorServiceTests
{
    public class FindAllByProject
    {
        [Fact]
        public void WhenFindingCollaboratorsByProject_ThenReturnAllAssociatedCollaborators()
        {
            //arrange
            Mock<IProject> projectMock = new Mock<IProject>();

            Mock<IHolidayPlanRepository> holidayPlanRepoMock = new Mock<IHolidayPlanRepository>();
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

            var assoc = new CollaboratorService(assocRepoMock.Object, holidayPlanRepoMock.Object);

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

            Mock<IHolidayPlanRepository> holidayPlanRepoMock = new Mock<IHolidayPlanRepository>();
            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();

            // No associations for the project
            List<IAssociationProjectCollaborator> emptyAssociations = new List<IAssociationProjectCollaborator>();

            assocRepoMock.Setup(a => a.FindAllByProject(projectMock.Object)).Returns(emptyAssociations);

            var assoc = new CollaboratorService(assocRepoMock.Object, holidayPlanRepoMock.Object);

            // Act
            var result = assoc.FindAllByProject(projectMock.Object);

            // Assert
            Assert.Empty(result);
        }
    }
}
