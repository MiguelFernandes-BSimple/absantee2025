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
    public class GetByTrainingModuleIdsTests : RepositoryTestBase
    {

        [Fact]
        public async Task WhenSearchingByTrainingModuleIds_ThenReturnsExpectedResult()
        {
            //Assert
            var trainingModuleCollab1 = new Mock<IAssociationTrainingModuleCollaborator>();
            var trainingModuleGuid1 = Guid.NewGuid();
            trainingModuleCollab1.Setup(t => t.TrainingModuleId).Returns(trainingModuleGuid1);
            var collabGuid1 = Guid.NewGuid();
            trainingModuleCollab1.Setup(t => t.CollaboratorId).Returns(collabGuid1);
            var traingModuleCollab1DM = new AssociationTrainingModuleCollaboratorDataModel(trainingModuleCollab1.Object);
            context.AssociationTrainingModuleCollaborators.Add(traingModuleCollab1DM);

            var trainingModuleCollab2 = new Mock<IAssociationTrainingModuleCollaborator>();
            var trainingModuleGuid2 = Guid.NewGuid();
            trainingModuleCollab2.Setup(t => t.TrainingModuleId).Returns(trainingModuleGuid2);
            var collabGuid2 = Guid.NewGuid();
            trainingModuleCollab2.Setup(t => t.CollaboratorId).Returns(collabGuid2);
            var traingModuleCollab2DM = new AssociationTrainingModuleCollaboratorDataModel(trainingModuleCollab2.Object);
            context.AssociationTrainingModuleCollaborators.Add(traingModuleCollab2DM);

            var trainingModuleCollab3 = new Mock<IAssociationTrainingModuleCollaborator>();
            var trainingModuleGuid3 = Guid.NewGuid();
            trainingModuleCollab3.Setup(t => t.TrainingModuleId).Returns(trainingModuleGuid3);
            var collabGuid3 = Guid.NewGuid();
            trainingModuleCollab3.Setup(t => t.CollaboratorId).Returns(collabGuid3);
            var traingModuleCollab3DM = new AssociationTrainingModuleCollaboratorDataModel(trainingModuleCollab3.Object);
            context.AssociationTrainingModuleCollaborators.Add(traingModuleCollab3DM);

            await context.SaveChangesAsync();

            _mapper.Setup(m => m.Map<AssociationTrainingModuleCollaboratorDataModel, AssociationTrainingModuleCollaborator>(
            It.Is<AssociationTrainingModuleCollaboratorDataModel>(t =>
                t.Id == traingModuleCollab1DM.Id
                )))
                .Returns(new AssociationTrainingModuleCollaborator(traingModuleCollab1DM.TrainingModuleId, traingModuleCollab1DM.CollaboratorId));

            _mapper.Setup(m => m.Map<AssociationTrainingModuleCollaboratorDataModel, AssociationTrainingModuleCollaborator>(
            It.Is<AssociationTrainingModuleCollaboratorDataModel>(t =>
                t.Id == traingModuleCollab2DM.Id
                )))
                .Returns(new AssociationTrainingModuleCollaborator(traingModuleCollab2DM.TrainingModuleId, traingModuleCollab2DM.CollaboratorId));

            _mapper.Setup(m => m.Map<AssociationTrainingModuleCollaboratorDataModel, AssociationTrainingModuleCollaborator>(
            It.Is<AssociationTrainingModuleCollaboratorDataModel>(t =>
                t.Id == traingModuleCollab3DM.Id
                )))
                .Returns(new AssociationTrainingModuleCollaborator(traingModuleCollab3DM.TrainingModuleId, traingModuleCollab3DM.CollaboratorId));

            var trainingModuleRepo = new AssociationTrainingModuleCollaboratorRepositoryEF(context, _mapper.Object);

            //Act
            var result = await trainingModuleRepo.GetByTrainingModuleIds([trainingModuleGuid1, trainingModuleGuid3]);

            //Assert
            Assert.Equal(2, result.Count());

            Assert.Contains(result, c => c.TrainingModuleId == trainingModuleGuid1);
            Assert.Contains(result, c => c.TrainingModuleId == trainingModuleGuid3);

        }
    }
}
