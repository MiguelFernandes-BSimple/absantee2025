using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Moq;

namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryTests
{
    public class Add
    {
        [Fact]
        public void WhenAddingDiferentAssociation_ThenReturnTrue()
        {
            //arrange
            Mock<IAssociationProjectCollaborator> assocMock = new Mock<IAssociationProjectCollaborator>();
            List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator>
            {
                assocMock.Object,
            };

            Mock<IAssociationProjectCollaborator> newAssocMock = new Mock<IAssociationProjectCollaborator>();

            assocMock.Setup(a => a.Equals(newAssocMock.Object)).Returns(false);

            var assocRepo = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);
            
            //act
            var result = assocRepo.Add(newAssocMock.Object);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void WhenAddingEqualAssociation_ThenReturnFalse()
        {
            //arrange
            Mock<IAssociationProjectCollaborator> assocMock = new Mock<IAssociationProjectCollaborator>();
            List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator>
            {
                assocMock.Object,
            };

            Mock<IAssociationProjectCollaborator> newAssocMock = new Mock<IAssociationProjectCollaborator>();

            assocMock.Setup(a => a.Equals(newAssocMock.Object)).Returns(true);

            var assocRepo = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

            //act
            var result = assocRepo.Add(newAssocMock.Object);

            //assert
            Assert.False(result);
        }
    }
}
