using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Tests.PeriodDateTests
{
    public class Contains
    {
        public static IEnumerable<object[]> ContainingPeriods()
        {
            yield return new object[] { new PeriodDate(new DateOnly(1, 1, 2020), new DateOnly(1, 1, 2021)) };
            yield return new object[] { new PeriodDate(new DateOnly(2, 1, 2020), new DateOnly(31, 12, 2020)) };
        }


        [Theory]
        [MemberData(nameof(ContainingPeriods))]
        public void WhenPassingContainingPeriods_ThenReturnTrue(IPeriodDate containedPeriod)
        {
            //arrange
            DateOnly initDate = new DateOnly(1, 1, 2020);
            DateOnly finalDate = new DateOnly(1, 1, 2021);

            IPeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.Contains(containedPeriod);

            //assert
            Assert.True(result);
        }

        public static IEnumerable<object[]> NonContainingPeriods()
        {
            yield return new object[] { new PeriodDate(new DateOnly(1, 1, 2018), new DateOnly(1, 1, 2019)) };
            yield return new object[] { new PeriodDate(new DateOnly(1, 1, 202), new DateOnly(1, 1, 2023)) };
        }


        [Theory]
        [MemberData(nameof(NonContainingPeriods))]
        public void WhenPassingNonContainingPeriods_ThenReturnFalse(IPeriodDate nonContainedPeriod)
        {
            //arrange
            DateOnly initDate = new DateOnly(1, 1, 2020);
            DateOnly finalDate = new DateOnly(1, 1, 2021);

            IPeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.Contains(nonContainedPeriod);

            //assert
            Assert.False(result);
        }
    }
}
