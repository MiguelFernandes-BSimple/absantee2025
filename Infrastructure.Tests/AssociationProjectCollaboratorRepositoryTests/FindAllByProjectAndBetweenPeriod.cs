namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryTests;

using Moq;
using Domain.Interfaces;
using Infrastructure.Repositories;

public class FindAllByProjectAndBetweenPeriod
{
    [Fact]
    public void WhenProjectHasNoAssociation_ThenReturnEmptyList()
    {
        //arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
        };

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
        assocMock1.Setup(a => a.AssociationIntersectPeriod(It.IsAny<IPeriodDate>())).Returns(true);

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //act
        var result = assoc.FindAllByProjectAndBetweenPeriod(projectMock.Object, It.IsAny<IPeriodDate>());

        //assert
        Assert.Empty(result);
    }

    [Fact]
    public void WhenDatesDontIntersect_ThenReturnEmptyList()
    {
        //arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
        };

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(true);
        assocMock1.Setup(a => a.AssociationIntersectPeriod(It.IsAny<IPeriodDate>())).Returns(false);

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //act
        var result = assoc.FindAllByProjectAndBetweenPeriod(projectMock.Object, It.IsAny<IPeriodDate>());

        //assert
        Assert.Empty(result);
    }

    [Fact]
    public void WhenProjectHasNoAssociation_AndDatesDontIntersect_ThenReturnEmptyList()
    {
        //arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
        };

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
        assocMock1.Setup(a => a.AssociationIntersectPeriod(It.IsAny<IPeriodDate>())).Returns(false);

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //act
        var result = assoc.FindAllByProjectAndBetweenPeriod(projectMock.Object, It.IsAny<IPeriodDate>());

        //assert
        Assert.Empty(result);
    }


    [Fact]
    public void WhenProjectHasAssociationAndDatesIntersect_ThenReturnCollaborator()
    {
        //arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
        };

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(true);
        assocMock1.Setup(a => a.AssociationIntersectPeriod(It.IsAny<IPeriodDate>())).Returns(true);

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        List<IAssociationProjectCollaborator> expected = associationsProjectCollaborator;

        //act
        var result = assoc.FindAllByProjectAndBetweenPeriod(projectMock.Object, It.IsAny<IPeriodDate>());

        //assert
        Assert.True(expected.SequenceEqual(result));
    }
}
