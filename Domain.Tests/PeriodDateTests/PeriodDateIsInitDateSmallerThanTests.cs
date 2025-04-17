using Domain.Interfaces;
using Domain.Models;

namespace Domain.Tests.PeriodDateTests
{
    public class PeriodDateIsInitDateSmallerThanTests
    {

        [Fact]
        public void WhenPassingDatesBiggerThanInitDate_ThenReturnFalse()
        {
            //arrange
            DateOnly initDate = new DateOnly(2020, 1, 1);
            DateOnly finalDate = new DateOnly(2021, 1, 1);
            DateOnly date = new DateOnly(2022, 1, 1);

            PeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.IsInitDateSmallerThan(date);

            //assert
            Assert.True(result);
        }

        public static IEnumerable<object[]> DatesSmallerThanInitDate()
        {
            yield return new object[] { new DateOnly(2019, 1, 1) };
            yield return new object[] { new DateOnly(2020, 1, 1) };
        }


        [Theory]
        [MemberData(nameof(DatesSmallerThanInitDate))]
        public void WhenPassingDatesSmallerThanInitDate_ThenReturnFalse(DateOnly date)
        {
            //arrange
            DateOnly initDate = new DateOnly(2020, 1, 1);
            DateOnly finalDate = new DateOnly(2021, 1, 1);

            PeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.IsInitDateSmallerThan(date);

            //assert
            Assert.False(result);
        }
    }
}
