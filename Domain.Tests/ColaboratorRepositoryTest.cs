using Domain;
using Moq;

namespace Domain.Tests;

public class CollaboratorRepositoryTest
{
    /**
    * Test for empty constructor
    */
    [Fact]
    public void WhenNotPassingAnyArguments_ThenObjectIsCreated()
    {
        //Arrange

        //Act
        new CollaboratorRepository();

        //Assert
    }

    /**
    * Test for constructor that receives a list of IColaborator
    */
    [Fact]
    public void WhenPassingCorrectCollaboratorList_ThenObjectIsCreated()
    {
        // Arrange
        // Collaborator doubles - stubs
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator3 = new Mock<ICollaborator>();

        // Create Collaborator List
        List<ICollaborator> collabList =
            new List<ICollaborator> { doubleCollaborator1.Object, doubleCollaborator2.Object, doubleCollaborator3.Object };

        // To be correct, all collaborators must be distinct - Distinct
        // Email is different from the others
        // Set up GetEmail() for each collaborator
        doubleCollaborator1.Setup(c1 => c1.GetEmail()).Returns("email1@example.com");
        doubleCollaborator2.Setup(c2 => c2.GetEmail()).Returns("email2@example.com");
        doubleCollaborator3.Setup(c3 => c3.GetEmail()).Returns("email3@example.com");

        // Act
        new CollaboratorRepository(collabList);

        // Assert
    }

    /**
    * Test for constructor that receives a list of IColaborator that is not valid
    * -> Should throw an exception 
    *
    * One element being invalid is enough for the whole list to be invalid too
    */
    [Fact]
    public void WhenPassingIncorrectCollaboratorList_ThenThrowException()
    {
        // Arrange
        // Collaborator doubles - stubs
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator3 = new Mock<ICollaborator>();

        // Create Collaborator List
        List<ICollaborator> collabList =
            new List<ICollaborator> { doubleCollaborator1.Object, doubleCollaborator2.Object, doubleCollaborator3.Object };

        // Set up two collaborators with the same email
        doubleCollaborator1.Setup(c1 => c1.GetEmail()).Returns("email1@example.com");
        doubleCollaborator2.Setup(c2 => c2.GetEmail()).Returns("email1@example.com");
        doubleCollaborator3.Setup(c3 => c3.GetEmail()).Returns("email3@example.com");

        // Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new CollaboratorRepository(collabList));

        Assert.Equal("Arguments are not valid!", exception.Message);
    }

    /**
    * Test to return all collaborators in the repository
    */
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

        // All elements are distinct
        // Set up GetEmail() for each collaborator
        doubleCollaborator1.Setup(c1 => c1.GetEmail()).Returns("email1@example.com");
        doubleCollaborator2.Setup(c2 => c2.GetEmail()).Returns("email2@example.com");
        doubleCollaborator3.Setup(c3 => c3.GetEmail()).Returns("email3@example.com");

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

