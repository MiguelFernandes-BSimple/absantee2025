using Moq;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryTests;

public class FindAllByProjectAndBetweenPeriodAsync
{
    [Fact]
    public async Task WhenProjectHasNoAssociation_ThenReturnEmptyListAsync()
    {
        // arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
        };

        var periodDouble = new Mock<IPeriodDate>();

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
        assocMock1.Setup(a => a.AssociationIntersectPeriod(periodDouble.Object)).Returns(true);

        var assocRepo = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        // act
        var result = await assocRepo.FindAllByProjectAndBetweenPeriodAsync(projectMock.Object, periodDouble.Object);

        // assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task WhenDatesDontIntersect_ThenReturnEmptyListAsync()
    {
        // arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
        };

        var periodDouble = new Mock<IPeriodDate>();

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(true);
        assocMock1.Setup(a => a.AssociationIntersectPeriod(periodDouble.Object)).Returns(false);

        var assocRepo = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        // act
        var result = await assocRepo.FindAllByProjectAndBetweenPeriodAsync(projectMock.Object, periodDouble.Object);

        // assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task WhenProjectHasNoAssociation_AndDatesDontIntersect_ThenReturnEmptyListAsync()
    {
        // arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
        };

        var periodDouble = new Mock<IPeriodDate>();

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
        assocMock1.Setup(a => a.AssociationIntersectPeriod(periodDouble.Object)).Returns(false);

        var assocRepo = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        // act
        var result = await assocRepo.FindAllByProjectAndBetweenPeriodAsync(projectMock.Object, periodDouble.Object);

        // assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task WhenProjectHasAssociationAndDatesIntersect_ThenReturnCollaboratorAsync()
    {
        // arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
        };

        var periodDouble = new Mock<IPeriodDate>();

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(true);
        assocMock1.Setup(a => a.AssociationIntersectPeriod(periodDouble.Object)).Returns(true);

        var assocRepo = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        List<IAssociationProjectCollaborator> expected = associationsProjectCollaborator;

        // act
        var result = await assocRepo.FindAllByProjectAndBetweenPeriodAsync(projectMock.Object, periodDouble.Object);

        // assert
        Assert.True(expected.SequenceEqual(result));
    }
}
