using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPeriodTests
{
    public class Intersects
    {
        [Fact]
        public void WhenPassingValidPeriod_ThenReturnsTrue()
        {
            var periodDouble = new Mock<IPeriodDate>();
            periodDouble.Setup(pd => pd.Intersects(It.IsAny<IPeriodDate>())).Returns(true);

            var holidayPeriod = new HolidayPeriod(periodDouble.Object);

            var result = holidayPeriod.Intersects(periodDouble.Object);

            Assert.True(result);
        }

        [Fact]
        public void WhenPassingValidIncorrectPeriod_ThenReturnsFalse()
        {
            var periodDouble = new Mock<IPeriodDate>();
            periodDouble.Setup(pd => pd.Intersects(It.IsAny<IPeriodDate>())).Returns(false);

            var holidayPeriod = new HolidayPeriod(periodDouble.Object);

            var result = holidayPeriod.Intersects(periodDouble.Object);

            Assert.False(result);
        }
    }
}