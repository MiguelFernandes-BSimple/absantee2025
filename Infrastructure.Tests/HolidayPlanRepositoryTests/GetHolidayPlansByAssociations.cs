using Infrastructure.Repositories;
using Domain.Interfaces;
using Moq;
namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class GetHolidayPlansByAssociations
{


    [Fact]
    public void GetHolidayPlansByAssociations_ReturnsCorrectPlans()
    {
        // Arrange
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();
        Mock<IAssociationProjectCollaborator> mockAssociation = new Mock<IAssociationProjectCollaborator>();

        mockAssociation.Setup(a => a.GetCollaborator()).Returns(doubleCollaborator1.Object);

        Mock<IHolidayPlan> doubleHolidayPlan1 = new Mock<IHolidayPlan>();
        Mock<IHolidayPlan> doubleHolidayPlan2 = new Mock<IHolidayPlan>();

        doubleHolidayPlan1.Setup(h => h.GetCollaborator()).Returns(doubleCollaborator1.Object);
        doubleHolidayPlan2.Setup(h => h.GetCollaborator()).Returns(doubleCollaborator2.Object);

        var holidayPlans = new List<IHolidayPlan>
            {
                doubleHolidayPlan1.Object,
                doubleHolidayPlan2.Object
            };

        var repository = new HolidayPlanRepository(holidayPlans);

        // Act
        var result = repository.GetHolidayPlansByAssociations(mockAssociation.Object);

        // Assert
        Assert.Contains(doubleHolidayPlan1.Object, result);
        Assert.DoesNotContain(doubleHolidayPlan2.Object, result);
        Assert.Single(result);
    }

    [Fact]
    public void GetHolidayPlansByAssociations_WithNoMatchingCollaborator_ReturnsEmpty()
    {
        // Arrange
        var mockCollaborator = new Mock<ICollaborator>();
        var mockAssociation = new Mock<IAssociationProjectCollaborator>();
        mockAssociation.Setup(a => a.GetCollaborator()).Returns(mockCollaborator.Object);

        var mockHolidayPlan = new Mock<IHolidayPlan>();
        mockHolidayPlan.Setup(h => h.GetCollaborator()).Returns(new Mock<ICollaborator>().Object); // Outro colaborador

        var holidayPlans = new List<IHolidayPlan> { mockHolidayPlan.Object };
        var repository = new HolidayPlanRepository(holidayPlans);

        // Act
        var result = repository.GetHolidayPlansByAssociations(mockAssociation.Object);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetHolidayPlansByAssociations_WithNoHolidayPlans_ReturnsEmpty()
    {
        // Arrange
        var mockAssociation = new Mock<IAssociationProjectCollaborator>();
        var repository = new HolidayPlanRepository(new List<IHolidayPlan>());

        // Act
        var result = repository.GetHolidayPlansByAssociations(mockAssociation.Object);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void WhenHolidayPlansByAssociations_ThenReturnsCorrectPlans()
    {
        // Arrange
        var mockCollaborator = new Mock<ICollaborator>();
        var mockAssociation = new Mock<IAssociationProjectCollaborator>();
        mockAssociation.Setup(a => a.GetCollaborator()).Returns(mockCollaborator.Object);

        var mockHolidayPlan1 = new Mock<IHolidayPlan>();
        var mockHolidayPlan2 = new Mock<IHolidayPlan>();

        // Apenas um plano pode pertencer ao colaborador
        mockHolidayPlan1.Setup(h => h.GetCollaborator()).Returns(mockCollaborator.Object);

        mockHolidayPlan2.Setup(h => h.GetCollaborator()).Returns(new Mock<ICollaborator>().Object);

        var holidayPlans = new List<IHolidayPlan>
        {
            mockHolidayPlan1.Object,
            mockHolidayPlan2.Object
        };

        var repository = new HolidayPlanRepository(holidayPlans);

        // Act
        var result = repository.GetHolidayPlansByAssociations(mockAssociation.Object);

        // Assert
        var expectedPlans = new List<IHolidayPlan> { mockHolidayPlan1.Object };
        Assert.Equal(expectedPlans, result.ToList());
    }




    [Fact]
    public void WhenHolidayPlansByAssociations_WithNoMatchingCollaborator_ThenReturnsEmpty()
    {
        // Arrange
        var mockCollaborator = new Mock<ICollaborator>();
        var mockAssociation = new Mock<IAssociationProjectCollaborator>();
        mockAssociation.Setup(a => a.GetCollaborator()).Returns(mockCollaborator.Object);

        var mockHolidayPlan = new Mock<IHolidayPlan>();
        mockHolidayPlan.Setup(h => h.GetCollaborator()).Returns(new Mock<ICollaborator>().Object); // Outro colaborador

        var holidayPlans = new List<IHolidayPlan> { mockHolidayPlan.Object };
        var repository = new HolidayPlanRepository(holidayPlans);

        // Act
        var result = repository.GetHolidayPlansByAssociations(mockAssociation.Object);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void WhenHolidayPlansByAssociations_WithNoHolidayPlans_ThenReturnsEmpty()
    {
        // Arrange
        var mockAssociation = new Mock<IAssociationProjectCollaborator>();
        var repository = new HolidayPlanRepository(new List<IHolidayPlan>());

        // Act
        var result = repository.GetHolidayPlansByAssociations(mockAssociation.Object);

        // Assert
        Assert.Empty(result);
    }
}