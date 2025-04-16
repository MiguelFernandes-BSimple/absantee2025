using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectTests
{
    public class ProjectGetPeriodDateTests
    {
        [Fact]
        public void WhenCallingMethod_ThenReturnsPeriodDate()
        {
            // arrange
            var startDate = new DateOnly(2025, 1, 1);
            var endDate = new DateOnly(2025, 1, 15);
            var periodDouble = new Mock<IPeriodDate>();
            periodDouble.Setup(pd => pd.GetInitDate()).Returns(startDate);
            periodDouble.Setup(pd => pd.GetFinalDate()).Returns(endDate);

            var project = new Project(1, "title", "TTL", periodDouble.Object);

            // act
            var result = project.GetPeriodDate();

            // assert
            Assert.Equal(result.GetInitDate(), startDate);
            Assert.Equal(result.GetFinalDate(), endDate);
        }
    }
}