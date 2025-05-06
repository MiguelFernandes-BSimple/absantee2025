using AutoMapper;
using Domain.Interfaces;
using Infrastructure.DataModel;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.TrainingSubjectRepositoryTests;

public class TrainingSubjectRepositoryGetByIdAsyncTests : RepositoryTestBase
{
    [Fact]
    public async Task WhenPassingValidId_ThenReturnTrainingSubject()
    {
        //Assert
        var trainingSubject1 = new Mock<ITrainingSubject>();
        var guid1 = Guid.NewGuid();
        trainingSubject1.Setup(t => t.Id).Returns(guid1);
        trainingSubject1.Setup(t => t.Subject).Returns("Subject1");
        trainingSubject1.Setup(t => t.Description).Returns("Description1");
        var trainingSubject1DM = new TrainingSubjectDataModel(trainingSubject1.Object);
        context.TrainingSubjects.Add(trainingSubject1DM);

        var trainingSubject2 = new Mock<ITrainingSubject>();
        var guid2 = Guid.NewGuid();
        trainingSubject2.Setup(t => t.Id).Returns(guid2);
        trainingSubject2.Setup(t => t.Subject).Returns("Subject2");
        trainingSubject2.Setup(t => t.Description).Returns("Description2");
        var trainingSubject2DM = new TrainingSubjectDataModel(trainingSubject2.Object);
        context.TrainingSubjects.Add(trainingSubject2DM);

        await context.SaveChangesAsync();

        ITrainingSubject expected = trainingSubject2.Object;

        var trainingSubjectRepository = new TrainingSubjectRepositoryEF(context, _mapper.Object);

        //Act
        var result = await trainingSubjectRepository.GetByIdAsync(guid2);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task WhenPassingInvalidId_ThenReturnNull()
    {
        //Assert
        var trainingSubject1 = new Mock<ITrainingSubject>();
        var guid1 = Guid.NewGuid();
        trainingSubject1.Setup(t => t.Id).Returns(guid1);
        trainingSubject1.Setup(t => t.Subject).Returns("Subject1");
        trainingSubject1.Setup(t => t.Description).Returns("Description1");
        var trainingSubject1DM = new TrainingSubjectDataModel(trainingSubject1.Object);
        context.TrainingSubjects.Add(trainingSubject1DM);

        var trainingSubject2 = new Mock<ITrainingSubject>();
        var guid2 = Guid.NewGuid();
        trainingSubject2.Setup(t => t.Id).Returns(guid2);
        trainingSubject2.Setup(t => t.Subject).Returns("Subject2");
        trainingSubject2.Setup(t => t.Description).Returns("Description2");
        var trainingSubject2DM = new TrainingSubjectDataModel(trainingSubject2.Object);
        context.TrainingSubjects.Add(trainingSubject2DM);

        await context.SaveChangesAsync();

        var tsToSearchId = Guid.Empty;

        var trainingSubjectRepository = new TrainingSubjectRepositoryEF(context, _mapper.Object);

        //Act
        var result = await trainingSubjectRepository.GetByIdAsync(tsToSearchId);

        //Assert
        Assert.Null(result);
    }
}