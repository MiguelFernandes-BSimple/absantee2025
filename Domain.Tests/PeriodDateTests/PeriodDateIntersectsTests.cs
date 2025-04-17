using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Tests.PeriodDateTests
{
    public class PeriodDateIntersectsTests
    {
        public static IEnumerable<object[]> IntersectionDates()
        {
            yield return new object[] { new PeriodDate(new DateOnly(2021, 1, 1), new DateOnly(2022, 1, 1)) };
            yield return new object[] { new PeriodDate(new DateOnly(2019, 1, 1), new DateOnly(2020, 1, 1)) };
            yield return new object[] { new PeriodDate(new DateOnly(2020, 1, 3), new DateOnly(2020, 1, 3)) };
        }


        [Theory]
        [MemberData(nameof(IntersectionDates))]
        public void WhenPassingIntersectionPeriods_ThenReturnTrue(PeriodDate intersectPeriod)
        {
            //arrange
            DateOnly initDate = new DateOnly(2020, 1, 1);
            DateOnly finalDate = new DateOnly(2021, 1, 1);

            PeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.Intersects(intersectPeriod);

            //assert
            Assert.True(result);
        }

        public static IEnumerable<object[]> NonIntersectionDates()
        {
            yield return new object[] { new PeriodDate(new DateOnly(2018, 1, 1), new DateOnly(2019, 1, 1)) };
            yield return new object[] { new PeriodDate(new DateOnly(2022, 1, 1), new DateOnly(2023, 1, 1)) };
        }


        [Theory]
        [MemberData(nameof(NonIntersectionDates))]
        public void WhenPassingNonIntersectionPeriods_ThenReturnFalse(PeriodDate intersectPeriod)
        {
            //arrange
            DateOnly initDate = new DateOnly(2020, 1, 1);
            DateOnly finalDate = new DateOnly(2021, 1, 1);

            PeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.Intersects(intersectPeriod);

            //assert
            Assert.False(result);
        }
    }
}
