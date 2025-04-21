using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.FormationSubjectRepositoryTests
{
    public class FormationSubjectRepositoryGetByIdAsyncTests
    {
        [Fact]
        public async Task WhenSearchingById_ThenReturnsSubjectWithId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AbsanteeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
                .Options;

            using var context = new AbsanteeContext(options);

            var subject1 = new Mock<IFormationSubject>();
            subject1.Setup(c => c.GetId()).Returns(1);
            subject1.Setup(c => c.GetTitle()).Returns("DOTNET");
            subject1.Setup(c => c.GetDescription()).Returns("test");
            var subjectDM1 = new FormationSubjectDataModel(subject1.Object);
            context.FormationSubjects.Add(subjectDM1);

            var subject2 = new Mock<IFormationSubject>();
            subject2.Setup(c => c.GetId()).Returns(2);
            subject2.Setup(c => c.GetTitle()).Returns("NODEJS");
            subject2.Setup(c => c.GetDescription()).Returns("test");
            var subjectDM2 = new FormationSubjectDataModel(subject2.Object);
            context.FormationSubjects.Add(subjectDM2);

            var subject3 = new Mock<IFormationSubject>();
            subject3.Setup(c => c.GetId()).Returns(3);
            subject3.Setup(c => c.GetTitle()).Returns("SPRINGBOOT");
            subject3.Setup(c => c.GetDescription()).Returns("test");
            var subjectDM3 = new FormationSubjectDataModel(subject3.Object);
            context.FormationSubjects.Add(subjectDM3);

            await context.SaveChangesAsync();

            var expected = new Mock<IFormationSubject>().Object;

            var subjectMapper = new Mock<IMapper<IFormationSubject, IFormationSubjectVisitor>>();
            subjectMapper.Setup(cm => cm.ToDomain(subjectDM3)).Returns(expected);

            var subjectRepository = new FormationSubjectRepository(context, subjectMapper.Object);
            //Act 
            var result = await subjectRepository.GetByIdAsync(3);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task WhenSearchingByIdWithNoSubjects_ThenReturnsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AbsanteeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
                .Options;

            using var context = new AbsanteeContext(options);

            var subject1 = new Mock<IFormationSubject>();
            subject1.Setup(c => c.GetId()).Returns(1);
            subject1.Setup(c => c.GetTitle()).Returns("DOTNET");
            subject1.Setup(c => c.GetDescription()).Returns("test");
            var subjectDM1 = new FormationSubjectDataModel(subject1.Object);
            context.FormationSubjects.Add(subjectDM1);

            var subject2 = new Mock<IFormationSubject>();
            subject2.Setup(c => c.GetId()).Returns(2);
            subject2.Setup(c => c.GetTitle()).Returns("NODEJS");
            subject2.Setup(c => c.GetDescription()).Returns("test");
            var subjectDM2 = new FormationSubjectDataModel(subject2.Object);
            context.FormationSubjects.Add(subjectDM2);

            var subject3 = new Mock<IFormationSubject>();
            subject3.Setup(c => c.GetId()).Returns(3);
            subject3.Setup(c => c.GetTitle()).Returns("SPRINGBOOT");
            subject3.Setup(c => c.GetDescription()).Returns("test");
            var subjectDM3 = new FormationSubjectDataModel(subject3.Object);
            context.FormationSubjects.Add(subjectDM3);

            await context.SaveChangesAsync();

            var expected = new Mock<IFormationSubject>().Object;

            var subjectMapper = new Mock<IMapper<IFormationSubject, IFormationSubjectVisitor>>();
            subjectMapper.Setup(cm => cm.ToDomain(subjectDM3)).Returns(expected);

            var subjectRepository = new FormationSubjectRepository(context, subjectMapper.Object);
            //Act 
            var result = await subjectRepository.GetByIdAsync(4);

            //Assert
            Assert.Null(result);
        }
    }
}