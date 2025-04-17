using Moq;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Tests.CollaboratorTests;

public class CollaboratorContractContainsDatesTests
{

    [Fact]
    public void WhenPassingValidDatesToContainsDates_ThenResultIsTrue()
    {
        // Arrange
        Mock<PeriodDateTime> searchPeriodDateTime = new Mock<PeriodDateTime>();
        Mock<PeriodDateTime> periodDateTime = new Mock<PeriodDateTime>();
        periodDateTime.Setup(pd => pd.Contains(searchPeriodDateTime.Object)).Returns(true);

        Collaborator collaborator = new Collaborator(It.IsAny<long>(), periodDateTime.Object);        

        // Act
        bool result = collaborator.ContractContainsDates(searchPeriodDateTime.Object);
        
        // Assert
        Assert.True(result);
    }


    [Fact]
    public void WhenPassingInvalidDatesToContainsDates_ThenResultIsFalse()
    {
        // Arrange
        Mock<PeriodDateTime> periodDateTime = new Mock<PeriodDateTime>();

        Collaborator collaborator = new Collaborator(It.IsAny<long>(), periodDateTime.Object);

        Mock<PeriodDateTime> searchPeriodDateTime = new Mock<PeriodDateTime>();
        periodDateTime.Setup(pd => pd.Contains(searchPeriodDateTime.Object)).Returns(false);

        // Act
        bool result = collaborator.ContractContainsDates(searchPeriodDateTime.Object);
        // Assert
        Assert.False(result);
    }
}