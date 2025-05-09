using Moq;
using Domain.Models;

namespace Domain.Tests.CollaboratorTests;

public class CollaboratorContractContainsDatesTests
{

    [Fact]
    public void WhenPassingValidDatesToContainsDates_ThenResultIsTrue()
    {
        // Arrange
        PeriodDateTime collabPeriod = new PeriodDateTime(DateTime.Now, DateTime.Now.AddMonths(2));
        PeriodDateTime inPeriod = new PeriodDateTime(DateTime.Now.AddDays(1), DateTime.Now.AddDays(10));

        Collaborator collaborator = new Collaborator(It.IsAny<Guid>(), collabPeriod);

        // Act
        bool result = collaborator.ContractContainsDates(inPeriod);

        // Assert
        Assert.True(result);
    }


    [Fact]
    public void WhenPassingInvalidDatesToContainsDates_ThenResultIsFalse()
    {
        // Arrange
        PeriodDateTime collabPeriod = new PeriodDateTime(DateTime.Now, DateTime.Now.AddMonths(2));
        PeriodDateTime inPeriod = new PeriodDateTime(DateTime.Now.AddMonths(1), DateTime.Now.AddMonths(3));

        Collaborator collaborator = new Collaborator(It.IsAny<Guid>(), collabPeriod);

        // Act
        bool result = collaborator.ContractContainsDates(inPeriod);

        // Assert
        Assert.False(result);
    }
}