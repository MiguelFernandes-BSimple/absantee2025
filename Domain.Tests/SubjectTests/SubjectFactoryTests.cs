using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Factory.SubjectFactory;
using Domain.IRepository;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.SubjectTests
{
    public class SubjectFactoryTests
    {
        [Fact]
        public void WhenPassingValidData_CreatesSubject()
        {
            // arrange
            var subjectRepoDouble = new Mock<ISubjectRepository>();
            subjectRepoDouble.Setup(srd => srd.TitleExists("title")).Returns(false);
            var subjectFactory = new SubjectFactory(subjectRepoDouble.Object);

            // act
            subjectFactory.Create("Title", "A Description");

            // assert
        }

        [Fact]
        public void WhenTitleAlreadyExists_ThenThrowsException()
        {
            // arrange
            var subjectRepoDouble = new Mock<ISubjectRepository>();
            subjectRepoDouble.Setup(srd => srd.TitleExists("Title")).Returns(true);
            var subjectFactory = new SubjectFactory(subjectRepoDouble.Object);

            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                //act
                subjectFactory.Create("Title", "A Description")
            );

            Assert.Equal("Title already exists", exception.Message);
        }

        [Fact]
        public void WhenPassingValidVisitor_ThenCreatesSubject()
        {
            // arrange
            var visitor = new Mock<ISubjectVisitor>();
            visitor.Setup(v => v._id).Returns(1);
            visitor.Setup(v => v._title).Returns("Title");
            visitor.Setup(v => v._description).Returns("Description");

            var subjectRepoDouble = new Mock<ISubjectRepository>();

            var subjectFactory = new SubjectFactory(subjectRepoDouble.Object);

            // act
            var result = subjectFactory.Create(visitor.Object);

            // assert
            Assert.NotNull(result);
            Assert.Equal(1, result.GetId());
            Assert.Equal("Title", result.GetTitle());
            Assert.Equal("Description", result.GetDescription());
        }
    }
}