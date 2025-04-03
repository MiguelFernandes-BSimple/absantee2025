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

        // To be correct, all collaborators must be distinct - Distinct
        // Email is different from the others
        doubleCollaborator1.Setup(c1 => c1.Equals(doubleCollaborator2.Object)).Returns(false);
        doubleCollaborator2.Setup(c2 => c2.Equals(doubleCollaborator1.Object)).Returns(false);

        //Create a collaborator list
        List<ICollaborator> expectedList = new List<ICollaborator> {
            doubleCollaborator1.Object,
            doubleCollaborator2.Object };

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
