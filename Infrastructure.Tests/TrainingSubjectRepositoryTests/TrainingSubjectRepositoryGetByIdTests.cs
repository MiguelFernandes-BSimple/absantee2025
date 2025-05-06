using AutoMapper;
using Domain.Interfaces;
using Infrastructure.DataModel;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.TrainingSubjectRepositoryTests;

public class TrainingSubjectRepositoryGetByIdTests
{
    //[Fact]
    //public async Task WhenPassingValidId_ThenReturnTrainingSubject()
    //{
    //    //Assert
    //    var options = new DbContextOptionsBuilder<AbsanteeContext>()
    //       .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
    //       .Options;

    //    using var context = new AbsanteeContext(options);

    //    var trainingSubject1 = new Mock<ITrainingSubject>();
    //    trainingSubject1.Setup(t => t.Id).Returns(1);
    //    trainingSubject1.Setup(t => t.Subject).Returns("Subject1");
    //    trainingSubject1.Setup(t => t.Description).Returns("Description1");
    //    var trainingSubject1DM = new TrainingSubjectDataModel(trainingSubject1.Object);
    //    context.TrainingSubjects.Add(trainingSubject1DM);

    //    var trainingSubject2 = new Mock<ITrainingSubject>();
    //    trainingSubject2.Setup(t => t.Id).Returns(2);
    //    trainingSubject2.Setup(t => t.Subject).Returns("Subject2");
    //    trainingSubject2.Setup(t => t.Description).Returns("Description2");
    //    var trainingSubject2DM = new TrainingSubjectDataModel(trainingSubject2.Object);
    //    context.TrainingSubjects.Add(trainingSubject2DM);

    //    await context.SaveChangesAsync();

    //    var tsToSearchId = 2;
    //    ITrainingSubject expected = trainingSubject2.Object;

    //    var mapper = new Mock<IMapper>();

    //    var trainingSubjectRepository = new TrainingSubjectRepositoryEF(context, mapper.Object);

    //    //Act
    //    var result = trainingSubjectRepository.GetById(tsToSearchId);

    //    //Assert
    //    Assert.Equal(expected, result);
    //}

    //[Fact]
    //public async Task WhenPassingInvalidId_ThenReturnNull()
    //{
    //    //Assert
    //    var options = new DbContextOptionsBuilder<AbsanteeContext>()
    //       .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
    //       .Options;

    //    using var context = new AbsanteeContext(options);

    //    var trainingSubject1 = new Mock<ITrainingSubject>();
    //    trainingSubject1.Setup(t => t.Id).Returns(1);
    //    trainingSubject1.Setup(t => t.Subject).Returns("Subject1");
    //    trainingSubject1.Setup(t => t.Description).Returns("Description1");
    //    var trainingSubject1DM = new TrainingSubjectDataModel(trainingSubject1.Object);
    //    context.TrainingSubjects.Add(trainingSubject1DM);

    //    var trainingSubject2 = new Mock<ITrainingSubject>();
    //    trainingSubject2.Setup(t => t.Id).Returns(2);
    //    trainingSubject2.Setup(t => t.Subject).Returns("Subject2");
    //    trainingSubject2.Setup(t => t.Description).Returns("Description2");
    //    var trainingSubject2DM = new TrainingSubjectDataModel(trainingSubject2.Object);
    //    context.TrainingSubjects.Add(trainingSubject2DM);

    //    await context.SaveChangesAsync();

    //    var tsToSearchId = 3;

    //    var mapper = new Mock<IMapper>();

    //    var trainingSubjectRepository = new TrainingSubjectRepositoryEF(context, mapper.Object);

    //    //Act
    //    var result = trainingSubjectRepository.GetById(tsToSearchId);

    //    //Assert
    //    Assert.Null(result);
    //}
}