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
        Mock<IPeriodDateTime> searchPeriodDateTime = new Mock<IPeriodDateTime>();
        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
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
        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();

        Collaborator collaborator = new Collaborator(It.IsAny<long>(), periodDateTime.Object);

        Mock<IPeriodDateTime> searchPeriodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(pd => pd.Contains(searchPeriodDateTime.Object)).Returns(false);

        // Act
        bool result = collaborator.ContractContainsDates(searchPeriodDateTime.Object);
        // Assert
        Assert.False(result);
    }
}