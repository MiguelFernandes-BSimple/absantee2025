using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Repositories;
using Moq;

namespace Infrastructure.Tests.TrainingModuleRepositoryTests;

public class GetBySubjectIdAndFinishedTests : RepositoryTestBase
{
    [Fact]
    public async Task WhenSearchingBySubjectIdAndFinishTrainingModule_ThenReturnsExpectedResult()
    {
        //Assert
        var trainingModule1 = new Mock<ITrainingModule>();
        var guid1 = Guid.NewGuid();
        trainingModule1.Setup(t => t.TrainingSubjectId).Returns(guid1);
        var period1 = new PeriodDateTime(DateTime.Today.AddDays(-1), DateTime.Today);
        var period2 = new PeriodDateTime(DateTime.Today.AddDays(-3), DateTime.Today.AddDays(-2));
        var periods1 = new List<PeriodDateTime>() { period1, period2 };
        trainingModule1.Setup(t => t.Periods).Returns(periods1);
        var trainingModuleDM1 = new TrainingModuleDataModel(trainingModule1.Object);
        context.TrainingModules.Add(trainingModuleDM1);

        var trainingModule2 = new Mock<ITrainingModule>();
        trainingModule2.Setup(t => t.TrainingSubjectId).Returns(guid1);
        var period3 = new PeriodDateTime(DateTime.Today.AddDays(2), DateTime.Today.AddDays(4));
        var periods2 = new List<PeriodDateTime>() { period3 };
        trainingModule2.Setup(t => t.Periods).Returns(periods2);
        var trainingModuleDM2 = new TrainingModuleDataModel(trainingModule2.Object);
        context.TrainingModules.Add(trainingModuleDM2);

        await context.SaveChangesAsync();

        var filteredDMs = new List<TrainingModuleDataModel>() { trainingModuleDM2 };
        var expected = new List<ITrainingModule>() { trainingModule2.Object };

        _mapper.Setup(m => m.Map<TrainingModuleDataModel, TrainingModule>(
            It.Is<TrainingModuleDataModel>(t =>
                t.Id == trainingModuleDM1.Id
            )))
            .Returns(new TrainingModule(trainingModuleDM1.Id, trainingModuleDM1.TrainingSubjectId, trainingModuleDM1.Periods));
         
        var trainingModuleRepo = new TrainingModuleRepositoryEF(context, _mapper.Object);

        //Act
        var result = (await trainingModuleRepo.GetBySubjectIdAndFinished(guid1, DateTime.Today)).ToList();

        //Assert
        Assert.Single(result);
        Assert.Equal(trainingModuleDM1.Id, result.First().Id);
    }
}
