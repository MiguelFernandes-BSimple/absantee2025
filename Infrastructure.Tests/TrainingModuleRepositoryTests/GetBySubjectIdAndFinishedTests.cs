using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.TrainingModuleRepositoryTests
{
    public class GetBySubjectIdAndFinishedTests
    {
        [Fact]
        public async Task WhenSearchingBySubjectIdAndFinishTrainingModule_ThenReturnsExpectedResult()
        {
            //Assert
            var options = new DbContextOptionsBuilder<AbsanteeContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
               .Options;

            using var context = new AbsanteeContext(options);

            
            var trainingModule1 = new Mock<ITrainingModule>();
            trainingModule1.Setup(t => t.TrainingSubjectId).Returns(1);
            var period1 = new PeriodDateTime(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            var period2 = new PeriodDateTime(DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-2));
            var periods1 = new List<PeriodDateTime>() { period1, period2 };
            trainingModule1.Setup(t => t.Periods).Returns(periods1);
            var trainingModuleDM1 = new TrainingModuleDataModel(trainingModule1.Object);
            context.TrainingModules.Add(trainingModuleDM1);

            
            var trainingModule2 = new Mock<ITrainingModule>();
            trainingModule2.Setup(t => t.TrainingSubjectId).Returns(1);
            var period3 = new PeriodDateTime(DateTime.Now.AddDays(2), DateTime.Now.AddDays(4));
            var periods2 = new List<PeriodDateTime>() { period3 };
            trainingModule2.Setup(t => t.Periods).Returns(periods2);
            var trainingModuleDM2 = new TrainingModuleDataModel(trainingModule2.Object);
            context.TrainingModules.Add(trainingModuleDM2);

            await context.SaveChangesAsync();

            var filteredDMs = new List<TrainingModuleDataModel>() { trainingModuleDM2 };
            var expected = new List<ITrainingModule>() { trainingModule2.Object };

            var mapper = new Mock<IMapper<ITrainingModule, ITrainingModuleVisitor>>();
            mapper.Setup(m => m.ToDomain(filteredDMs)).Returns(expected);
            
            var trainingModuleRepo = new TrainingModuleRepository(context, mapper.Object);

            //Act
            var result = await trainingModuleRepo.GetBySubjectIdAndFinished(1, DateTime.Now);

            //Assert
            Assert.True(expected.SequenceEqual(result));
        }
    }
}
