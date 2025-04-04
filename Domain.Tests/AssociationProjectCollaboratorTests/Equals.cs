using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.AssociationProjectCollaboratorTests
{
    public class Equals
    {
        [Fact]
        public void WhenReceivesSameAssociation_ReturnTrue()
        {
            //arrange
            var projectDouble = new Mock<IProject>();
            var periodDateDouble = new Mock<IPeriodDate>();

            projectDouble.Setup(p => p.ContainsDates(periodDateDouble.Object)).Returns(true);
            projectDouble.Setup(c => c.IsFinished()).Returns(false);

            var collaboratorDouble = new Mock<ICollaborator>();
            collaboratorDouble.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

            var assocProjCollab1 = new AssociationProjectCollaborator(periodDateDouble.Object, collaboratorDouble.Object, projectDouble.Object);
            var assocProjCollab2 = new AssociationProjectCollaborator(periodDateDouble.Object, collaboratorDouble.Object, projectDouble.Object);

            collaboratorDouble.Setup(c => c.Equals(collaboratorDouble.Object)).Returns(true);
            projectDouble.Setup(c => c.Equals(projectDouble.Object)).Returns(true);
            periodDateDouble.Setup(c => c.Intersects(periodDateDouble.Object)).Returns(true);

            //act
            bool result = assocProjCollab1.Equals(assocProjCollab2);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void WhenReceivesDifferentCollaborator_ReturnFalse()
        {
            //arrange
            var projectDouble = new Mock<IProject>();
            var periodDateDouble = new Mock<IPeriodDate>();

            projectDouble.Setup(p => p.ContainsDates(periodDateDouble.Object)).Returns(true);
            projectDouble.Setup(c => c.IsFinished()).Returns(false);

            var collaboratorDouble = new Mock<ICollaborator>();
            collaboratorDouble.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

            var assocProjCollab1 = new AssociationProjectCollaborator(periodDateDouble.Object, collaboratorDouble.Object, projectDouble.Object);
            var assocProjCollab2 = new AssociationProjectCollaborator(periodDateDouble.Object, collaboratorDouble.Object, projectDouble.Object);

            collaboratorDouble.Setup(c => c.Equals(collaboratorDouble.Object)).Returns(false);
            projectDouble.Setup(c => c.Equals(projectDouble.Object)).Returns(true);
            periodDateDouble.Setup(c => c.Intersects(periodDateDouble.Object)).Returns(true);

            //act
            bool result = assocProjCollab1.Equals(assocProjCollab2);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void WhenReceivesDifferentProject_ReturnFalse()
        {
            //arrange
            var projectDouble = new Mock<IProject>();
            var periodDateDouble = new Mock<IPeriodDate>();

            projectDouble.Setup(p => p.ContainsDates(periodDateDouble.Object)).Returns(true);
            projectDouble.Setup(c => c.IsFinished()).Returns(false);

            var collaboratorDouble = new Mock<ICollaborator>();
            collaboratorDouble.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

            var assocProjCollab1 = new AssociationProjectCollaborator(periodDateDouble.Object, collaboratorDouble.Object, projectDouble.Object);
            var assocProjCollab2 = new AssociationProjectCollaborator(periodDateDouble.Object, collaboratorDouble.Object, projectDouble.Object);

            collaboratorDouble.Setup(c => c.Equals(collaboratorDouble.Object)).Returns(true);
            projectDouble.Setup(c => c.Equals(projectDouble.Object)).Returns(false);
            periodDateDouble.Setup(c => c.Intersects(periodDateDouble.Object)).Returns(true);

            //act
            bool result = assocProjCollab1.Equals(assocProjCollab2);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void WhenReceivesNotIntersectingPeriods_ReturnFalse()
        {
            //arrange
            var projectDouble = new Mock<IProject>();
            var periodDateDouble = new Mock<IPeriodDate>();

            projectDouble.Setup(p => p.ContainsDates(periodDateDouble.Object)).Returns(true);
            projectDouble.Setup(c => c.IsFinished()).Returns(false);

            var collaboratorDouble = new Mock<ICollaborator>();
            collaboratorDouble.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

            var assocProjCollab1 = new AssociationProjectCollaborator(periodDateDouble.Object, collaboratorDouble.Object, projectDouble.Object);
            var assocProjCollab2 = new AssociationProjectCollaborator(periodDateDouble.Object, collaboratorDouble.Object, projectDouble.Object);

            collaboratorDouble.Setup(c => c.Equals(collaboratorDouble.Object)).Returns(true);
            projectDouble.Setup(c => c.Equals(projectDouble.Object)).Returns(true);
            periodDateDouble.Setup(c => c.Intersects(periodDateDouble.Object)).Returns(false);

            //act
            bool result = assocProjCollab1.Equals(assocProjCollab2);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void WhenReceivesNull_ReturnFalse()
        {
            //arrange
            var projectDouble = new Mock<IProject>();
            var periodDateDouble = new Mock<IPeriodDate>();

            projectDouble.Setup(p => p.ContainsDates(periodDateDouble.Object)).Returns(true);
            projectDouble.Setup(c => c.IsFinished()).Returns(false);

            var collaboratorDouble = new Mock<ICollaborator>();
            collaboratorDouble.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

            var assocProjCollab1 = new AssociationProjectCollaborator(periodDateDouble.Object, collaboratorDouble.Object, projectDouble.Object);
            var assocProjCollab2 = new AssociationProjectCollaborator(periodDateDouble.Object, collaboratorDouble.Object, projectDouble.Object);

            //act
            bool result = assocProjCollab1.Equals(null);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void WhenReceivesDiffernteObject_ReturnFalse()
        {
            //arrange
            var projectDouble = new Mock<IProject>();
            var periodDateDouble = new Mock<IPeriodDate>();

            projectDouble.Setup(p => p.ContainsDates(periodDateDouble.Object)).Returns(true);
            projectDouble.Setup(c => c.IsFinished()).Returns(false);

            var collaboratorDouble = new Mock<ICollaborator>();
            collaboratorDouble.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

            var assocProjCollab1 = new AssociationProjectCollaborator(periodDateDouble.Object, collaboratorDouble.Object, projectDouble.Object);
            var assocProjCollab2 = new AssociationProjectCollaborator(periodDateDouble.Object, collaboratorDouble.Object, projectDouble.Object);

            //act
            bool result = assocProjCollab1.Equals(new string("test"));

            //assert
            Assert.False(result);
        }
    }
}
