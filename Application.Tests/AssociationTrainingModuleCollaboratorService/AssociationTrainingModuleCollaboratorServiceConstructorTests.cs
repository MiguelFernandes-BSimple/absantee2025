using Application.Services;
using Domain.Factory;
using Domain.IRepository;
using Moq;

namespace Application.Tests.AssociationTrainingModuleServiceTests;

public class AssociationTrainingModuleCollaboratorServiceTests
{
    [Fact]
    public void WhenPassingValidParameters_ThenInstatiateObject()
    {
        // Arrange

        // Act
        var result = new AssociationTrainingModuleCollaboratorService(
                It.IsAny<IAssociationTrainingModuleCollaboratorRepository>(),
                It.IsAny<ITrainingModuleRepository>(),
                It.IsAny<ICollaboratorRepository>(),
                It.IsAny<IAssociationTrainingModuleCollaboratorFactory>());

        // Assert
        Assert.NotNull(result);
    }
}