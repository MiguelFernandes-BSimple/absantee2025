using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Tests.PeriodDateTests
{
    public class Intersects
    {
        public static IEnumerable<object[]> IntersectionDates()
        {
            yield return new object[] { new PeriodDate(new DateOnly(1, 1, 2021), new DateOnly(1, 1, 2022)) };
            yield return new object[] { new PeriodDate(new DateOnly(1, 1, 2019), new DateOnly(1, 1, 2020)) };
            yield return new object[] { new PeriodDate(new DateOnly(2, 1, 2020), new DateOnly(3, 1, 2020)) };
        }


        [Theory]
        [MemberData(nameof(IntersectionDates))]
        public void WhenPassingIntersectionPeriods_ThenReturnTrue(IPeriodDate intersectPeriod)
        {
            //arrange
            DateOnly initDate = new DateOnly(1, 1, 2020);
            DateOnly finalDate = new DateOnly(1, 1, 2021);

            IPeriodDate periodDate = new PeriodDate(initDate, finalDate);
            
            //act
            var result = periodDate.Intersects(intersectPeriod);

            //assert
            Assert.True(result);
        }

        public static IEnumerable<object[]> NonIntersectionDates()
        {
            yield return new object[] { new PeriodDate(new DateOnly(1, 1, 2018), new DateOnly(1, 1, 2019)) };
            yield return new object[] { new PeriodDate(new DateOnly(1, 1, 202), new DateOnly(1, 1, 2023)) };
        }


        [Theory]
        [MemberData(nameof(NonIntersectionDates))]
        public void WhenPassingNonIntersectionPeriods_ThenReturnFalse(IPeriodDate intersectPeriod)
        {
            //arrange
            DateOnly initDate = new DateOnly(1, 1, 2020);
            DateOnly finalDate = new DateOnly(1, 1, 2021);

            IPeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.Intersects(intersectPeriod);

            //assert
            Assert.False(result);
        }
    }
}
