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

namespace Infrastructure.Tests.CollaboratorRepositoryTests
{
    public class CollaboratorRepositoryGetByIdsAsyncTests
    {
        [Fact]
        public async Task WhenSearchingById_ThenReturnsAllCollaboratorsWithId()
        {
            // Arrange
            var collaboratorDM1 = new Mock<ICollaboratorVisitor>();
            var collaboratorDM2 = new Mock<ICollaboratorVisitor>();
            var collaboratorDM3 = new Mock<ICollaboratorVisitor>();
            var users = new List<CollaboratorDataModel>
            {
                (CollaboratorDataModel)collaboratorDM1.Object,
                (CollaboratorDataModel)collaboratorDM2.Object,
                (CollaboratorDataModel)collaboratorDM3.Object
            }.AsQueryable();

            var mockSet = new Mock<DbSet<CollaboratorDataModel>>();
            mockSet.As<IQueryable<CollaboratorDataModel>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSet.As<IQueryable<CollaboratorDataModel>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSet.As<IQueryable<CollaboratorDataModel>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSet.As<IQueryable<CollaboratorDataModel>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var absanteeMock = new Mock<IAbsanteeContext>();
            absanteeMock.Setup(a => a.Collaborators).Returns(mockSet.Object);

            collaboratorDM1.Setup(c => c.Id).Returns(1);
            collaboratorDM2.Setup(c => c.Id).Returns(2);
            collaboratorDM3.Setup(c => c.Id).Returns(3);

            var userFiltered = new List<CollaboratorDataModel>
            {
                (CollaboratorDataModel)collaboratorDM1.Object,
                (CollaboratorDataModel)collaboratorDM3.Object
            };

            var expected = new List<Collaborator>
            {
                new Mock<Collaborator>().Object,
                new Mock<Collaborator>().Object
            };

            var collabMapper = new Mock<IMapper<ICollaborator, ICollaboratorVisitor>>();
            collabMapper.Setup(cm => cm.ToDomain(userFiltered)).Returns(expected);

            var collaboratorRepository = new CollaboratorRepository((AbsanteeContext)absanteeMock.Object, collabMapper.Object);
            //Act 
            var result = await collaboratorRepository.GetByIdsAsync([1, 3]);

            //Assert
            Assert.True(expected.SequenceEqual(result));
        }

        [Fact]
        public async Task WhenSearchingByIdWithNoCollaborators_ThenReturnsEmptyList()
        {
            // Arrange
            var collaboratorDM1 = new Mock<ICollaboratorVisitor>();
            var collaboratorDM2 = new Mock<ICollaboratorVisitor>();
            var collaboratorDM3 = new Mock<ICollaboratorVisitor>();
            var users = new List<CollaboratorDataModel>
            {
                (CollaboratorDataModel)collaboratorDM1.Object,
                (CollaboratorDataModel)collaboratorDM2.Object,
                (CollaboratorDataModel)collaboratorDM3.Object
            }.AsQueryable();

            var mockSet = new Mock<DbSet<CollaboratorDataModel>>();
            mockSet.As<IQueryable<CollaboratorDataModel>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSet.As<IQueryable<CollaboratorDataModel>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSet.As<IQueryable<CollaboratorDataModel>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSet.As<IQueryable<CollaboratorDataModel>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var absanteeMock = new Mock<IAbsanteeContext>();
            absanteeMock.Setup(a => a.Collaborators).Returns(mockSet.Object);

            collaboratorDM1.Setup(c => c.Id).Returns(1);
            collaboratorDM2.Setup(c => c.Id).Returns(2);
            collaboratorDM3.Setup(c => c.Id).Returns(3);

            var userFiltered = new List<CollaboratorDataModel>();

            var expected = new List<Collaborator>();

            var collabMapper = new Mock<IMapper<ICollaborator, ICollaboratorVisitor>>();
            collabMapper.Setup(cm => cm.ToDomain(userFiltered)).Returns(expected);

            var collaboratorRepository = new CollaboratorRepository((AbsanteeContext)absanteeMock.Object, collabMapper.Object);
            //Act 
            var result = await collaboratorRepository.GetByIdsAsync([4, 5]);

            //Assert
            Assert.Empty(result);
        }
    }
}
