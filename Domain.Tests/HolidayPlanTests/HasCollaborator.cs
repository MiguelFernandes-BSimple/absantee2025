using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPlanTests;

public class HasCollaborator
{
    [Fact]
    public void WhenSameCollaborator_ReturnsTrue()
    {
        //Arrange
        var collaboratorId = 1;
        var holidayPlan = new HolidayPlan(collaboratorId);

        // Act
        var result = holidayPlan.HasCollaborator(collaboratorId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void WhenDifferentCollaborator_ReturnsFalse()
    {
        // Arrange
        var collaboratorId = 1;
        var holidayPlan = new HolidayPlan(collaboratorId);

        // Act
        var result = holidayPlan.HasCollaborator(It.IsAny<long>());

        // Assert
        Assert.False(result);
    }
}