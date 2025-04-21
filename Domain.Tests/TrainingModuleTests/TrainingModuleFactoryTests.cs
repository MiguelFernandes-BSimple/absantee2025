using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.TrainingModuleTests
{
    public class TrainingModuleFactoryTests
    {
        [Fact]
        public void WhenPassingValidData_ThenCreatesTrainingModule()
        {
            // arrange
            var subjectRepoDouble = new Mock<ISubjectRepository>();
            var subjectDouble = new Mock<ISubject>();

            subjectRepoDouble.Setup(srd => srd.GetById(It.IsAny<long>())).Returns(subjectDouble.Object);

            var trainingModuleFactory = new TrainingModuleFactory(subjectRepoDouble.Object);

            var periodList = new List<PeriodDateTime> { new PeriodDateTime(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2)) };

            // act
            var result = trainingModuleFactory.Create(2, periodList);

            // assert
            Assert.Equal(2, result.GetSubjectId());
            Assert.Equal(periodList, result.GetPeriodsList());
        }

        [Fact]
        public void WhenPassingValidVisitor_ThenCreatesTrainingModule()
        {
            // arrange
            var periodList = new List<PeriodDateTime> { new PeriodDateTime(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2)) };

            var visitorDouble = new Mock<ITrainingModuleVisitor>();
            visitorDouble.Setup(vd => vd.id).Returns(1);
            visitorDouble.Setup(vd => vd.subjectId).Returns(2);
            visitorDouble.Setup(vd => vd.periodsList).Returns(periodList);

            var subjectRepoDouble = new Mock<ISubjectRepository>();

            var trainingModuleFactory = new TrainingModuleFactory(subjectRepoDouble.Object);

            // act
            var result = trainingModuleFactory.Create(visitorDouble.Object);

            // assert
            Assert.Equal(1, result.GetId());
            Assert.Equal(2, result.GetSubjectId());
            Assert.Equal(periodList, result.GetPeriodsList());
        }

        [Fact]
        public void WhenSubjectDoesntExist_ThenThrowsException()
        {
            // arrange
            var subjectRepoDouble = new Mock<ISubjectRepository>();
            var subjectDouble = new Mock<ISubject>();

            subjectRepoDouble.Setup(srd => srd.GetById(It.IsAny<long>())).Returns((ISubject?)null);

            var trainingModuleFactory = new TrainingModuleFactory(subjectRepoDouble.Object);

            var periodList = new List<PeriodDateTime> { new PeriodDateTime(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2)) };

            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                //act
                trainingModuleFactory.Create(2, periodList)
            );

            Assert.Equal("Subject does not exist", exception.Message);

        }
    }
}