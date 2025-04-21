using Domain.Factory;
using Domain.IRepository;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.FormationSubjectTests
{
    public class FormationSubjectFactoryTests
    {
        [Fact]
        public async Task WhenCreatingFormationSubject_ThenFormationSubjectIsCreated()
        {
            //arrange
            string title = "DOTNET";

            var formationSubjectRepository = new Mock<IFormationSubjectRepository>();
            formationSubjectRepository.Setup(repo => repo.GetByTitleAsync(title)).ReturnsAsync((IFormationSubject?)null);

            var formationSubjectFactory = new FormationSubjectFactory(formationSubjectRepository.Object);

            //act
            var result = await formationSubjectFactory.Create(title, "test description");

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task WhenCreatingFormationSubjectWithAnRepeatedTitle_ThenThrowsArgumentException()
        {
            string title = "DOTNET";
            var existingFormationSubject = new Mock<IFormationSubject>();

            var formationSubjectRepository = new Mock<IFormationSubjectRepository>();
            formationSubjectRepository.Setup(repo => repo.GetByTitleAsync(title)).ReturnsAsync(existingFormationSubject.Object);

            var formationSubjectFactory = new FormationSubjectFactory(formationSubjectRepository.Object);

            //Assert
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                        // Act
                        formationSubjectFactory.Create(title, "test description"));

            Assert.Equal("A subject with this title already exists.", exception.Message);
        }

        [Fact]
        public void WhenCreatingFormationSubjectFromDataModel_ThenFormationSubjectIsCreated()
        {
            //Arrange
            var formationSubjectVisitor = new Mock<IFormationSubjectVisitor>();

            formationSubjectVisitor.Setup(u => u.Id).Returns(1);
            formationSubjectVisitor.Setup(u => u.Title).Returns("DOTNET");
            formationSubjectVisitor.Setup(u => u.Description).Returns("test description");

            var formationSubjectRepository = new Mock<IFormationSubjectRepository>();
            var formationSubjectFactory = new FormationSubjectFactory(formationSubjectRepository.Object);

            //Act
            var result = formationSubjectFactory.Create(formationSubjectVisitor.Object);

            //Assert
            Assert.NotNull(result);
        }

    }
}