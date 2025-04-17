using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectTests
{
    public class ProjectSetIdTests
    {
        [Fact]
        public void WhenPassingCorrectValue_ThenSetsId()
        {
            // arrange
            var id = 5;
            var project = new Project(1, "Title", "TTL", It.IsAny<PeriodDate>());

            // act
            project.SetId(id);
            var result = project.GetId();

            // assert
            Assert.Equal(result, id);
        }
    }
}