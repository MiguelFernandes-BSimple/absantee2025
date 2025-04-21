using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Infrastructure.Tests.FormationSubjectRepositoryTests
{
    public class FormationSubjectRepositoryGetByTitleAsyncTests
    {
        [Fact]
        public async Task WhenTitleExists_ThenReturnsCorrectSubject()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AbsanteeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AbsanteeContext(options);

            var subjectMock = new Mock<IFormationSubject>();
            subjectMock.Setup(s => s.GetId()).Returns(1);
            subjectMock.Setup(s => s.GetTitle()).Returns("DOTNET");
            subjectMock.Setup(s => s.GetDescription()).Returns("Framework");

            var subjectDM = new FormationSubjectDataModel(subjectMock.Object);
            context.FormationSubjects.Add(subjectDM);
            await context.SaveChangesAsync();

            var expected = new Mock<IFormationSubject>().Object;

            var mapperMock = new Mock<IMapper<IFormationSubject, IFormationSubjectVisitor>>();
            mapperMock.Setup(m => m.ToDomain(subjectDM)).Returns(expected);

            var repo = new FormationSubjectRepository(context, mapperMock.Object);

            // Act
            var result = await repo.GetByTitleAsync("DOTNET");

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task WhenTitleDoesNotExist_ThenReturnsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AbsanteeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AbsanteeContext(options);

            var subjectMock = new Mock<IFormationSubject>();
            subjectMock.Setup(s => s.GetId()).Returns(1);
            subjectMock.Setup(s => s.GetTitle()).Returns("DOTNET");
            subjectMock.Setup(s => s.GetDescription()).Returns("Framework");

            var subjectDM = new FormationSubjectDataModel(subjectMock.Object);
            context.FormationSubjects.Add(subjectDM);
            await context.SaveChangesAsync();

            var mapperMock = new Mock<IMapper<IFormationSubject, IFormationSubjectVisitor>>();
            var repo = new FormationSubjectRepository(context, mapperMock.Object);

            // Act
            var result = await repo.GetByTitleAsync("NODEJS");

            // Assert
            Assert.Null(result);
        }
    }
}
