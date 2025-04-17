using Domain.Interfaces;
using Domain.Models;

namespace Domain.Tests.PeriodDateTimeTests
{
    public class PeriodDateTimeIntersectsTests
    {
        public static IEnumerable<object[]> IntersectionDates()
        {
            yield return new object[] { new PeriodDateTime(new DateTime(2021, 1, 1), new DateTime(2022, 1, 1)) };
            yield return new object[] { new PeriodDateTime(new DateTime(2019, 1, 1), new DateTime(2020, 1, 1)) };
            yield return new object[] { new PeriodDateTime(new DateTime(2020, 1, 3), new DateTime(2020, 1, 3)) };
        }


        [Theory]
        [MemberData(nameof(IntersectionDates))]
        public void WhenPassingIntersectionPeriods_ThenReturnTrue(PeriodDateTime intersectPeriod)
        {
            //arrange
            DateTime initDate = new DateTime(2020, 1, 1);
            DateTime finalDate = new DateTime(2021, 1, 1);

            PeriodDateTime periodDateTime = new PeriodDateTime(initDate, finalDate);

            //act
            var result = periodDateTime.Intersects(intersectPeriod);

            //assert
            Assert.True(result);
        }

        public static IEnumerable<object[]> NonIntersectionDates()
        {
            yield return new object[] { new PeriodDateTime(new DateTime(2018, 1, 1), new DateTime(2019, 1, 1)) };
            yield return new object[] { new PeriodDateTime(new DateTime(2022, 1, 1), new DateTime(2023, 1, 1)) };
        }


        [Theory]
        [MemberData(nameof(NonIntersectionDates))]
        public void WhenPassingNonIntersectionPeriods_ThenReturnFalse(PeriodDateTime intersectPeriod)
        {
            //arrange
            DateTime initDate = new DateTime(2020, 1, 1);
            DateTime finalDate = new DateTime(2021, 1, 1);

            PeriodDateTime periodDate = new PeriodDateTime(initDate, finalDate);

            //act
            var result = periodDate.Intersects(intersectPeriod);

            //assert
            Assert.False(result);
        }
    }
}
