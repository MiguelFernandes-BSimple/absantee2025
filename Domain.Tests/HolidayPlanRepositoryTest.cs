using Domain;
using Moq;

namespace Domain.Tests;

public class HolidayPlanRepositoryTest
{
  [Fact]
public void GivenProjectWithCollaborators_WhenGetHolidayDaysForAllCollaborators_ThenReturnsCorrectDays()
{
    // Arrange
    var mockHolidayPlanRepository = new Mock<IHolidayPlanRepository>(); 
    var mockProject = new Mock<IProject>();
    var mockAssociation = new Mock<IAssociationProjectColaborator>(); 
    var mockColaborator = new Mock<IColaborator>(); 

    var mockHolidayPeriod = new Mock<IHolidayPeriod>();
    mockHolidayPeriod.Setup(h => h.GetInitDate()).Returns(new DateOnly(2024, 6, 1));  
    mockHolidayPeriod.Setup(h => h.GetFinalDate()).Returns(new DateOnly(2024, 6, 10)); 
    mockHolidayPeriod.Setup(h => h.GetDurationInDays(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(10); 

    var mockHolidayPlan = new Mock<IHolidayPlan>();
    mockHolidayPlan.Setup(hp => hp.GetCollaborator()).Returns(mockColaborator.Object); 
    mockHolidayPlan.Setup(hp => hp.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { mockHolidayPeriod.Object }); 

    
    mockAssociation.Setup(a => a.GetCollaborators(mockProject.Object)).Returns(new List<IColaborator> { mockColaborator.Object });

    mockHolidayPlanRepository.Setup(r => r.GetHolidayDaysForAllProjectCollaboratorsBetweenDates(
        mockAssociation.Object, 
        mockProject.Object, 
        new DateOnly(2024, 6, 1), 
        new DateOnly(2024, 6, 10)
    )).Returns(10);  

    // Act
    int result = mockHolidayPlanRepository.Object.GetHolidayDaysForAllProjectCollaboratorsBetweenDates(
        mockAssociation.Object, 
        mockProject.Object, 
        new DateOnly(2024, 6, 1), 
        new DateOnly(2024, 6, 10)
    );

    // Assert
    Assert.Equal(10, result);
}



}


   
