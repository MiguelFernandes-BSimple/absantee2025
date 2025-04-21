using Domain.Interfaces;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Infrastructure.Tests.FormationModuleRepositoryTests
{
    public class FormationModuleRepositoryGetBySubjectIdTests
    {
        [Fact]
        public async Task WhenSubjectIdExists_ThenReturnsCorrectModule()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AbsanteeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AbsanteeContext(options);

            var moduleDM = new FormationModuleDataModel
            {
                Id = 1,
                FormationSubjectId = 100
            };

            context.FormationModules.Add(moduleDM);
            await context.SaveChangesAsync();

            var expected = new Mock<IFormationModule>().Object;

            var mapperMock = new Mock<IMapper<IFormationModule, IFormationModuleVisitor>>();
            mapperMock.Setup(m => m.ToDomain(moduleDM)).Returns(expected);

            var repository = new FormationModuleRepository(context, mapperMock.Object);

            // Act
            var result = await repository.GetBySubjectId(100);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task WhenSubjectIdDoesNotExist_ThenReturnsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AbsanteeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AbsanteeContext(options);

            var moduleDM = new FormationModuleDataModel
            {
                Id = 1,
                FormationSubjectId = 200
            };

            context.FormationModules.Add(moduleDM);
            await context.SaveChangesAsync();

            var mapperMock = new Mock<IMapper<IFormationModule, IFormationModuleVisitor>>();

            var repository = new FormationModuleRepository(context, mapperMock.Object);

            // Act
            var result = await repository.GetBySubjectId(999);

            // Assert
            Assert.Null(result);
        }
    }
}
