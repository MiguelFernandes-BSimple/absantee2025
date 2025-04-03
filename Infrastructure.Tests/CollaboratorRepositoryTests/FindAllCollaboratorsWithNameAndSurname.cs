namespace Infrastructure.Tests.CollaboratorRepositoryTests;

using Moq;
using Domain.Interfaces;
using Infrastructure.Repositories;

public class FindAllCollaboratorWithNameAndSurname
{
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
        doubleCollaborator1.Setup(c1 => c1.Equals(It.IsAny<ICollaborator>())).Returns(false);
        doubleCollaborator2.Setup(c2 => c2.Equals(It.IsAny<ICollaborator>())).Returns(false);
        doubleCollaborator3.Setup(c3 => c3.Equals(It.IsAny<ICollaborator>())).Returns(false);
        doubleCollaborator4.Setup(c4 => c4.Equals(It.IsAny<ICollaborator>())).Returns(false);

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
        doubleCollaborator1.Setup(c1 => c1.Equals(It.IsAny<ICollaborator>())).Returns(false);
        doubleCollaborator2.Setup(c2 => c2.Equals(It.IsAny<ICollaborator>())).Returns(false);
        doubleCollaborator3.Setup(c3 => c3.Equals(It.IsAny<ICollaborator>())).Returns(false);

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
