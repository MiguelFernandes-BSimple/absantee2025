using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.TrainingModuleTests
{
    public class TrainingModuleConstructorTests
    {
        [Fact]
        public void WhenPassingValidData_ThenTrainingModuleIsCreated()
        {
            //Arrange
            var periodDateTime1 = new PeriodDateTime(DateTime.Now.AddDays(1), DateTime.Now.AddDays(3));

            var periodDateTime2 = new PeriodDateTime(DateTime.Now.AddDays(5), DateTime.Now.AddDays(8));

            var periods = new List<PeriodDateTime>() { periodDateTime1, periodDateTime2};

            //Act
            new TrainingModule(It.IsAny<Guid>(), periods);
        }

        [Fact]
        public void WhenPeriodsIntersect_ThenThrowException()
        {
            //Arrange
            var periodDateTime1 = new PeriodDateTime(DateTime.Now.AddDays(1), DateTime.Now.AddDays(3));
            var periodDateTime2 = new PeriodDateTime(DateTime.Now.AddDays(2), DateTime.Now.AddDays(6));

            var periods = new List<PeriodDateTime>() { periodDateTime1, periodDateTime2 };

            //Arrange
            var exception = Assert.Throws<ArgumentException>(() =>
                //Act
                new TrainingModule(It.IsAny<Guid>(), periods)
            );

            Assert.Equal("Training periods cannot overlap.", exception.Message);
        }

        [Fact]
        public void WhenPeriodStartsInPast_ThenThrowException()
        {
            //Arrange
            var periodDateTime1 = new PeriodDateTime(DateTime.Now.AddDays(6), DateTime.Now.AddDays(9));
            var periodDateTime2 = new PeriodDateTime(DateTime.Now.AddDays(-5), DateTime.Now.AddDays(3));

            var periods = new List<PeriodDateTime>() { periodDateTime1, periodDateTime2 };

            //Arrange
            var exception = Assert.Throws<ArgumentException>(() =>
                //Act
                new TrainingModule(It.IsAny<Guid>(), periods)
            );

            Assert.Equal("Periods must start in the future", exception.Message);
        }
    }
}
