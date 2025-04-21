using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Moq;

namespace Domain.Tests.TrainingModuleCollaboratorsTests
{
    public class TrainingModuleCollaboratorsFactoryTests
    {
        [Fact]
        public void WhenPassingValidIds_ThenTrainingModuleCollaboratorIsCreated()
        {
            //Arrange
            var collabId = 1;
            var collab = new Mock<ICollaborator>();

            var trainingModuleId = 1;
            var trainingSubject = new Mock<ITrainingModule>();

            var collaboratorRepo = new Mock<ICollaboratorRepository>();
            collaboratorRepo.Setup(c => c.GetByIdAsync(collabId)).ReturnsAsync(collab.Object);

            var trainingSubjectRepo = new Mock<ITrainingModuleRepository>();
            trainingSubjectRepo.Setup(t => t.GetByIdAsync(trainingModuleId)).ReturnsAsync(trainingSubject.Object);

            var factory = new TrainingModuleCollaboratorsFactory(collaboratorRepo.Object, trainingSubjectRepo.Object);

            //Act
            var result = factory.Create(trainingModuleId, collabId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task WhenCollaboratorDontExists_ThenThrowException()
        {
            //Arrange
            var collabId = 1;

            var trainingModuleId = 1;
            var trainingSubject = new Mock<ITrainingModule>();

            var collaboratorRepo = new Mock<ICollaboratorRepository>();
            collaboratorRepo.Setup(c => c.GetByIdAsync(collabId)).ReturnsAsync((ICollaborator?)null);

            var trainingSubjectRepo = new Mock<ITrainingModuleRepository>();
            trainingSubjectRepo.Setup(t => t.GetByIdAsync(trainingModuleId)).ReturnsAsync(trainingSubject.Object);

            var factory = new TrainingModuleCollaboratorsFactory(collaboratorRepo.Object, trainingSubjectRepo.Object);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                //Act
                factory.Create(trainingModuleId, collabId)
            ); 

            Assert.Equal("Collaborator must exists", exception.Message);
        }

        [Fact]
        public async Task WhenTrainingModuleDontExists_ThenThrowException()
        {
            //Arrange
            var collabId = 1;
            var collab = new Mock<ICollaborator>();

            var trainingModuleId = 1;

            var collaboratorRepo = new Mock<ICollaboratorRepository>();
            collaboratorRepo.Setup(c => c.GetByIdAsync(collabId)).ReturnsAsync(collab.Object);

            var trainingSubjectRepo = new Mock<ITrainingModuleRepository>();
            trainingSubjectRepo.Setup(t => t.GetByIdAsync(trainingModuleId)).ReturnsAsync((ITrainingModule?)null);

            var factory = new TrainingModuleCollaboratorsFactory(collaboratorRepo.Object, trainingSubjectRepo.Object);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                //Act
                factory.Create(trainingModuleId, collabId)
            );

            Assert.Equal("Training Module must exists", exception.Message);
        }
    }
}
