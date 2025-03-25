using Domain;
using Moq;

namespace Domain.Tests;

public class ColaboratorRepositoryTest
{
    [Fact]
    public void WhenPassingCorrectValues_ThenObjectIsCreated()
    {
        //Arrange
        Mock<IColaborator> doubleCollab = new Mock<IColaborator>();

        List<IColaborator> listCollab = new List<IColaborator> { doubleCollab.Object };

        //Act
        new ColaboratorRepository(listCollab);

        //Assert
    }

    [Fact]
    public void WhenQueriedFindAllColaborators_ThenReturnCollaboratorList()
    {
        //Arrange
        //Double for colaborators to be added
        Mock<IColaborator> doubleColaborator1 = new Mock<IColaborator>();
        Mock<IColaborator> doubleColaborator2 = new Mock<IColaborator>();
        Mock<IColaborator> doubleColaborator3 = new Mock<IColaborator>();

        //Create a colaborator list
        List<IColaborator> expectedList = new List<IColaborator> {
            doubleColaborator1.Object,
            doubleColaborator2.Object,
            doubleColaborator3.Object };

        //Instatiate ColaboratorRepository with the list 
        ColaboratorRepository collabRepo = new ColaboratorRepository(expectedList);

        //Act
        //Get all Colaborators
        IEnumerable<IColaborator> result = collabRepo.FindAllColaborators();

        // convert to list for comparison
        List<IColaborator> resultList = result.ToList();

        //Assert
        //Verifying that the lists are the same
        Assert.Equal(expectedList.Count(), resultList.Count());
        Assert.True(expectedList.SequenceEqual(resultList));
    }

    [Fact]
    public void WhenPassingCorrectName_ThenReturnCollaboratorList()
    {
        //Arrange
        //Double for colaborators to be added
        Mock<IColaborator> doubleColaborator1 = new Mock<IColaborator>();
        Mock<IColaborator> doubleColaborator2 = new Mock<IColaborator>();
        Mock<IColaborator> doubleColaborator3 = new Mock<IColaborator>();

        string names = "First names";

        doubleColaborator1.Setup(c => c.HasNames(names)).Returns(true);
        doubleColaborator2.Setup(c => c.HasNames(names)).Returns(false);
        doubleColaborator3.Setup(c => c.HasNames(names)).Returns(true);

        //Create a colaborator list
        List<IColaborator> totalList = new List<IColaborator> {
            doubleColaborator1.Object,
            doubleColaborator2.Object,
            doubleColaborator3.Object };

        //Create Expected list
        List<IColaborator> expectedList = new List<IColaborator> {
            doubleColaborator1.Object,
            doubleColaborator3.Object };

        //Instatiate ColaboratorRepository with the list 
        ColaboratorRepository collabRepo = new ColaboratorRepository(totalList);

        //Act
        //Get all Colaborators
        IEnumerable<IColaborator> result = collabRepo.FindAllColaboratorsWithName(names);

        // convert to list for comparison
        List<IColaborator> resultList = result.ToList();

        //Assert
        //Verifying that the lists only has the elements that were not filtered
        Assert.Equal(expectedList.Count(), resultList.Count());
        Assert.True(expectedList.SequenceEqual(resultList));
    }

    [Fact]
    public void WhenPassingIncorrectName_ThenReturnEmptyList()
    {
        //Arrange
        //Double for colaborators to be added
        Mock<IColaborator> doubleColaborator1 = new Mock<IColaborator>();
        Mock<IColaborator> doubleColaborator2 = new Mock<IColaborator>();
        Mock<IColaborator> doubleColaborator3 = new Mock<IColaborator>();

        string names = "First names";

        doubleColaborator1.Setup(c => c.HasNames(names)).Returns(false);
        doubleColaborator2.Setup(c => c.HasNames(names)).Returns(false);
        doubleColaborator3.Setup(c => c.HasNames(names)).Returns(false);

        //Create a colaborator list
        List<IColaborator> totalList = new List<IColaborator> {
            doubleColaborator1.Object,
            doubleColaborator2.Object,
            doubleColaborator3.Object };

        //Create Expected list
        List<IColaborator> expectedList = new List<IColaborator> { };

        //Instatiate ColaboratorRepository with the list 
        ColaboratorRepository collabRepo = new ColaboratorRepository(totalList);

        //Act
        //Get all Colaborators
        IEnumerable<IColaborator> result = collabRepo.FindAllColaboratorsWithName(names);

        // convert to list for comparison
        List<IColaborator> resultList = result.ToList();

        //Assert
        //Verifying that the lists only has the elements that were not filtered
        Assert.Equal(expectedList.Count(), resultList.Count());
        Assert.True(expectedList.SequenceEqual(resultList));
    }

