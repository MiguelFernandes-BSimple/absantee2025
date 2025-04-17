using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.PeriodDateTests
{
    public class PeriodDateConstructorTests
    {
        [Fact]
        public void WhenCreatingWithValidDates_ThenPeriodDateIsCreated()
        {
            //arrange
            DateOnly initDate = new DateOnly(2020, 1, 1);
            DateOnly finalDate = new DateOnly(2021, 1, 1);

            //act
            new PeriodDate(initDate, finalDate);
            //assert
        }

        public static IEnumerable<object[]> PeriodDateData_InvalidFields()
        {
            yield return new object[] { new DateOnly(2021, 1, 1), new DateOnly(2020, 1, 1) };
        }


        [Theory]
        [MemberData(nameof(PeriodDateData_InvalidFields))]
        public void WhenCreatingWithInvalidDates_ThenThrowException(DateOnly initDate, DateOnly finalDate)
        {
            //arrange

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(
                () =>
                    //act
                    new PeriodDate(initDate, finalDate)
            );
            Assert.Equal("Invalid Arguments", exception.Message);
        }
    }
}
