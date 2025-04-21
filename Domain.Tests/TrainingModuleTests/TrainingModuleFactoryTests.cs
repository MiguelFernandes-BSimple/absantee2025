using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.TrainingModuleTests;

public class TrainingModuleFactoryTests
{
    [Fact]
    public async Task WhenPassingValidArguments_ThenInstatiateObject()
    {
        // Arrange
        Mock<ITrainingModuleRepository> tmRepo = new Mock<ITrainingModuleRepository>();
        Mock<ITrainingSubjectRepository> tsRepo = new Mock<ITrainingSubjectRepository>();

        long trainingSubjectId = 1;
        PeriodDateTime period1 = new PeriodDateTime(DateTime.Now.AddDays(10), DateTime.Now.AddMonths(10));
        PeriodDateTime period2 = new PeriodDateTime(DateTime.Now.AddYears(1), DateTime.Now.AddYears(2));

        Mock<ITrainingSubject> tSubject = new Mock<ITrainingSubject>();

        tsRepo.Setup(tsr => tsr.GetByIdAsync(trainingSubjectId)).ReturnsAsync(tSubject.Object);

        Mock<ITrainingModule> tModule1 = new Mock<ITrainingModule>();
        Mock<ITrainingModule> tModule2 = new Mock<ITrainingModule>();
        PeriodDateTime tmPeriod1 = new PeriodDateTime(DateTime.Now.AddYears(3), DateTime.Now.AddYears(4));
        PeriodDateTime tmPeriod2 = new PeriodDateTime(DateTime.Now.AddYears(5), DateTime.Now.AddYears(6));

        IEnumerable<ITrainingModule> tmList = new List<ITrainingModule> { tModule1.Object, tModule2.Object };
        tmRepo.Setup(tmr => tmr.FindAllBySubject(trainingSubjectId)).ReturnsAsync(tmList);

        tModule1.Setup(t1 => t1.Periods).Returns(new List<PeriodDateTime> { tmPeriod1 });
        tModule2.Setup(t2 => t2.Periods).Returns(new List<PeriodDateTime> { tmPeriod2 });

        List<PeriodDateTime> periodList = new List<PeriodDateTime> { period1, period2 };

        TrainingModuleFactory factory = new TrainingModuleFactory(tmRepo.Object, tsRepo.Object);

        // Act
        var result = await factory.Create(trainingSubjectId, periodList);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task WhenPassingInvalidSubjectId_ThenThrowException()
    {
        // Arrange
        Mock<ITrainingModuleRepository> tmRepo = new Mock<ITrainingModuleRepository>();
        Mock<ITrainingSubjectRepository> tsRepo = new Mock<ITrainingSubjectRepository>();

        long trainingSubjectId = 1;
        PeriodDateTime period1 = new PeriodDateTime(DateTime.Now.AddDays(10), DateTime.Now.AddMonths(10));
        PeriodDateTime period2 = new PeriodDateTime(DateTime.Now.AddYears(1), DateTime.Now.AddYears(2));

        tsRepo.Setup(tsr => tsr.GetByIdAsync(trainingSubjectId)).ReturnsAsync((ITrainingSubject)null!);

        Mock<ITrainingModule> tModule1 = new Mock<ITrainingModule>();
        Mock<ITrainingModule> tModule2 = new Mock<ITrainingModule>();
        PeriodDateTime tmPeriod1 = new PeriodDateTime(DateTime.Now.AddYears(3), DateTime.Now.AddYears(4));
        PeriodDateTime tmPeriod2 = new PeriodDateTime(DateTime.Now.AddYears(5), DateTime.Now.AddYears(6));

        IEnumerable<ITrainingModule> tmList = new List<ITrainingModule> { tModule1.Object, tModule2.Object };
        tmRepo.Setup(tmr => tmr.FindAllBySubject(trainingSubjectId)).ReturnsAsync(tmList);

        tModule1.Setup(t1 => t1.Periods).Returns(new List<PeriodDateTime> { tmPeriod1 });
        tModule2.Setup(t2 => t2.Periods).Returns(new List<PeriodDateTime> { tmPeriod2 });

        List<PeriodDateTime> periodList = new List<PeriodDateTime> { period1, period2 };

        TrainingModuleFactory factory = new TrainingModuleFactory(tmRepo.Object, tsRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            factory.Create(trainingSubjectId, periodList)
            );

        Assert.Equal("Invalid inputs", exception.Message);
    }

    [Fact]
    public async Task WhenPassingIntersectingPeriods_ThenThrowException()
    {
        // Arrange
        Mock<ITrainingModuleRepository> tmRepo = new Mock<ITrainingModuleRepository>();
        Mock<ITrainingSubjectRepository> tsRepo = new Mock<ITrainingSubjectRepository>();

        long trainingSubjectId = 1;
        PeriodDateTime period1 = new PeriodDateTime(DateTime.Now.AddDays(10), DateTime.Now.AddMonths(10));
        PeriodDateTime period2 = new PeriodDateTime(DateTime.Now.AddYears(1), DateTime.Now.AddYears(2));

        tsRepo.Setup(tsr => tsr.GetByIdAsync(trainingSubjectId)).ReturnsAsync((ITrainingSubject)null!);

        Mock<ITrainingModule> tModule1 = new Mock<ITrainingModule>();
        Mock<ITrainingModule> tModule2 = new Mock<ITrainingModule>();
        PeriodDateTime tmPeriod1 = new PeriodDateTime(DateTime.Now.AddYears(1), DateTime.Now.AddYears(4));
        PeriodDateTime tmPeriod2 = new PeriodDateTime(DateTime.Now.AddYears(5), DateTime.Now.AddYears(6));

        IEnumerable<ITrainingModule> tmList = new List<ITrainingModule> { tModule1.Object, tModule2.Object };
        tmRepo.Setup(tmr => tmr.FindAllBySubject(trainingSubjectId)).ReturnsAsync(tmList);

        tModule1.Setup(t1 => t1.Periods).Returns(new List<PeriodDateTime> { tmPeriod1 });
        tModule2.Setup(t2 => t2.Periods).Returns(new List<PeriodDateTime> { tmPeriod2 });

        List<PeriodDateTime> periodList = new List<PeriodDateTime> { period1, period2 };

        TrainingModuleFactory factory = new TrainingModuleFactory(tmRepo.Object, tsRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            factory.Create(trainingSubjectId, periodList)
            );

        Assert.Equal("Invalid inputs", exception.Message);
    }

    [Fact]
    public void WhenPassingValidVisitor_ThenInstatiateObject()
    {
        // Arrange
        Mock<ITrainingModuleRepository> tmRepo = new Mock<ITrainingModuleRepository>();
        Mock<ITrainingSubjectRepository> tsRepo = new Mock<ITrainingSubjectRepository>();

        Mock<ITrainingModuleVisitor> visitor = new Mock<ITrainingModuleVisitor>();
        visitor.Setup(v => v.Id).Returns(It.IsAny<long>());
        visitor.Setup(v => v.Periods).Returns(new List<PeriodDateTime>());

        TrainingModuleFactory factory = new TrainingModuleFactory(tmRepo.Object, tsRepo.Object);

        // Act
        var result = factory.Create(visitor.Object);

        // Assert
        Assert.NotNull(result);
    }
}