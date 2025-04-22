using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Moq;

namespace Application.Tests.TrainingModuleServiceTests;

public class TrainingModuleServiceAddTests
{
    [Fact]
    public async Task WhenPassingValidParameters_ThenObjectIsAdded()
    {
        // Arrange        
        Mock<ITrainingSubjectRepository> tsRepo = new Mock<ITrainingSubjectRepository>();
        Mock<ITrainingModuleRepository> tmRepo = new Mock<ITrainingModuleRepository>();
        Mock<ITrainingModuleFactory> tmFactory = new Mock<ITrainingModuleFactory>();

        long trainingSubjectId = 1;
        List<PeriodDateTime> periods = new List<PeriodDateTime>();

        Mock<ITrainingSubject> ts = new Mock<ITrainingSubject>();
        tsRepo.Setup(tsr => tsr.GetByIdAsync(trainingSubjectId)).ReturnsAsync(ts.Object);

        var service =
            new TrainingModuleService(tmRepo.Object, tsRepo.Object, tmFactory.Object);

        // Act
        await service.Add(trainingSubjectId, periods);

        // Assert
    }

}