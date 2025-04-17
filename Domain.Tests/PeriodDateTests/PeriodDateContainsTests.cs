using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Tests.PeriodDateTests
{
    public class PeriodDateContainsTests
    {
        public static IEnumerable<object[]> ContainingPeriods()
        {
            yield return new object[] { new PeriodDate(new DateOnly(2020, 1, 1), new DateOnly(2021, 1, 1)) };
            yield return new object[] { new PeriodDate(new DateOnly(2020, 1, 2), new DateOnly(2020, 12, 31)) };
        }


        [Theory]
        [MemberData(nameof(ContainingPeriods))]
        public void WhenPassingContainingPeriods_ThenReturnTrue(PeriodDate containedPeriod)
        {
            //arrange
            DateOnly initDate = new DateOnly(2020, 1, 1);
            DateOnly finalDate = new DateOnly(2021, 1, 1);

            PeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.Contains(containedPeriod);

            //assert
            Assert.True(result);
        }

        public static IEnumerable<object[]> NonContainingPeriods()
        {
            yield return new object[] { new PeriodDate(new DateOnly(2018, 1, 1), new DateOnly(2019, 1, 1)) };
            yield return new object[] { new PeriodDate(new DateOnly(2022, 1, 1), new DateOnly(2023, 1, 1)) };
        }


        [Theory]
        [MemberData(nameof(NonContainingPeriods))]
        public void WhenPassingNonContainingPeriods_ThenReturnFalse(PeriodDate nonContainedPeriod)
        {
            //arrange
            DateOnly initDate = new DateOnly(2020, 1, 1);
            DateOnly finalDate = new DateOnly(2021, 1, 1);

            PeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.Contains(nonContainedPeriod);

            //assert
            Assert.False(result);
        }
    }
}
