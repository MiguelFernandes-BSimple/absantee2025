namespace Infrastructure.Tests.CollaboratorRepositoryTests;

using Moq;
using Domain.Interfaces;
using Infrastructure.Repositories;
using System.Threading.Tasks;

public class FindAllCollaboratorsAsync
{

    [Fact]
    public async Task WhenQueriedFindAllCollaboratorsAsync_ThenReturnCollaboratorList()
    {
        //Arrange
        var doubleCollaborator1 = new Mock<ICollaborator>();
        var doubleCollaborator2 = new Mock<ICollaborator>();

        doubleCollaborator1.Setup(c1 => c1.Equals(doubleCollaborator2.Object)).Returns(false);
        doubleCollaborator2.Setup(c2 => c2.Equals(doubleCollaborator1.Object)).Returns(false);

        var expectedList = new List<ICollaborator> { doubleCollaborator1.Object, doubleCollaborator2.Object };

        var collabRepo = new CollaboratorRepository(expectedList);

        //Act
        var result = await collabRepo.FindAllCollaboratorsAsync();

        var resultList = result.ToList();

        //Assert
        Assert.True(expectedList.SequenceEqual(resultList));
    }
}