using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Moq;

namespace Domain.Tests.TrainingModuleCollaboratorsTests
{
    public class TrainingModuleCollaboratorsConstructorTests
    {
        [Fact]
        public void WhenPassingValidData_ThenTrainingModuleCollaboratorIsCreated()
        {
            //Arrange

            //Act
            new AssociationTrainingModuleCollaborator(It.IsAny<Guid>(), It.IsAny<Guid>());

            //Assert
        }
    }
}
