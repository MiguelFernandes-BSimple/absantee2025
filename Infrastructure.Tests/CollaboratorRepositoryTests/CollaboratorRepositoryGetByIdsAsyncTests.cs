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
            var options = new DbContextOptionsBuilder<AbsanteeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
                .Options;

            using var context = new AbsanteeContext(options);

            var collaborator1 = new Mock<ICollaborator>();
            collaborator1.Setup(c => c.Id).Returns(1);
            var collaboratorDM1 = new CollaboratorDataModel(collaborator1.Object);
            context.Collaborators.Add(collaboratorDM1);

            var collaborator2 = new Mock<ICollaborator>();
            collaborator2.Setup(c => c.Id).Returns(2);
            var collaboratorDM2 = new CollaboratorDataModel(collaborator2.Object);
            context.Collaborators.Add(collaboratorDM2);

            var collaborator3 = new Mock<ICollaborator>();
            collaborator3.Setup(c => c.Id).Returns(3);
            var collaboratorDM3 = new CollaboratorDataModel(collaborator3.Object);
            context.Collaborators.Add(collaboratorDM3);

            await context.SaveChangesAsync();

            var collabsFiltered = new List<CollaboratorDataModel>()
            {
                collaboratorDM1,
                collaboratorDM3
            };

            var expected = new List<ICollaborator>
            {
                collaborator1.Object,
                collaborator3.Object
            };

            var collabMapper = new Mock<IMapper<ICollaborator, ICollaboratorVisitor>>();
            collabMapper.Setup(cm => cm.ToDomain(collabsFiltered)).Returns(expected);

            var collaboratorRepository = new CollaboratorRepository(context, collabMapper.Object);
            //Act 
            var result = await collaboratorRepository.GetByIdsAsync([1, 3]);

            //Assert
            Assert.True(expected.SequenceEqual(result));
        }
    }
}
