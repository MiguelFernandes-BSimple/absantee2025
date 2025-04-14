using Domain.Interfaces;
using Moq;
using Infrastructure.Repositories;
using Infrastructure.Mapper;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class AddHolidayPlan
{
    [Fact]
    public void WhenAddingCorrectHolidayPlanToRepository_ThenReturnTrue()
    {
        // Arrange 
        // Double for holidayPlan
        Mock<IHolidayPlan> doubleHolidayPlan = new Mock<IHolidayPlan>();

        // Double for holidayPlan Colllaborator
        Mock<ICollaborator> doubleCollab = new Mock<ICollaborator>();

        // Collab has the holidayPlan
        doubleHolidayPlan.Setup(hp => hp.GetCollaboratorId()).Returns(It.IsAny<long>());

        // Instatiate repository
        var context = new Mock<AbsanteeContext>();
        var mapper = new Mock<HolidayPlanMapper>();
        HolidayPlanRepositoryEF repo = new HolidayPlanRepositoryEF(context.Object, mapper.Object);

        // Act
        // add holiday plan to repository
        bool result = repo.AddHolidayPlan(doubleHolidayPlan.Object);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void WhenAddingHolidayPlanWithRepeatedCollaboratorToRepository_ThenReturnFalse()
    {
        // Arrange 
        // Double for holidayPlan
        Mock<IHolidayPlan> doubleHolidayPlan = new Mock<IHolidayPlan>();
        Mock<IHolidayPlan> doubleHolidayPlanToAdd = new Mock<IHolidayPlan>();

        // Double for holidayPlan Colllaborator
        Mock<ICollaborator> doubleCollab = new Mock<ICollaborator>();

        // Collab has the holidayPlan
        doubleHolidayPlan.Setup(hp => hp.GetCollaborator()).Returns(doubleCollab.Object);
        doubleHolidayPlan.Setup(hp => hp.HasCollaborator(It.IsAny<ICollaborator>())).Returns(true);
        doubleHolidayPlanToAdd.Setup(hp => hp.GetCollaborator()).Returns(doubleCollab.Object);
        doubleHolidayPlanToAdd.Setup(hp => hp.HasCollaborator(It.IsAny<ICollaborator>())).Returns(true);

        // Instatiate repository
        var context = new Mock<AbsanteeContext>();
        var mapper = new Mock<HolidayPlanMapper>();
        HolidayPlanRepositoryEF repo = new HolidayPlanRepositoryEF(context.Object, mapper.Object);

        // Act
        // add holiday plan to repository
        bool result = repo.AddHolidayPlan(doubleHolidayPlanToAdd.Object);

        // Assert
        Assert.False(result);

    }
}