    /**
    * Test for finding all users by names 
    * Happy Path
    */
    [Fact]
    public void WhenPassingCorrectName_ThenReturnCollaboratorList()
    {
        //Arrange
        //Double for collaborators to be added
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator3 = new Mock<ICollaborator>();

        // All elements are distinct
        // Set up GetEmail() for each collaborator
        doubleCollaborator1.Setup(c1 => c1.GetEmail()).Returns("email1@example.com");
        doubleCollaborator2.Setup(c2 => c2.GetEmail()).Returns("email2@example.com");
        doubleCollaborator3.Setup(c3 => c3.GetEmail()).Returns("email3@example.com");

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

    /**
        * Test for finding all users by names 
        * Names passed is unknown - no existing collaborator with names
        */
    [Fact]
    public void WhenPassingUnknownName_ThenReturnEmptyList()
    {
        //Arrange
        //Double for collaborators to be added
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator3 = new Mock<ICollaborator>();

        // All elements are distinct
        // Set up GetEmail() for each collaborator
        doubleCollaborator1.Setup(c1 => c1.GetEmail()).Returns("email1@example.com");
        doubleCollaborator2.Setup(c2 => c2.GetEmail()).Returns("email2@example.com");
        doubleCollaborator3.Setup(c3 => c3.GetEmail()).Returns("email3@example.com");

        string names = "First names";

        doubleCollaborator1.Setup(c => c.HasNames(names)).Returns(false);
        doubleCollaborator2.Setup(c => c.HasNames(names)).Returns(false);
        doubleCollaborator3.Setup(c => c.HasNames(names)).Returns(false);

        //Create a collaborator list
        List<ICollaborator> totalList = new List<ICollaborator> {
            doubleCollaborator1.Object,
            doubleCollaborator2.Object,
            doubleCollaborator3.Object };

        //Create Expected list
        List<ICollaborator> expectedList = new List<ICollaborator> { };

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

    /**
    * Test for finding all users by surnames 
    * Happy Path
    */
    [Fact]
    public void WhenPassingCorrectSurname_ThenReturnCollaboratorList()
    {
        //Arrange
        //Double for collaborators to be added
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator3 = new Mock<ICollaborator>();

        // All elements are distinct
        // Set up GetEmail() for each collaborator
        doubleCollaborator1.Setup(c1 => c1.GetEmail()).Returns("email1@example.com");
        doubleCollaborator2.Setup(c2 => c2.GetEmail()).Returns("email2@example.com");
        doubleCollaborator3.Setup(c3 => c3.GetEmail()).Returns("email3@example.com");

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

    /**
    * Test for finding all users by surnames 
    * Surnames passed are unknown - no existing collaborators with surnames
    */
    [Fact]
    public void WhenPassingUnknownSurname_ThenReturnEmptyList()
    {
        //Arrange
        //Double for collaborators to be added
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator3 = new Mock<ICollaborator>();

        // All elements are distinct
        // Set up GetEmail() for each collaborator
        doubleCollaborator1.Setup(c1 => c1.GetEmail()).Returns("email1@example.com");
        doubleCollaborator2.Setup(c2 => c2.GetEmail()).Returns("email2@example.com");
        doubleCollaborator3.Setup(c3 => c3.GetEmail()).Returns("email3@example.com");

        string surnames = "Surnames";

        doubleCollaborator1.Setup(c => c.HasSurnames(surnames)).Returns(false);
        doubleCollaborator2.Setup(c => c.HasSurnames(surnames)).Returns(false);
        doubleCollaborator3.Setup(c => c.HasSurnames(surnames)).Returns(false);

        //Create a collaborator list
        List<ICollaborator> totalList = new List<ICollaborator> {
            doubleCollaborator1.Object,
            doubleCollaborator2.Object,
            doubleCollaborator3.Object };

        //Create Expected list
        List<ICollaborator> expectedList = new List<ICollaborator> { };

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

    /**
    * Test for finding all users by names and surnames, simultaneoulsy
    * Happy Path
    */
    [Fact]
    public void WhenPassingCorrectNameAndSurname_ThenReturnCollaboratorList()
    {
        //Arrange
        //Double for collaborators to be added
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator3 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator4 = new Mock<ICollaborator>();

        // All elements are distinct
        // Set up GetEmail() for each collaborator
        doubleCollaborator1.Setup(c1 => c1.GetEmail()).Returns("email1@example.com");
        doubleCollaborator2.Setup(c2 => c2.GetEmail()).Returns("email2@example.com");
        doubleCollaborator3.Setup(c3 => c3.GetEmail()).Returns("email3@example.com");
        doubleCollaborator4.Setup(c4 => c4.GetEmail()).Returns("email4@example.com");

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

    /**
    * Test for finding all users by names and surnames, simultaneously
    * The resulting list of collaborators have to possess both the names and surnames inputed
    * Else they are not valid
    */
    [Fact]
    public void WhenPassingUnknownNameAndSurname_ThenReturnEmptyList()
    {
        //Arrange
        //Double for collaborators to be added
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator3 = new Mock<ICollaborator>();

        // All elements are distinct
        // Set up GetEmail() for each collaborator
        doubleCollaborator1.Setup(c1 => c1.GetEmail()).Returns("email1@example.com");
        doubleCollaborator2.Setup(c2 => c2.GetEmail()).Returns("email2@example.com");
        doubleCollaborator3.Setup(c3 => c3.GetEmail()).Returns("email3@example.com");

        string names = "First names";
        string surnames = "Surnames";

        //Setup the names for each collaborator
        doubleCollaborator1.Setup(c => c.HasNames(names)).Returns(true);
        doubleCollaborator2.Setup(c => c.HasNames(names)).Returns(false);
        doubleCollaborator3.Setup(c => c.HasNames(names)).Returns(false);

        //Setup the names for each collaborator
        doubleCollaborator1.Setup(c => c.HasSurnames(surnames)).Returns(false);
        doubleCollaborator2.Setup(c => c.HasSurnames(surnames)).Returns(false);
        doubleCollaborator3.Setup(c => c.HasSurnames(surnames)).Returns(true);

        //Create a collaborator list
        List<ICollaborator> totalList = new List<ICollaborator> {
            doubleCollaborator1.Object,
            doubleCollaborator2.Object,
            doubleCollaborator3.Object };

        //Create Expected list
        List<ICollaborator> expectedList = new List<ICollaborator> { };

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