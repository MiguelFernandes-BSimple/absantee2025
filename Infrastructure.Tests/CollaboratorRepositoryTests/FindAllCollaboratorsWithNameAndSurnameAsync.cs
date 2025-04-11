namespace Infrastructure.Tests.CollaboratorRepositoryTests;

using Moq;
using Domain.Interfaces;
using Infrastructure.Repositories;
using System.Threading.Tasks;

public class FindAllCollaboratorWithNameAndSurnameAsync
{
    //[Fact]
    //public async Task PassingCorrectNameAndSurname_ThenReturnCollaboratorList()
    //{
    //    //Arrange
    //    var doubleCollaborator1 = new Mock<ICollaborator>();
    //    var doubleCollaborator2 = new Mock<ICollaborator>();
    //    var doubleCollaborator3 = new Mock<ICollaborator>();
    //    var doubleCollaborator4 = new Mock<ICollaborator>();

    //    doubleCollaborator1.Setup(c1 => c1.Equals(It.IsAny<ICollaborator>())).Returns(false);
    //    doubleCollaborator2.Setup(c2 => c2.Equals(It.IsAny<ICollaborator>())).Returns(false);
    //    doubleCollaborator3.Setup(c3 => c3.Equals(It.IsAny<ICollaborator>())).Returns(false);
    //    doubleCollaborator4.Setup(c4 => c4.Equals(It.IsAny<ICollaborator>())).Returns(false);

    //    string names = "First names";
    //    string surnames = "Surnames";

    //    doubleCollaborator1.Setup(c => c.HasNames(names)).Returns(true);
    //    doubleCollaborator2.Setup(c => c.HasNames(names)).Returns(false);
    //    doubleCollaborator3.Setup(c => c.HasNames(names)).Returns(true);
    //    doubleCollaborator4.Setup(c => c.HasNames(names)).Returns(false);

    //    doubleCollaborator1.Setup(c => c.HasSurnames(surnames)).Returns(false);
    //    doubleCollaborator2.Setup(c => c.HasSurnames(surnames)).Returns(false);
    //    doubleCollaborator3.Setup(c => c.HasSurnames(surnames)).Returns(true);
    //    doubleCollaborator4.Setup(c => c.HasSurnames(surnames)).Returns(true);

    //    var totalList = new List<ICollaborator> { doubleCollaborator1.Object, doubleCollaborator2.Object, doubleCollaborator3.Object, doubleCollaborator4.Object };

    //    var expectedList = new List<ICollaborator> { doubleCollaborator3.Object };

    //    var collabRepo = new CollaboratorRepository(totalList);

    //    //Act
    //    var result = await collabRepo.FindAllCollaboratorsWithNameAndSurnameAsync(names, surnames);

    //    var resultList = result.ToList();

    //    //Assert
    //    Assert.True(expectedList.SequenceEqual(resultList));
    //}

    //[Fact]
    //public async Task WhenPassingUnknownNameAndSurname_ThenReturnEmptyList()
    //{
    //    //Arrange
    //    var doubleCollaborator1 = new Mock<ICollaborator>();
    //    var doubleCollaborator2 = new Mock<ICollaborator>();
    //    var doubleCollaborator3 = new Mock<ICollaborator>();

    //    doubleCollaborator1.Setup(c1 => c1.Equals(It.IsAny<ICollaborator>())).Returns(false);
    //    doubleCollaborator2.Setup(c2 => c2.Equals(It.IsAny<ICollaborator>())).Returns(false);
    //    doubleCollaborator3.Setup(c3 => c3.Equals(It.IsAny<ICollaborator>())).Returns(false);

    //    string names = "First names";
    //    string surnames = "Surnames";

    //    doubleCollaborator1.Setup(c => c.HasNames(names)).Returns(true);
    //    doubleCollaborator2.Setup(c => c.HasNames(names)).Returns(false);
    //    doubleCollaborator3.Setup(c => c.HasNames(names)).Returns(false);

    //    doubleCollaborator1.Setup(c => c.HasSurnames(surnames)).Returns(false);
    //    doubleCollaborator2.Setup(c => c.HasSurnames(surnames)).Returns(false);
    //    doubleCollaborator3.Setup(c => c.HasSurnames(surnames)).Returns(true);

    //    var totalList = new List<ICollaborator> { doubleCollaborator1.Object, doubleCollaborator2.Object, doubleCollaborator3.Object };

    //    var expectedList = new List<ICollaborator> { };

    //    var collabRepo = new CollaboratorRepository(totalList);

    //    //Act
    //    var result = await collabRepo.FindAllCollaboratorsWithNameAndSurnameAsync(names, surnames);

    //    var resultList = result.ToList();

    //    //Assert
    //    Assert.True(expectedList.SequenceEqual(resultList));
    //}
}
