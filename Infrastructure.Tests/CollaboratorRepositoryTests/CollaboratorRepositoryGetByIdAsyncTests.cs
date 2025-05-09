using Domain.Interfaces;
using Domain.Visitor;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Moq;
using Infrastructure.Repositories;
using Domain.Models;

namespace Infrastructure.Tests.CollaboratorRepositoryTests
{
    public class CollaboratorRepositoryGetByIdAsyncTests : RepositoryTestBase
    {
        [Fact]
        public async Task WhenSearchingById_ThenReturnsCollaborator()
        {
            // Arrange
            var collaborator1 = new Mock<ICollaborator>();
            var guid1 = Guid.NewGuid();
            var userguid1 = Guid.NewGuid();
            var period1 = new PeriodDateTime(DateTime.Today.AddDays(-1), DateTime.Today);
            collaborator1.Setup(c => c.Id).Returns(guid1);
            collaborator1.Setup(c => c.UserId).Returns(userguid1);
            var collaboratorDM1 = new CollaboratorDataModel(collaborator1.Object);
            context.Collaborators.Add(collaboratorDM1);

            var collaborator2 = new Mock<ICollaborator>();
            var guid2 = Guid.NewGuid();
            collaborator1.Setup(c => c.Id).Returns(guid2);
            var collaboratorDM2 = new CollaboratorDataModel(collaborator2.Object);
            context.Collaborators.Add(collaboratorDM2);

            await context.SaveChangesAsync();

            var expected = new Mock<ICollaborator>().Object;

            _mapper.Setup(m => m.Map<CollaboratorDataModel, Collaborator>(
            It.Is<CollaboratorDataModel>(t =>
                t.Id == collaboratorDM1.Id
                )))
                .Returns(new Collaborator(collaboratorDM1.Id, collaboratorDM1.UserId, collaboratorDM1.PeriodDateTime));

            var collaboratorRepository = new CollaboratorRepositoryEF(context, _mapper.Object);
            //Act 
            var result = await collaboratorRepository.GetByIdAsync(guid1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(collaboratorDM1.Id, result.Id);
        }

        [Fact]
        public async Task WhenSearchingByIdWithNoCollaborators_ThenReturnsNull()
        {
            // Arrange
            var collaborator1 = new Mock<ICollaborator>();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var userguid1 = Guid.NewGuid();
            var period1 = new PeriodDateTime(DateTime.Today.AddDays(-1), DateTime.Today);
            collaborator1.Setup(c => c.Id).Returns(guid1);
            collaborator1.Setup(c => c.UserId).Returns(userguid1);
            var collaboratorDM1 = new CollaboratorDataModel(collaborator1.Object);
            context.Collaborators.Add(collaboratorDM1);

            await context.SaveChangesAsync();

            _mapper.Setup(m => m.Map<CollaboratorDataModel, Collaborator>(
           It.Is<CollaboratorDataModel>(t =>
               t.Id == collaboratorDM1.Id
               )))
               .Returns(new Collaborator(collaboratorDM1.Id, collaboratorDM1.UserId, collaboratorDM1.PeriodDateTime));

            var collaboratorRepository = new CollaboratorRepositoryEF(context, _mapper.Object);

            //Act 
            var result = await collaboratorRepository.GetByIdAsync(guid2);

            //Assert
            Assert.Null(result);
        }
    }
}
