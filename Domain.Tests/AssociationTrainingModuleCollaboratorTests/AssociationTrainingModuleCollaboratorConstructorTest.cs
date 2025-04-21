using Domain.Models;
using Moq;

namespace Domain.Tests.AssociationTrainingModuleCollaboratorTests;

public class AssociationTrainingModuleCollaboratorConstructorTest
{
    [Fact]
    public void WhenPassingParameters_ThenInstatiateObject()
    {
        // Arrange

        // Act
        var result = new AssociationTrainingModuleCollaborator(It.IsAny<long>(), It.IsAny<long>());

        // Assert
        Assert.NotNull(result);
    }
}