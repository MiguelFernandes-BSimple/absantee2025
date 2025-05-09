using Domain.Interfaces;
using Domain.Visitor;
using Infrastructure.DataModel;
using AutoMapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Domain.Models;

namespace Infrastructure.Tests.CollaboratorRepositoryTests
{
    public class CollaboratorRepositoryGetByIdsAsyncTests : RepositoryTestBase
    {
        [Fact]
        public async Task WhenSearchingById_ThenReturnsAllCollaboratorsWithId()
        {

            // Arrange
            var collaborator1 = new Mock<ICollaborator>();
            var guid1 = Guid.NewGuid();
            var userguid1 = Guid.NewGuid();
            var period1 = new PeriodDateTime(DateTime.Today, DateTime.Today.AddYears(1));
            collaborator1.Setup(c => c.Id).Returns(guid1);
            collaborator1.Setup(c => c.UserId).Returns(userguid1);
            var collaboratorDM1 = new CollaboratorDataModel(collaborator1.Object);
            context.Collaborators.Add(collaboratorDM1);

            var collaborator2 = new Mock<ICollaborator>();
            var guid2 = Guid.NewGuid();
            var userguid2 = Guid.NewGuid();
            var period2 = new PeriodDateTime(DateTime.Today, DateTime.Today.AddYears(1));
            collaborator2.Setup(c => c.Id).Returns(guid2);
            collaborator2.Setup(c => c.UserId).Returns(userguid2);
            var collaboratorDM2 = new CollaboratorDataModel(collaborator2.Object);
            context.Collaborators.Add(collaboratorDM2);

            var collaborator3 = new Mock<ICollaborator>();
            var guid3 = Guid.NewGuid();
            var userguid3 = Guid.NewGuid();
            var period3 = new PeriodDateTime(DateTime.Today, DateTime.Today.AddYears(1));
            collaborator3.Setup(c => c.Id).Returns(guid3);
            collaborator3.Setup(c => c.UserId).Returns(userguid3);
            var collaboratorDM3 = new CollaboratorDataModel(collaborator3.Object);
            context.Collaborators.Add(collaboratorDM3);

            await context.SaveChangesAsync();

            _mapper.Setup(m => m.Map<CollaboratorDataModel, Collaborator>(
            It.Is<CollaboratorDataModel>(t =>
                t.Id == collaboratorDM1.Id
                )))
                .Returns(new Collaborator(collaboratorDM1.Id, collaboratorDM1.UserId, collaboratorDM1.PeriodDateTime));

            _mapper.Setup(m => m.Map<CollaboratorDataModel, Collaborator>(
            It.Is<CollaboratorDataModel>(t =>
                t.Id == collaboratorDM3.Id
                )))
                .Returns(new Collaborator(collaboratorDM3.Id, collaboratorDM3.UserId, collaboratorDM3.PeriodDateTime));

            var collaboratorRepository = new CollaboratorRepositoryEF(context, _mapper.Object);
            //Act 
            var result = await collaboratorRepository.GetByIdsAsync([guid1, guid3]);

            //Assert
            Assert.Equal(2, result.Count());

            Assert.Contains(result, c => c.Id == guid1);
            Assert.Contains(result, c => c.Id == guid3);
        }
    }
}
