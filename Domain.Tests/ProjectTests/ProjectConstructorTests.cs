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
            new Project(Guid.NewGuid(), "Title", "TTL", It.IsAny<PeriodDate>());
        }

        [Theory]
        [InlineData("")]
        [InlineData("Este título tem facilmente mais de cinquenta caracteres.")]
        public void WhenPassingInvalidTitle_ThenThrowsException(string title)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                //act
                new Project(Guid.NewGuid(), title, "PJT", It.IsAny<PeriodDate>())
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
                new Project(Guid.NewGuid(), "Title", acronym, It.IsAny<PeriodDate>())
            );

            Assert.Equal("Invalid Arguments", exception.Message);
        }
    }
}