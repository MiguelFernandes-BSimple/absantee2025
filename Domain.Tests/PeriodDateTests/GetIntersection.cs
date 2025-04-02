using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Tests.PeriodDateTests
{
    public class GetIntersection
    {
        public static IEnumerable<object[]> PeriodsThatIntersect()
        {
            yield return new object[] { 
                new PeriodDate(new DateOnly(1, 1, 2021), new DateOnly(1, 1, 2022)), 
                new PeriodDate(new DateOnly(1, 1, 2021), new DateOnly(1, 1, 2021)) 
            };
            yield return new object[] {
                new PeriodDate(new DateOnly(1, 1, 2019), new DateOnly(1, 1, 2020)),
                new PeriodDate(new DateOnly(1, 1, 2020), new DateOnly(1, 1, 2020))
            };
        }


        [Theory]
        [MemberData(nameof(PeriodsThatIntersect))]
        public void WhenPassingPeriod_ThenReturnIntersection(IPeriodDate periodDate2, IPeriodDate intersectionPeriod)
        {
            //arrange
            DateOnly initDate = new DateOnly(1, 1, 2020);
            DateOnly finalDate = new DateOnly(1, 1, 2021);

            IPeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.GetIntersection(periodDate2);

            //assert
            Assert.Equal(intersectionPeriod, result);
        }

        [Fact]
        public void WhenPeriodsDontIntersect_ThenReturnNull()
        {
            //arrange
            DateOnly initDate = new DateOnly(1, 1, 2020);
            DateOnly finalDate = new DateOnly(1, 1, 2021);

            IPeriodDate periodDate = new PeriodDate(initDate, finalDate);

            DateOnly initDate2 = new DateOnly(1, 1, 2018);
            DateOnly finalDate2 = new DateOnly(1, 1, 2019);

            IPeriodDate periodDate2 = new PeriodDate(initDate2, finalDate2);

            //act
            var result = periodDate.GetIntersection(periodDate2);

            //assert
            Assert.Null(result);
        }
    }
}
