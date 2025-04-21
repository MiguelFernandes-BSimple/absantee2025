using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.TrainingModuleTests
{
    public class TrainingModuleConstructorTests
    {
        [Fact]
        public void WhenPassingValidData_ThenCreatesSubject()
        {
            // arrange
            var periodList = new List<PeriodDateTime>{
                new PeriodDateTime( DateTime.Now.AddDays(1), DateTime.Now.AddDays(2))
            };

            var subjectId = 2;

            // act
            new TrainingModule(1, subjectId, periodList);

            // assert
        }

        [Fact]
        public void WhenPassingValidData_ThenCreateSubject()
        {
            // arrange
            var periodList = new List<PeriodDateTime>{
                new PeriodDateTime( DateTime.Now.AddDays(1), DateTime.Now.AddDays(2))
            };

            var subjectId = 2;

            // act
            new TrainingModule(subjectId, periodList);

            // assert
        }
    }
}