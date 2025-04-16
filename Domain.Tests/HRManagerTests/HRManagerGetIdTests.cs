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
    public class HRManagerGetIdTests
    {
        [Fact]
        public void WhenGettingId_ThenReturnsId()
        {
            //arrange
            var id = 1;
            var hrManager = new HRManager(id, It.IsAny<long>(), It.IsAny<IPeriodDateTime>());
            //act
            var hrId = hrManager.GetId();
            
            //assert
            Assert.Equal(id, hrId);
        }
    }
}
