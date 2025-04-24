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

namespace Infrastructure.Tests.TrainingModuleCollaboratorsRepositoryTests
{
    public class GetByTrainingModuleIdsTests
    {
        private readonly IMapper _mapper;

        public GetByTrainingModuleIdsTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Add both profiles for testing both mappings
                cfg.AddProfile<DataModelMappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

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


            var trainingModuleRepo = new TrainingModuleCollaboratorsRepository(context, _mapper);

            //Act
            var result = await trainingModuleRepo.GetByTrainingModuleIds([2]);

            //Assert
            Assert.Equal(
                expected.Select(e => e.TrainingModuleId),
                result.Select(r => r.TrainingModuleId)
            );
        }
    }
}
