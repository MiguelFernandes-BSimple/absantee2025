using Domain.Interfaces;
using Domain.Models;

namespace Domain.Tests.PeriodDateTimeTests
{
    public class PeriodDateTimeIsFinalDateSmallerThanTests
    {

        [Fact]
        public void WhenPassingDatesBiggerThanFinalDate_ThenReturnFalse()
        {
            //Arrange
            DateTime initDate = new DateTime(2020, 1, 1);
            DateTime finalDate = new DateTime(2021, 1, 1);
            DateTime date = new DateTime(2022, 1, 1);

            PeriodDateTime periodDateTime = new PeriodDateTime(initDate, finalDate);

            //Act
            var result = periodDateTime.IsFinalDateSmallerThan(date);

            //Assert
            Assert.True(result);
        }

        public static IEnumerable<object[]> DatesSmallerThanFinalDate()
        {
            yield return new object[] { new DateTime(2020, 1, 1) };
            yield return new object[] { new DateTime(2021, 1, 1) };
        }


        [Theory]
        [MemberData(nameof(DatesSmallerThanFinalDate))]
        public void WhenPassingDatesSmallerThanFinalDate_ThenReturnFalse(DateTime date)
        {
            //Arrange
            DateTime initDate = new DateTime(2020, 1, 1);
            DateTime finalDate = new DateTime(2021, 1, 1);

            PeriodDateTime periodDateTime = new PeriodDateTime(initDate, finalDate);

            //Act
            var result = periodDateTime.IsFinalDateSmallerThan(date);

            //Assert
            Assert.False(result);
        }
    }
}