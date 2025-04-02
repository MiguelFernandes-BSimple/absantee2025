using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Tests.PeriodDateTests
{
    public class Duration
    {
        [Fact]
        public void WhenDurationIsCalled_ThenReturnExpectedDurationInDays()
        {
            //arrange
            DateOnly initDate = new DateOnly(1, 1, 2020);
            DateOnly finalDate = new DateOnly(10, 1, 2021);

            IPeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.Duration();
            
            //assert
            Assert.Equal(10, result);
        }
    }
}
