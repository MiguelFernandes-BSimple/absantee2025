using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Tests.HolidayPeriodTests;

namespace Domain.Tests.PeriodDateTests
{
    public class ContainsDate
    {
        public static IEnumerable<object[]> ContainingDate()
        {
            yield return new object[] { new DateOnly(1, 1, 2020) };
            yield return new object[] { new DateOnly(1, 1, 2021) };
            yield return new object[] { new DateOnly(2, 1, 2020) };
        }


        [Theory]
        [MemberData(nameof(ContainingDate))]
        public void WhenPassingContainingDate_ThenReturnTrue(DateOnly containedDate)
        {
            //arrange
            DateOnly initDate = new DateOnly(1, 1, 2020);
            DateOnly finalDate = new DateOnly(1, 1, 2021);

            IPeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.ContainsDate(containedDate);

            //assert
            Assert.True(result);
        }

        public static IEnumerable<object[]> NonContainingDate()
        {
            yield return new object[] { new DateOnly(1, 1, 2019) };
            yield return new object[] { new DateOnly(1, 1, 2022) };
        }


        [Theory]
        [MemberData(nameof(NonContainingDate))]
        public void WhenPassingNonContainingDate_ThenReturnFalse(DateOnly nonContainedDate)
        {
            //arrange
            DateOnly initDate = new DateOnly(1, 1, 2020);
            DateOnly finalDate = new DateOnly(1, 1, 2021);

            IPeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.ContainsDate(nonContainedDate);

            //assert
            Assert.False(result);
        }
    }
}
