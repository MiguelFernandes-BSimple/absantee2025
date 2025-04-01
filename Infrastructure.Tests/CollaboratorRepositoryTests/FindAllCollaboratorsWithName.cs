namespace Infrastructure.Tests.CollaboratorRepositoryTests;

using Moq;
using Domain.Interfaces;
using Infrastructure.Repositories;

public class FindAllCollaboratorsWithName
{
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
}
