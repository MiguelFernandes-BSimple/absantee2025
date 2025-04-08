using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
using System.Linq.Expressions;

namespace Application.Tests.CollaboratorServiceTests
{
    public class FindAllByProjectAndBetweenPeriod
    {
        [Fact]
        public void WhenProjectHasCollaboratorsInPeriod_ThenReturnAllCollaborators()
        {
            // Arrange
            Mock<IProject> projectMock = new Mock<IProject>();
            var periodDouble = new Mock<IPeriodDate>();

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

            assoc1.Setup(a => a.GetCollaboratorId()).Returns(It.IsAny<long>());
            assoc2.Setup(a => a.GetCollaboratorId()).Returns(It.IsAny<long>());

            List<ICollaborator> expected = new List<ICollaborator>()
            {
                collab1.Object,
                collab2.Object
            };

            assocRepoMock.Setup(a => a.FindAllByProjectAndBetweenPeriod(projectMock.Object, periodDouble.Object)).Returns(associations);

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.Find(It.IsAny<Expression<Func<ICollaborator, bool>>>())).Returns(expected);

            var service = new CollaboratorService(assocRepoMock.Object, holidayPlanRepoMock.Object, collabRepository.Object);

            // Act
            var result = service.FindAllByProjectAndBetweenPeriod(projectMock.Object, periodDouble.Object);

            // Assert
            Assert.True(expected.SequenceEqual(result));
        }


        [Fact]
        public void WhenProjectHasNoCollaboratorsInPeriod_ThenReturnEmptyList()
        {
            // Arrange
            Mock<IProject> projectMock = new Mock<IProject>();
            var periodDouble = new Mock<IPeriodDate>();

            Mock<IHolidayPlanRepository> holidayPlanRepoMock = new Mock<IHolidayPlanRepository>();
            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();

            // No associations for the project in the period
            List<IAssociationProjectCollaborator> emptyAssociations = new List<IAssociationProjectCollaborator>();

            assocRepoMock.Setup(a => a.FindAllByProjectAndBetweenPeriod(projectMock.Object, periodDouble.Object)).Returns(emptyAssociations);

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();

            var service = new CollaboratorService(assocRepoMock.Object, holidayPlanRepoMock.Object, collabRepository.Object);

            // Act
            var result = service.FindAllByProjectAndBetweenPeriod(projectMock.Object, periodDouble.Object);

            // Assert
            Assert.Empty(result);
        }
    }
}
