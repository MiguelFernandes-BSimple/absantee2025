using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Tests.SubjectTests
{
    public class SubjectConstructorTests
    {
        [Fact]
        public void WhenPassingValidData_ThenCreatesSubject()
        {
            // arrange 

            // act
            new Subject(1, "Title", "A subject description");

            // assert
        }

        [Theory]
        [InlineData("")]
        [InlineData("A title bigger than twenty characters")]
        public void WhenPassingInvalidTitle_ThenThrowsArgumentException(string title)
        {
            // arrange

            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                //act
                new Subject(1, title, "Description")
            );

            Assert.Equal("Invalid Arguments", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text.")]
        public void WhenPassingInvalidDescription_ThenThrowsArgumentException(string description)
        {
            // arrange

            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                //act
                new Subject(1, "title", description)
            );

            Assert.Equal("Invalid Arguments", exception.Message);
        }
    }
}