using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Domain.Models;
using Moq;

namespace Domain.Tests.TrainingSubjectTests
{
    public class TrainingSubjectConstructorsTests
    {
        [Fact]
        public void WhenPassingValidData_ThenCreatesTrainingSubject()
        {
            //Arrange

            //Act
            new TrainingSubject("Subject", "Description");

            //Assert
        }

        [Theory]
        [InlineData("")]
        [InlineData("This subject title clearly exceeds the twenty character limit.")]
        public void WhenPassingInvalidSubject_ThenThrowsException(string subject)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                // act
                new TrainingSubject(subject, "ValidDescription")
            );

            Assert.Equal("Subject must be alphanumeric and no longer than 20 characters.", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("This description is deliberately made very long in order to go beyond the one hundred character limit that has been imposed.")]
        public void WhenPassingInvalidDescription_ThenThrowsException(string description)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                // act
                new TrainingSubject("ValidSubject", description)
            );

            Assert.Equal("Description must be alphanumeric and no longer than 100 characters.", exception.Message);
        }
    }
}
