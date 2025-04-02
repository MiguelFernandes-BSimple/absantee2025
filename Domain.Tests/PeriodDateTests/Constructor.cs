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
    public class Constructor
    {
        [Fact]
        public void WhenCreatingWithValidDates_ThenPeriodDateIsCreated()
        {
            //arrange
            DateOnly initDate = new DateOnly(1, 1, 2020);
            DateOnly finalDate = new DateOnly(1, 1, 2021);

            //act
            new PeriodDate(initDate, finalDate);
            //assert
        }

        public static IEnumerable<object[]> PeriodDateData_InvalidFields()
        {
            yield return new object[] { new DateOnly(1, 1, 2021), new DateOnly(1, 1, 2021) };
            yield return new object[] { new DateOnly(1, 1, 2021), new DateOnly(1, 1, 2020) };
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
