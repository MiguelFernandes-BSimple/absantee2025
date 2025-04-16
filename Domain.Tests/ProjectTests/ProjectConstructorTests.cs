using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectTests
{
    public class ProjectConstructorTests
    {
        [Fact]
        public void WhenPassingValidData_ThenCreatesProject()
        {
            new Project(1, "Title", "TTL", It.IsAny<IPeriodDate>());
        }

        [Theory]
        [InlineData("")]
        [InlineData("Este título tem facilmente mais de cinquenta caracteres.")]
        public void WhenPassingInvalidTitle_ThenThrowsException(string title)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                //act
                new Project(1, title, "PJT", It.IsAny<IPeriodDate>())
            );

            Assert.Equal("Invalid Arguments", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("pjt")]
        [InlineData("PJT@")]
        [InlineData("pjtJtjtjtjt")]
        public void WhenPassingInvalidAcronym_ThenThrowsException(string acronym)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                //act
                new Project(1, "Title", acronym, It.IsAny<IPeriodDate>())
            );

            Assert.Equal("Invalid Arguments", exception.Message);
        }
    }
}