namespace Infrastructure.Tests.CollaboratorRepositoryTests;

using Moq;
using Domain.Interfaces;
using Infrastructure.Repositories;

public class FindAllCollaborators
{
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
}