    [Fact]
    public void WhenPassingCorrectSurname_ThenReturnCollaboratorList()
    {
        //Arrange
        //Double for colaborators to be added
        Mock<IColaborator> doubleColaborator1 = new Mock<IColaborator>();
        Mock<IColaborator> doubleColaborator2 = new Mock<IColaborator>();
        Mock<IColaborator> doubleColaborator3 = new Mock<IColaborator>();

        string surnames = "Surnames";

        doubleColaborator1.Setup(c => c.HasSurnames(surnames)).Returns(true);
        doubleColaborator2.Setup(c => c.HasSurnames(surnames)).Returns(false);
        doubleColaborator3.Setup(c => c.HasSurnames(surnames)).Returns(true);

        //Create a colaborator list
        List<IColaborator> totalList = new List<IColaborator> {
            doubleColaborator1.Object,
            doubleColaborator2.Object,
            doubleColaborator3.Object };

        //Create Expected list
        List<IColaborator> expectedList = new List<IColaborator> {
            doubleColaborator1.Object,
            doubleColaborator3.Object };

        //Instatiate ColaboratorRepository with the list 
        ColaboratorRepository collabRepo = new ColaboratorRepository(totalList);

        //Act
        //Get all Colaborators
        IEnumerable<IColaborator> result = collabRepo.FindAllColaboratorsWithSurname(surnames);

        // convert to list for comparison
        List<IColaborator> resultList = result.ToList();

        //Assert
        //Verifying that the lists are the same
        Assert.Equal(expectedList.Count(), resultList.Count());
        Assert.True(expectedList.SequenceEqual(resultList));
    }

    [Fact]
    public void WhenPassingIncorrectSurname_ThenReturnEmptyList()
    {
        //Arrange
        //Double for colaborators to be added
        Mock<IColaborator> doubleColaborator1 = new Mock<IColaborator>();
        Mock<IColaborator> doubleColaborator2 = new Mock<IColaborator>();
        Mock<IColaborator> doubleColaborator3 = new Mock<IColaborator>();

        string surnames = "Surnames";

        doubleColaborator1.Setup(c => c.HasSurnames(surnames)).Returns(false);
        doubleColaborator2.Setup(c => c.HasSurnames(surnames)).Returns(false);
        doubleColaborator3.Setup(c => c.HasSurnames(surnames)).Returns(false);

        //Create a colaborator list
        List<IColaborator> totalList = new List<IColaborator> {
            doubleColaborator1.Object,
            doubleColaborator2.Object,
            doubleColaborator3.Object };

        //Create Expected list
        List<IColaborator> expectedList = new List<IColaborator> { };

        //Instatiate ColaboratorRepository with the list 
        ColaboratorRepository collabRepo = new ColaboratorRepository(totalList);

        //Act
        //Get all Colaborators
        IEnumerable<IColaborator> result = collabRepo.FindAllColaboratorsWithSurname(surnames);

        // convert to list for comparison
        List<IColaborator> resultList = result.ToList();

        //Assert
        //Verifying that the lists are the same
        Assert.Equal(expectedList.Count(), resultList.Count());
        Assert.True(expectedList.SequenceEqual(resultList));
    }

