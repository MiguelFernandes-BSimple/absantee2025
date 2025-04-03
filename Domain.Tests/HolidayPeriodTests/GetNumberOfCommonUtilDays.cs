using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPeriodTests
{
    public class GetNumberOfCommonUtilDays
    {
        [Fact]
        public void WhenPassingValidData_ThenRetunsNumberOfCommonUtilDays()
        {
            // arrange
            var expectedDays = 3;

            var initDate = new DateOnly(2025, 4, 14);
            var finalDate = new DateOnly(2025, 4, 16);

            var periodDouble = new Mock<IPeriodDate>();
            periodDouble.Setup(pd => pd.GetInitDate()).Returns(initDate);
            periodDouble.Setup(pd => pd.GetFinalDate()).Returns(finalDate);

            var holidayPeriod = new HolidayPeriod(periodDouble.Object);

            // act
            var result = holidayPeriod.GetNumberOfCommonUtilDays();

            // assert
            Assert.Equal(result, expectedDays);
        }

        [Fact]
        public void WhenPassingValidDataWithWeekEnd_ThenRetunsNumberOfCommonUtilDays()
        {
            // arrange
            var expectedDays = 11;

            var initDate = new DateOnly(2025, 4, 1);
            var finalDate = new DateOnly(2025, 4, 15);

            var periodDouble = new Mock<IPeriodDate>();
            periodDouble.Setup(pd => pd.GetInitDate()).Returns(initDate);
            periodDouble.Setup(pd => pd.GetFinalDate()).Returns(finalDate);

            var holidayPeriod = new HolidayPeriod(periodDouble.Object);

            // act
            var result = holidayPeriod.GetNumberOfCommonUtilDays();

            // assert
            Assert.Equal(result, expectedDays);
        }

    }
}