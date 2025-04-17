using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Tests.PeriodDateTests
{
    public class PeriodDateGetIntersectionTests
    {
        public static IEnumerable<object[]> PeriodsThatIntersect()
        {
            yield return new object[] {
                new PeriodDate(new DateOnly(2021, 1, 1), new DateOnly(2022, 1, 1)),
                new PeriodDate(new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 1))
            };
            yield return new object[] {
                new PeriodDate(new DateOnly(2019, 1, 1), new DateOnly(2020, 1, 1)),
                new PeriodDate(new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 1))
            };
        }


        [Theory]
        [MemberData(nameof(PeriodsThatIntersect))]
        public void WhenPassingPeriod_ThenReturnIntersection(PeriodDate periodDate2, PeriodDate intersectionPeriod)
        {
            //arrange
            DateOnly initDate = new DateOnly(2020, 1, 1);
            DateOnly finalDate = new DateOnly(2021, 1, 1);

            PeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.GetIntersection(periodDate2);

            //assert
            Assert.NotNull(result);
            Assert.Equal(intersectionPeriod.GetInitDate(), result.GetInitDate());
            Assert.Equal(intersectionPeriod.GetFinalDate(), result.GetFinalDate());
        }

        [Fact]
        public void WhenPeriodsDontIntersect_ThenReturnNull()
        {
            //arrange
            DateOnly initDate = new DateOnly(2020, 1, 1);
            DateOnly finalDate = new DateOnly(2021, 1, 1);

            PeriodDate periodDate = new PeriodDate(initDate, finalDate);

            DateOnly initDate2 = new DateOnly(2018, 1, 1);
            DateOnly finalDate2 = new DateOnly(2019, 1, 1);

            PeriodDate periodDate2 = new PeriodDate(initDate2, finalDate2);

            //act
            var result = periodDate.GetIntersection(periodDate2);

            //assert
            Assert.Null(result);
        }
    }
}
