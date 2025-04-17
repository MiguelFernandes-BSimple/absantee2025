using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Tests.PeriodDateTests
{
    public class PeriodDateIsFinalDateSmallerThanTests
    {

        [Fact]
        public void WhenPassingDatesBiggerThanFinalDate_ThenReturnFalse()
        {
            //arrange
            DateOnly initDate = new DateOnly(2020, 1, 1);
            DateOnly finalDate = new DateOnly(2021, 1, 1);
            DateOnly date = new DateOnly(2022, 1, 1);

            PeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.IsFinalDateSmallerThan(date);

            //assert
            Assert.True(result);
        }

        public static IEnumerable<object[]> DatesSmallerThanFinalDate()
        {
            yield return new object[] { new DateOnly(2020, 1, 1) };
            yield return new object[] { new DateOnly(2021, 1, 1) };
        }


        [Theory]
        [MemberData(nameof(DatesSmallerThanFinalDate))]
        public void WhenPassingDatesSmallerThanFinalDate_ThenReturnFalse(DateOnly date)
        {
            //arrange
            DateOnly initDate = new DateOnly(2020, 1, 1);
            DateOnly finalDate = new DateOnly(2021, 1, 1);

            PeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.IsFinalDateSmallerThan(date);

            //assert
            Assert.False(result);
        }
    }
}
