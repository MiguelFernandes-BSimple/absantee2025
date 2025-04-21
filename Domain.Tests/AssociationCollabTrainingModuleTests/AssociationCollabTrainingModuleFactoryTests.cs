using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.AssociationCollabTrainingModuleTests
{
    public class AssociationCollabTrainingModuleFactoryTests
    {
        [Fact]
        public void WhenPassingValidData_ThenCreatesAssociation(){
            // arrange
            var trainingDouble = new Mock<ITrainingModule>();
            var collabDouble = new Mock<ICollaborator>();

            var trainingModuleRepoDouble = new Mock<ITrainingModuleRepository>();
            trainingModuleRepoDouble.Setup(tmrd => tmrd.GetById(It.IsAny<long>())).Returns(trainingDouble.Object);

            var collabRepoDouble = new Mock<ICollaboratorRepository>();
            collabRepoDouble.Setup(crd => crd.GetById(It.IsAny<long>())).Returns(collabDouble.Object);

            var associationRepositoryDouble = new Mock<IAssociationCollabTrainingModuleRepository>();
            associationRepositoryDouble.Setup(ard => ard.CheckIfCanAdd(1,2)).Returns(true);

            var associationCollabTrainingModuleFactory = new AssociationCollabTrainingModuleFactory(trainingModuleRepoDouble.Object, collabRepoDouble.Object, associationRepositoryDouble.Object);

            // act
            var result = associationCollabTrainingModuleFactory.Create(1,2);

            // assert
            Assert.Equal(1, result._collaboratorId);
            Assert.Equal(2, result._trainingModuleId);
        }

        [Fact]
        public void WhenTrainingModuleDoesNotExist_ThenThrowsException(){
            // arrange
            var collabDouble = new Mock<ICollaborator>();

            var trainingModuleRepoDouble = new Mock<ITrainingModuleRepository>();
            trainingModuleRepoDouble.Setup(tmrd => tmrd.GetById(It.IsAny<long>())).Returns((TrainingModule?)null);

            var collabRepoDouble = new Mock<ICollaboratorRepository>();
            collabRepoDouble.Setup(crd => crd.GetById(It.IsAny<long>())).Returns(collabDouble.Object);

            var associationRepositoryDouble = new Mock<IAssociationCollabTrainingModuleRepository>();
            associationRepositoryDouble.Setup(ard => ard.CheckIfCanAdd(1,2)).Returns(true);

            var associationCollabTrainingModuleFactory = new AssociationCollabTrainingModuleFactory(trainingModuleRepoDouble.Object, collabRepoDouble.Object, associationRepositoryDouble.Object);;

            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                //act
                associationCollabTrainingModuleFactory.Create(1,2)
            );

            Assert.Equal("Training Module does not exist", exception.Message);
        }

        [Fact]
        public void WhenCollaboratorDoesNotExist_ThenThrowsException(){
            // arrange
            var trainingDouble = new Mock<ITrainingModule>();
            
            var trainingModuleRepoDouble = new Mock<ITrainingModuleRepository>();
            trainingModuleRepoDouble.Setup(tmrd => tmrd.GetById(It.IsAny<long>())).Returns(trainingDouble.Object);

            var collabRepoDouble = new Mock<ICollaboratorRepository>();
            collabRepoDouble.Setup(crd => crd.GetById(It.IsAny<long>())).Returns((Collaborator?)null);

            var associationRepositoryDouble = new Mock<IAssociationCollabTrainingModuleRepository>();
            associationRepositoryDouble.Setup(ard => ard.CheckIfCanAdd(1,2)).Returns(true);

            var associationCollabTrainingModuleFactory = new AssociationCollabTrainingModuleFactory(trainingModuleRepoDouble.Object, collabRepoDouble.Object, associationRepositoryDouble.Object);;

            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                //act
                associationCollabTrainingModuleFactory.Create(1,2)
            );

            Assert.Equal("Collaborator does not exist", exception.Message);
        }

        [Fact]
        public void WhenCannotAdd_ThenThrowsException(){
            // arrange
            var trainingDouble = new Mock<ITrainingModule>();
            var collabDouble = new Mock<ICollaborator>();

            var trainingModuleRepoDouble = new Mock<ITrainingModuleRepository>();
            trainingModuleRepoDouble.Setup(tmrd => tmrd.GetById(It.IsAny<long>())).Returns(trainingDouble.Object);

            var collabRepoDouble = new Mock<ICollaboratorRepository>();
            collabRepoDouble.Setup(crd => crd.GetById(It.IsAny<long>())).Returns(collabDouble.Object);

            var associationRepositoryDouble = new Mock<IAssociationCollabTrainingModuleRepository>();
            associationRepositoryDouble.Setup(ard => ard.CheckIfCanAdd(1,2)).Returns(false);

            var associationCollabTrainingModuleFactory = new AssociationCollabTrainingModuleFactory(trainingModuleRepoDouble.Object, collabRepoDouble.Object, associationRepositoryDouble.Object);

            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                //act
                associationCollabTrainingModuleFactory.Create(1,2)
            );

            Assert.Equal("Invalid Arguments", exception.Message);
        }

        [Fact]
        public void WhenPassingValidVisitor_ThenCreatesAssociation(){
            // arrange
            var association = new Mock<IAssociationCollabTrainingModuleVisitor>();
            association.Setup(a => a._id).Returns(1);
            association.Setup(a => a._collaboratorId).Returns(2);
            association.Setup(a => a._trainingModuleId).Returns(3);

            var trainingModuleRepoDouble = new Mock<ITrainingModuleRepository>();
            var collabRepoDouble = new Mock<ICollaboratorRepository>();
            var associationRepositoryDouble = new Mock<IAssociationCollabTrainingModuleRepository>();

            var associationCollabTrainingModuleFactory = new AssociationCollabTrainingModuleFactory(trainingModuleRepoDouble.Object, collabRepoDouble.Object, associationRepositoryDouble.Object);

            // act
            var result = associationCollabTrainingModuleFactory.Create(association.Object);

            // assert
            Assert.Equal(1, result._id);
            Assert.Equal(2, result._collaboratorId);
            Assert.Equal(3, result._trainingModuleId);
        }
    }
}