using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using AutoMapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.TrainingModuleRepositoryTests
{
    public class GetBySubjectIdAndFinishedTests
    {
        private readonly IMapper _mapper;

        public GetBySubjectIdAndFinishedTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Add both profiles for testing both mappings
                cfg.AddProfile<DataModelMappingProfile>();
            });

            _mapper = config.CreateMapper();
        }
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
            var period1 = new PeriodDateTime(DateTime.Today.AddDays(-1), DateTime.Today);
            var period2 = new PeriodDateTime(DateTime.Today.AddDays(-3), DateTime.Today.AddDays(-2));
            var periods1 = new List<PeriodDateTime>() { period1, period2 };
            trainingModule1.Setup(t => t.Periods).Returns(periods1);
            var trainingModuleDM1 = new TrainingModuleDataModel(trainingModule1.Object);
            context.TrainingModules.Add(trainingModuleDM1);

            
            var trainingModule2 = new Mock<ITrainingModule>();
            trainingModule2.Setup(t => t.TrainingSubjectId).Returns(1);
            var period3 = new PeriodDateTime(DateTime.Today.AddDays(2), DateTime.Today.AddDays(4));
            var periods2 = new List<PeriodDateTime>() { period3 };
            trainingModule2.Setup(t => t.Periods).Returns(periods2);
            var trainingModuleDM2 = new TrainingModuleDataModel(trainingModule2.Object);
            context.TrainingModules.Add(trainingModuleDM2);

            await context.SaveChangesAsync();

            var filteredDMs = new List<TrainingModuleDataModel>() { trainingModuleDM2 };
            var expected = new List<ITrainingModule>() { trainingModule2.Object };

            var trainingModuleRepo = new TrainingModuleRepository(context, _mapper);

            //Act
            var result = await trainingModuleRepo.GetBySubjectIdAndFinished(1, DateTime.Today);

            //Assert
            Assert.True(expected.SequenceEqual(result));
        }
    }
}
