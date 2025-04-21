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

namespace Infrastructure.Tests.TrainingModuleCollaboratorsRepositoryTests
{
    public class GetByTrainingModuleIdsTests
    {
        [Fact]
        public async Task WhenSearchingByTrainingModuleIds_ThenReturnsExpectedResult()
        {
            //Assert
            var options = new DbContextOptionsBuilder<AbsanteeContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
               .Options;

            using var context = new AbsanteeContext(options);

            var trainingModuleCollab1 = new Mock<ITrainingModuleCollaborators>();
            trainingModuleCollab1.Setup(t => t.TrainingModuleId).Returns(1);
            var traingModuleCollab1DM = new TrainingModuleCollaboratorDataModel(trainingModuleCollab1.Object);
            context.TrainingModuleCollaboratorDataModels.Add(traingModuleCollab1DM);
            
            var trainingModuleCollab2 = new Mock<ITrainingModuleCollaborators>();
            trainingModuleCollab2.Setup(t => t.TrainingModuleId).Returns(2);
            var traingModuleCollab2DM = new TrainingModuleCollaboratorDataModel(trainingModuleCollab2.Object);
            context.TrainingModuleCollaboratorDataModels.Add(traingModuleCollab2DM);

            await context.SaveChangesAsync();

            var filteredDMs = new List<TrainingModuleCollaboratorDataModel>() { traingModuleCollab2DM };
            var expected = new List<ITrainingModuleCollaborators>() { trainingModuleCollab2.Object };

            var mapper = new Mock<IMapper<ITrainingModuleCollaborators, ITrainingModuleCollaboratorsVisitor>>();
            mapper.Setup(m => m.ToDomain(filteredDMs)).Returns(expected);

            var trainingModuleRepo = new TrainingModuleCollaboratorsRepository(context, mapper.Object);

            //Act
            var result = await trainingModuleRepo.GetByTrainingModuleIds([2]);

            //Assert
            Assert.True(expected.SequenceEqual(result));
        }
    }
}
