using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HRManagerTests;

public class Constructor
{
    [Fact]
    public void WhenCreatingHRManagerWithValidPeriod_ThenHRManagerIsCreatedCorrectly()
    {

        long id = 1;

        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        //act
        new HRManager(id, periodDateTime.Object);
        //assert
    }

    [Fact]
    public void WhenCreatingHRManagerWithValidInitDate_ThenHRManagerIsCreatedCorrectly()
    {
        long id = 2;
        //arrange
        Mock<IUser> user = new Mock<IUser>();
        user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
        user.Setup(u => u.IsDeactivated()).Returns(false);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateTime>());

        //act
        new HRManager(id, periodDateTime.Object);
        //assert
    }
}
