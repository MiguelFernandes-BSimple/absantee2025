using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HRManagerTests
{
    public class HRManagerGetPeriodDateTimeTests
    {
        [Fact]
        public void WhenGettingPeriodDateTime_ThenReturnsPeriodDateTime()
        {
            //arrange
            var periodDateTime = new Mock<PeriodDateTime>();
            var hrManager = new HRManager(It.IsAny<long>(), It.IsAny<long>(), periodDateTime.Object);
            //act
            var hrPeriodDateTime = hrManager.GetPeriodDateTime();

            //assert
            Assert.Equal(periodDateTime.Object, hrPeriodDateTime);
        }
    }
}
