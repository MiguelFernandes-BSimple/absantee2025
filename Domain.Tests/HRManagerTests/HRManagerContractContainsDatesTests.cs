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
    public class HRManagerContractContainsDatesTests
    {
        [Fact]
        public void WhenPeriodDateTimeInsideContract_ThenReturnsTrue()
        {
            //arrange
            Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
            periodDateTime.Setup(p => p.Contains(It.IsAny<IPeriodDateTime>())).Returns(true);

            var hrManager = new HRManager(It.IsAny<long>(), periodDateTime.Object); 
            //act
            var result = hrManager.ContractContainsDates(It.IsAny<IPeriodDateTime>());

            //assert
            Assert.True(result);
        }

        [Fact]
        public void WhenPeriodDateTimeNotInsideContract_ThenReturnsFalse()
        {
            //arrange
            Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
            periodDateTime.Setup(p => p.Contains(It.IsAny<IPeriodDateTime>())).Returns(false);

            var hrManager = new HRManager(It.IsAny<long>(), periodDateTime.Object);
            //act
            var result = hrManager.ContractContainsDates(It.IsAny<IPeriodDateTime>());

            //assert
            Assert.False(result);
        }
    }
}
