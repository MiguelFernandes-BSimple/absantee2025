namespace Infrastructure.Tests.CollaboratorRepositoryTests;

using Moq;
using Domain.Interfaces;
using Infrastructure.Repositories;
using System.Threading.Tasks;

public class FindAllCollaboratorsWithNameAsync
{
    [Fact]
    public async Task WhenPassingCorrectName_ThenReturnCollaboratorList()
    {
        //Arrange
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator3 = new Mock<ICollaborator>();

        doubleCollaborator1.Setup(c1 => c1.Equals(It.IsAny<ICollaborator>())).Returns(false);
        doubleCollaborator2.Setup(c2 => c2.Equals(It.IsAny<ICollaborator>())).Returns(false);
        doubleCollaborator3.Setup(c3 => c3.Equals(It.IsAny<ICollaborator>())).Returns(false);

        string names = "First names";

        doubleCollaborator1.Setup(c => c.HasNames(names)).Returns(true);
        doubleCollaborator2.Setup(c => c.HasNames(names)).Returns(false);
        doubleCollaborator3.Setup(c => c.HasNames(names)).Returns(true);

        var totalList = new List<ICollaborator> { doubleCollaborator1.Object, doubleCollaborator2.Object, doubleCollaborator3.Object };

        var expectedList = new List<ICollaborator> { doubleCollaborator1.Object, doubleCollaborator3.Object };

        var collabRepo = new CollaboratorRepository(totalList);

        //Act
        var result = await collabRepo.FindAllCollaboratorsWithNameAsync(names);

        var resultList = result.ToList();

        //Assert
        Assert.True(expectedList.SequenceEqual(resultList));
    }

    [Fact]
    public async Task WhenPassingUnknownName_ThenReturnEmptyList()
    {
        //Arrange
        var doubleCollaborator1 = new Mock<ICollaborator>();
        var doubleCollaborator2 = new Mock<ICollaborator>();
        var doubleCollaborator3 = new Mock<ICollaborator>();

        doubleCollaborator1.Setup(c1 => c1.Equals(It.IsAny<ICollaborator>())).Returns(false);
        doubleCollaborator2.Setup(c2 => c2.Equals(It.IsAny<ICollaborator>())).Returns(false);
        doubleCollaborator3.Setup(c3 => c3.Equals(It.IsAny<ICollaborator>())).Returns(false);

        string names = "First names";

        doubleCollaborator1.Setup(c => c.HasNames(names)).Returns(false);
        doubleCollaborator2.Setup(c => c.HasNames(names)).Returns(false);
        doubleCollaborator3.Setup(c => c.HasNames(names)).Returns(false);

        var totalList = new List<ICollaborator> { doubleCollaborator1.Object, doubleCollaborator2.Object, doubleCollaborator3.Object };

        var expectedList = new List<ICollaborator> { };

        var collabRepo = new CollaboratorRepository(totalList);

        //Act
        var result = await collabRepo.FindAllCollaboratorsWithNameAsync(names);

        var resultList = result.ToList();

        //Assert
        Assert.True(expectedList.SequenceEqual(resultList));
    }
}