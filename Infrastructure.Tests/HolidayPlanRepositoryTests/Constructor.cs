using Infrastructure.Repositories;
using Domain.Interfaces;
using Moq;
namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class Constructor
{

    [Fact]
    public void WhenNotPassingAnyArguments_ThenObjectIsCreated()
    {
        //Arrange

        //Act
        new HolidayPlanRepository();

        //Assert
    }

    [Fact]
    public void WhenPassingCorrectHolidayPlansList_ThenObjectIsCreated()
    {
        // Arrange 
        // HolidayPlan doubles - stubs
        Mock<IHolidayPlan> doubleHolidayPlan1 = new Mock<IHolidayPlan>();
        Mock<IHolidayPlan> doubleHolidayPlan2 = new Mock<IHolidayPlan>();
        Mock<IHolidayPlan> doubleHolidayPlan3 = new Mock<IHolidayPlan>();

        //Collaborator doubles for constructor verification
        Mock<ICollaborator> doubleColab1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleColab2 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleColab3 = new Mock<ICollaborator>();

        // Create HolidayPlan List
        List<IHolidayPlan> holidayPlansList =
            new List<IHolidayPlan> { doubleHolidayPlan1.Object, doubleHolidayPlan2.Object, doubleHolidayPlan3.Object };

        // To be insertable, all holidayPlans must have distinct collaborators
        // no holidayPlan iss associated with the same collaborator
        doubleHolidayPlan1.Setup(hp1 => hp1.GetCollaborator()).Returns(doubleColab1.Object);
        doubleHolidayPlan2.Setup(hp2 => hp2.GetCollaborator()).Returns(doubleColab2.Object);
        doubleHolidayPlan3.Setup(hp3 => hp3.GetCollaborator()).Returns(doubleColab3.Object);

        // Act
        new HolidayPlanRepository(holidayPlansList);

        // Assert 
    }

    /**
    * Test for constructor that receives a list of holidayPlans that is not valid
    * -> Should throw exception
    *
    * One element being invalid is enough for the whole list to be invalid too 
    */


    [Fact]
    public void WhenPassingIncorrectHolidayPlansList_ThenShouldThrowException()
    {
        // Arrange 
        // HolidayPlan doubles - stubs
        Mock<IHolidayPlan> doubleHolidayPlan1 = new Mock<IHolidayPlan>();
        Mock<IHolidayPlan> doubleHolidayPlan2 = new Mock<IHolidayPlan>();
        Mock<IHolidayPlan> doubleHolidayPlan3 = new Mock<IHolidayPlan>();

        //Collaborator doubles for constructor verification
        Mock<ICollaborator> doubleColab1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleColab2 = new Mock<ICollaborator>();

        // Create HolidayPlan List
        List<IHolidayPlan> holidayPlansList =
            new List<IHolidayPlan> { doubleHolidayPlan1.Object, doubleHolidayPlan2.Object, doubleHolidayPlan3.Object };

        // To be insertable, all holidayPlans must have distinct collaborators
        // no holidayPlan iss associated with the same collaborator
        doubleHolidayPlan1.Setup(hp1 => hp1.GetCollaborator()).Returns(doubleColab1.Object);
        doubleHolidayPlan1.Setup(hp => hp.HasCollaborator(It.IsAny<ICollaborator>())).Returns(true);
        doubleHolidayPlan2.Setup(hp2 => hp2.GetCollaborator()).Returns(doubleColab2.Object);
        doubleHolidayPlan2.Setup(hp => hp.HasCollaborator(It.IsAny<ICollaborator>())).Returns(true);
        doubleHolidayPlan3.Setup(hp3 => hp3.GetCollaborator()).Returns(doubleColab2.Object);
        doubleHolidayPlan3.Setup(hp => hp.HasCollaborator(It.IsAny<ICollaborator>())).Returns(true);

        // Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new HolidayPlanRepository(holidayPlansList));

        Assert.Equal("Arguments are not valid!", exception.Message);
    }

    [Fact]
    public void WhenPassingCorrectValues_ThenObjectIsCreated()
    {
        //arrange
        Mock<IHolidayPlan> holidayPlanDouble = new Mock<IHolidayPlan>();
        List<IHolidayPlan> holidayPlans = new List<IHolidayPlan> { holidayPlanDouble.Object };

        //act
        new HolidayPlanRepository(holidayPlans);

        //assert
    }


}