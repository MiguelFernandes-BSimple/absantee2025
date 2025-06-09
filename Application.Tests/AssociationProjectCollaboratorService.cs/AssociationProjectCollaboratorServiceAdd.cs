using Application.Services;
using Domain.Models;
using Domain.Factory;
using Domain.Interfaces;
using Moq;
using Domain.IRepository;
using AutoMapper;

namespace Application.Tests.AssociationProjectServiceTests;

public class AssociationProjectCollaboratorServiceAdd
{
    [Fact]
    public async Task WhenAddingValidAssociation_ThenItsAddedSuccessfully()
    {
        // Arrange
        var mockFactory = new Mock<IAssociationProjectCollaboratorFactory>();
        var mockRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        var mockCollabRepository = new Mock<ICollaboratorRepository>();
        var mockUserRepository = new Mock<IUserRepository>();
        var mockProjectRepository = new Mock<IProjectRepository>();
        var mockMapper = new Mock<IMapper>();


        Guid collabId = new Guid();
        Guid projectId = new Guid();

        mockFactory
            .Setup(f => f.Create(It.IsAny<PeriodDate>(), collabId, projectId))
            .ReturnsAsync(It.IsAny<AssociationProjectCollaborator>());

        var service = new AssociationProjectCollaboratorService(mockRepository.Object, mockFactory.Object, mockCollabRepository.Object,
            mockUserRepository.Object, mockProjectRepository.Object, mockMapper.Object);

        // Act
        await Task.Run(() => service.Add(It.IsAny<PeriodDate>(), collabId, projectId));

        // Assert
        mockRepository.Verify(r => r.AddAsync(It.IsAny<AssociationProjectCollaborator>()), Times.Once);
    }

}
