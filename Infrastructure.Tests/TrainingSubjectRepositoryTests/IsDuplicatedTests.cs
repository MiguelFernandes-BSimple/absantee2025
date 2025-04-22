using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.TrainingSubjectRepositoryTests
{
    public class IsDuplicatedTests
    {
        [Fact]
        public async Task WhenSearchingByTrainingModuleIds_ThenReturnsExpectedResult()
        {
            //Assert
            var options = new DbContextOptionsBuilder<AbsanteeContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
               .Options;

            using var context = new AbsanteeContext(options);

            var trainingSubject1 = new Mock<ITrainingSubject>();
            trainingSubject1.Setup(t => t.Subject).Returns("Subject1");
            trainingSubject1.Setup(t => t.Description).Returns("Description1");
            var trainingSubject1DM = new TrainingSubjectDataModel(trainingSubject1.Object);
            context.TrainingSubjects.Add(trainingSubject1DM);

            var trainingSubject2 = new Mock<ITrainingSubject>();
            trainingSubject2.Setup(t => t.Subject).Returns("Subject2");
            trainingSubject2.Setup(t => t.Description).Returns("Description2");
            var trainingSubject2DM = new TrainingSubjectDataModel(trainingSubject2.Object);
            context.TrainingSubjects.Add(trainingSubject2DM);

            await context.SaveChangesAsync();

            var filteredDMs = new List<TrainingSubjectDataModel>() { trainingSubject2DM };
            var expected = new List<ITrainingSubject>() { trainingSubject2.Object };

            var mapper = new Mock<IMapper<ITrainingSubject, ITrainingSubjectVisitor>>();
            mapper.Setup(m => m.ToDomain(filteredDMs)).Returns(expected);

            var trainingModuleRepo = new TrainingSubjectRepository(context, mapper.Object);

            //Act
            var result = await trainingModuleRepo.IsDuplicated("Subject2");

            //Assert
            Assert.True(result);
        }
    }
}