    [Fact]
    public void WhenPassingCorrectNameAndSurname_ThenReturnCollaboratorList()
    {
        //Arrange
        //Double for colaborators to be added
        Mock<IColaborator> doubleColaborator1 = new Mock<IColaborator>();
        Mock<IColaborator> doubleColaborator2 = new Mock<IColaborator>();
        Mock<IColaborator> doubleColaborator3 = new Mock<IColaborator>();
        Mock<IColaborator> doubleColaborator4 = new Mock<IColaborator>();

        string names = "First names";
        string surnames = "Surnames";

        //Setup the names for each colaborator
        doubleColaborator1.Setup(c => c.HasNames(names)).Returns(true);
        doubleColaborator2.Setup(c => c.HasNames(names)).Returns(false);
        doubleColaborator3.Setup(c => c.HasNames(names)).Returns(true);
        doubleColaborator4.Setup(c => c.HasNames(names)).Returns(false);

        //Setup the names for each colaborator
        doubleColaborator1.Setup(c => c.HasSurnames(surnames)).Returns(false);
        doubleColaborator2.Setup(c => c.HasSurnames(surnames)).Returns(false);
        doubleColaborator3.Setup(c => c.HasSurnames(surnames)).Returns(true);
        doubleColaborator4.Setup(c => c.HasSurnames(surnames)).Returns(true);

        //Create a colaborator list
        List<IColaborator> totalList = new List<IColaborator> {
            doubleColaborator1.Object,
            doubleColaborator2.Object,
            doubleColaborator3.Object,
            doubleColaborator4.Object };

        //Create Expected list
        List<IColaborator> expectedList = new List<IColaborator> { doubleColaborator3.Object };

        //Instatiate ColaboratorRepository with the list 
        ColaboratorRepository collabRepo = new ColaboratorRepository(totalList);

        //Act
        //Get all Colaborators
        IEnumerable<IColaborator> result = collabRepo.FindAllColaboratorsWithNameAndSurname(names, surnames);

        // convert to list for comparison
        List<IColaborator> resultList = result.ToList();

        //Assert
        //Verifying that the lists are the same
        Assert.Equal(expectedList.Count(), resultList.Count());
        Assert.True(expectedList.SequenceEqual(resultList));
    }

    [Fact]
    public void WhenPassingIncorrectNameOrSurnameOrBoth_ThenReturnEmptyList()
    {
        //Arrange
        //Double for colaborators to be added
        Mock<IColaborator> doubleColaborator1 = new Mock<IColaborator>();
        Mock<IColaborator> doubleColaborator2 = new Mock<IColaborator>();
        Mock<IColaborator> doubleColaborator3 = new Mock<IColaborator>();

        string names = "First names";
        string surnames = "Surnames";

        //Setup the names for each colaborator
        doubleColaborator1.Setup(c => c.HasNames(names)).Returns(true);
        doubleColaborator2.Setup(c => c.HasNames(names)).Returns(false);
        doubleColaborator3.Setup(c => c.HasNames(names)).Returns(false);

        //Setup the names for each colaborator
        doubleColaborator1.Setup(c => c.HasSurnames(surnames)).Returns(false);
        doubleColaborator2.Setup(c => c.HasSurnames(surnames)).Returns(false);
        doubleColaborator3.Setup(c => c.HasSurnames(surnames)).Returns(true);

        //Create a colaborator list
        List<IColaborator> totalList = new List<IColaborator> {
            doubleColaborator1.Object,
            doubleColaborator2.Object,
            doubleColaborator3.Object };

        //Create Expected list
        List<IColaborator> expectedList = new List<IColaborator> { };

        //Instatiate ColaboratorRepository with the list 
        ColaboratorRepository collabRepo = new ColaboratorRepository(totalList);

        //Act
        //Get all Colaborators
        IEnumerable<IColaborator> result = collabRepo.FindAllColaboratorsWithNameAndSurname(names, surnames);

        // convert to list for comparison
        List<IColaborator> resultList = result.ToList();

        //Assert
        //Verifying that the lists are the same
        Assert.Equal(expectedList.Count(), resultList.Count());
        Assert.True(expectedList.SequenceEqual(resultList));
    }
}