using Domain;
using Moq;

namespace Domain.Tests;

public class CollaboratorRepositoryTest
{
    [Fact]
    public void WhenPassingCorrectValues_ThenObjectIsCreated()
    {
        //Arrange
        Mock<ICollaborator> doubleCollab = new Mock<ICollaborator>();

        List<ICollaborator> listCollab = new List<ICollaborator> { doubleCollab.Object };

        //Act
        new CollaboratorRepository(listCollab);

        //Assert
    }

    [Fact]
    public void WhenQueriedFindAllCollaborators_ThenReturnCollaboratorList()
    {
        //Arrange
        //Double for collaborators to be added
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator3 = new Mock<ICollaborator>();

        //Create a collaborator list
        List<ICollaborator> expectedList = new List<ICollaborator> {
            doubleCollaborator1.Object,
            doubleCollaborator2.Object,
            doubleCollaborator3.Object };

        //Instatiate CollaboratorRepository with the list 
        CollaboratorRepository collabRepo = new CollaboratorRepository(expectedList);

        //Act
        //Get all Collaborators
        IEnumerable<ICollaborator> result = collabRepo.FindAllCollaborators();

        // convert to list for comparison
        List<ICollaborator> resultList = result.ToList();

        //Assert
        //Verifying that the lists are the same
        Assert.True(expectedList.SequenceEqual(resultList));
    }

    [Fact]
    public void WhenPassingCorrectName_ThenReturnCollaboratorList()
    {
        //Arrange
        //Double for collaborators to be added
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator3 = new Mock<ICollaborator>();

        string names = "First names";

        doubleCollaborator1.Setup(c => c.HasNames(names)).Returns(true);
        doubleCollaborator2.Setup(c => c.HasNames(names)).Returns(false);
        doubleCollaborator3.Setup(c => c.HasNames(names)).Returns(true);

        //Create a collaborator list
        List<ICollaborator> totalList = new List<ICollaborator> {
            doubleCollaborator1.Object,
            doubleCollaborator2.Object,
            doubleCollaborator3.Object };

        //Create Expected list
        List<ICollaborator> expectedList = new List<ICollaborator> {
            doubleCollaborator1.Object,
            doubleCollaborator3.Object };

        //Instatiate CollaboratorRepository with the list 
        CollaboratorRepository collabRepo = new CollaboratorRepository(totalList);

        //Act
        //Get all Collaborators
        IEnumerable<ICollaborator> result = collabRepo.FindAllCollaboratorsWithName(names);

        // convert to list for comparison
        List<ICollaborator> resultList = result.ToList();

        //Assert
        //Verifying that the lists only has the elements that were not filtered
        Assert.True(expectedList.SequenceEqual(resultList));
    }

    [Fact]
    public void WhenPassingCorrectSurname_ThenReturnCollaboratorList()
    {
        //Arrange
        //Double for collaborators to be added
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator3 = new Mock<ICollaborator>();

        string surnames = "Surnames";

        doubleCollaborator1.Setup(c => c.HasSurnames(surnames)).Returns(true);
        doubleCollaborator2.Setup(c => c.HasSurnames(surnames)).Returns(false);
        doubleCollaborator3.Setup(c => c.HasSurnames(surnames)).Returns(true);

        //Create a collaborator list
        List<ICollaborator> totalList = new List<ICollaborator> {
            doubleCollaborator1.Object,
            doubleCollaborator2.Object,
            doubleCollaborator3.Object };

        //Create Expected list
        List<ICollaborator> expectedList = new List<ICollaborator> {
            doubleCollaborator1.Object,
            doubleCollaborator3.Object };

        //Instatiate CollaboratorRepository with the list 
        CollaboratorRepository collabRepo = new CollaboratorRepository(totalList);

        //Act
        //Get all Collaborators
        IEnumerable<ICollaborator> result = collabRepo.FindAllCollaboratorsWithSurname(surnames);

        // convert to list for comparison
        List<ICollaborator> resultList = result.ToList();

        //Assert
        //Verifying that the lists are the same
        Assert.True(expectedList.SequenceEqual(resultList));
    }

    [Fact]
    public void WhenPassingCorrectNameAndSurname_ThenReturnCollaboratorList()
    {
        //Arrange
        //Double for collaborators to be added
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator3 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator4 = new Mock<ICollaborator>();

        string names = "First names";
        string surnames = "Surnames";

        //Setup the names for each collaborator
        doubleCollaborator1.Setup(c => c.HasNames(names)).Returns(true);
        doubleCollaborator2.Setup(c => c.HasNames(names)).Returns(false);
        doubleCollaborator3.Setup(c => c.HasNames(names)).Returns(true);
        doubleCollaborator4.Setup(c => c.HasNames(names)).Returns(false);

        //Setup the names for each collaborator
        doubleCollaborator1.Setup(c => c.HasSurnames(surnames)).Returns(false);
        doubleCollaborator2.Setup(c => c.HasSurnames(surnames)).Returns(false);
        doubleCollaborator3.Setup(c => c.HasSurnames(surnames)).Returns(true);
        doubleCollaborator4.Setup(c => c.HasSurnames(surnames)).Returns(true);

        //Create a collaborator list
        List<ICollaborator> totalList = new List<ICollaborator> {
            doubleCollaborator1.Object,
            doubleCollaborator2.Object,
            doubleCollaborator3.Object,
            doubleCollaborator4.Object };

        //Create Expected list
        List<ICollaborator> expectedList = new List<ICollaborator> { doubleCollaborator3.Object };

        //Instatiate CollaboratorRepository with the list 
        CollaboratorRepository collabRepo = new CollaboratorRepository(totalList);

        //Act
        //Get all Collaborators
        IEnumerable<ICollaborator> result = collabRepo.FindAllCollaboratorsWithNameAndSurname(names, surnames);

        // convert to list for comparison
        List<ICollaborator> resultList = result.ToList();

        //Assert
        //Verifying that the lists are the same
        Assert.True(expectedList.SequenceEqual(resultList));
    }
}