using Moq;
using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Domain.Models;
using System.Linq.Expressions;

namespace Application.Tests.CollaboratorServiceTests
{
    public class FindAllByProject
    {
        [Fact]
        public void WhenFindingCollaboratorsByProject_ThenReturnAllAssociatedCollaborators()
        {
            //arrange
            Mock<IProject> projectMock = new Mock<IProject>();


            Mock<ICollaborator> collab1 = new Mock<ICollaborator>();
            Mock<ICollaborator> collab2 = new Mock<ICollaborator>();

            IEnumerable<ICollaborator> expected = new List<ICollaborator>()
            {
                collab1.Object,
                collab2.Object
            };

            Mock<IAssociationProjectCollaborator> assoc1 = new Mock<IAssociationProjectCollaborator>();
            Mock<IAssociationProjectCollaborator> assoc2 = new Mock<IAssociationProjectCollaborator>();

            assoc1.Setup(a => a.GetCollaboratorId()).Returns(It.IsAny<long>());
            assoc2.Setup(a => a.GetCollaboratorId()).Returns(It.IsAny<long>());

            List<IAssociationProjectCollaborator> associations = new List<IAssociationProjectCollaborator>()
            {
                assoc1.Object,
                assoc2.Object,
            };

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            assocRepoMock.Setup(a => a.FindAllByProject(projectMock.Object)).Returns(associations);

            Mock<IHolidayPlanRepository> holidayPlanRepoMock = new Mock<IHolidayPlanRepository>();
            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();

            collabRepository.Setup(c => c.Find(It.IsAny<Expression<Func<ICollaborator, bool>>>())).Returns(expected);

            var collabService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepoMock.Object, collabRepository.Object);

            //act
            var result = collabService.FindAllByProject(projectMock.Object);

            //assert
            Assert.True(expected.SequenceEqual(result));
        }

        [Fact]
        public void WhenProjectHasNoCollaborators_ThenReturnEmptyList()
        {
            // Arrange
            Mock<IProject> projectMock = new Mock<IProject>();

            Mock<IHolidayPlanRepository> holidayPlanRepoMock = new Mock<IHolidayPlanRepository>();
            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();

            // No associations for the project
            List<IAssociationProjectCollaborator> emptyAssociations = new List<IAssociationProjectCollaborator>();

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            assocRepoMock.Setup(a => a.FindAllByProject(projectMock.Object)).Returns(emptyAssociations);

            var collabService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepoMock.Object, collabRepository.Object);

            // Act
            var result = collabService.FindAllByProject(projectMock.Object);

            // Assert
            Assert.Empty(result);
        }
    }
}
