using Application.Services;
using Domain.Factory;
using Domain.IRepository;
using Moq;

namespace Application.Tests.TrainingSubjectServiceTests;

public class TrainingSubjectServiceAddTests
{
    [Fact]
    public async Task WhenPassingValidParameters_ThenObjectIsAdded()
    {
        // Arrange        
        Mock<ITrainingSubjectRepository> tsRepo = new Mock<ITrainingSubjectRepository>();
        Mock<ITrainingSubjectFactory> tsFactory = new Mock<ITrainingSubjectFactory>();

        string title = "Title";
        string description = "description";

        var service =
            new TrainingSubjectService(tsRepo.Object, tsFactory.Object);

        // Act
        await service.Add(title, description);

        // Assert
    }

}