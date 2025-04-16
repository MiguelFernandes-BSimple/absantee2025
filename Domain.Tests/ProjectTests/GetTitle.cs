using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectTests
{
    public class GetTitle
    {
        [Fact]
        public void WhenCallingMethod_ThenReturnsTitle()
        {
            // arrange
            var title = "Title";
            var project = new Project(1, title, "TTL", It.IsAny<IPeriodDate>());

            // act
            var result = project.GetTitle();

            // assert
            Assert.Equal(result, title);
        }
    }
}