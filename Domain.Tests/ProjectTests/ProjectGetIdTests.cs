using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectTests
{
    public class ProjectGetIdTests
    {
        [Fact]
        public void WhenCallingMethod_ThenReturnsId()
        {
            // arrange
            var id = 1;
            var project = new Project(id, "Title", "TTL", It.IsAny<IPeriodDate>());

            // act
            var result = project.GetId();

            // assert
            Assert.Equal(result, id);
        }
    }
}