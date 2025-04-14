using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.HolidayPeriodTests;
public class Factory
{
    [Fact]
    public void WhenCreatingWithValidData_ThenObjectIsInstantiated()
    {
        // Arrange 
        var periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.GetInitDate()).Returns(It.IsAny<DateOnly>());
        periodDateMock.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateOnly>());

        var holidayPlanMock = new Mock<IHolidayPlan>();

        var holidayPlanRepo = new Mock<IHolidayPlanRepository>();
        holidayPlanRepo.Setup(hp => hp.HolidayPeriodExists(It.IsAny<long>(), periodDateMock.Object)).Returns(false);
        holidayPlanRepo.Setup(hp => hp.GetById(It.IsAny<long>())).Returns(holidayPlanMock.Object);

        var collaboratorMock = new Mock<ICollaborator>();
        collaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        var collaboratorRepo = new Mock<ICollaboratorRepository>();
        collaboratorRepo.Setup(c => c.GetById(It.IsAny<long>())).Returns(collaboratorMock.Object);


        var holidayPeriodFactory = new HolidayPeriodFactory(holidayPlanRepo.Object, collaboratorRepo.Object);

        // Act
        holidayPeriodFactory.Create(It.IsAny<long>(), periodDateMock.Object);

        // Assert

    }

    [Fact]
    public void WhenCreatingWithExistingHolidayPeriod_ThenThrowsException()
    {
        // Arrange 
        var periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.GetInitDate()).Returns(It.IsAny<DateOnly>());
        periodDateMock.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateOnly>());

        var holidayPlanMock = new Mock<IHolidayPlan>();

        var holidayPlanRepo = new Mock<IHolidayPlanRepository>();
        holidayPlanRepo.Setup(hp => hp.HolidayPeriodExists(It.IsAny<long>(), periodDateMock.Object)).Returns(true);
        holidayPlanRepo.Setup(hp => hp.GetById(It.IsAny<long>())).Returns(holidayPlanMock.Object);

        var collaboratorMock = new Mock<ICollaborator>();
        collaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        var collaboratorRepo = new Mock<ICollaboratorRepository>();
        collaboratorRepo.Setup(c => c.GetById(It.IsAny<long>())).Returns(collaboratorMock.Object);


        var holidayPeriodFactory = new HolidayPeriodFactory(holidayPlanRepo.Object, collaboratorRepo.Object);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                holidayPeriodFactory.Create(It.IsAny<long>(), periodDateMock.Object)

        );
        Assert.Equal("Holiday Period already exists for this Holiday Plan.", exception.Message);
    }

    [Fact]
    public void WhenCreatingWithoutHolidayPlan_ThenThrowsException()
    {
        // Arrange 
        var periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.GetInitDate()).Returns(It.IsAny<DateOnly>());
        periodDateMock.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateOnly>());

        var holidayPlanMock = new Mock<IHolidayPlan>();

        var holidayPlanRepo = new Mock<IHolidayPlanRepository>();
        holidayPlanRepo.Setup(hp => hp.HolidayPeriodExists(It.IsAny<long>(), periodDateMock.Object)).Returns(false);
        holidayPlanRepo.Setup(hp => hp.GetById(It.IsAny<long>())).Returns((HolidayPlan?)null);

        var collaboratorMock = new Mock<ICollaborator>();
        collaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        var collaboratorRepo = new Mock<ICollaboratorRepository>();
        collaboratorRepo.Setup(c => c.GetById(It.IsAny<long>())).Returns(collaboratorMock.Object);


        var holidayPeriodFactory = new HolidayPeriodFactory(holidayPlanRepo.Object, collaboratorRepo.Object);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                holidayPeriodFactory.Create(It.IsAny<long>(), periodDateMock.Object)

        );
        Assert.Equal("Holiday Plan doesn't exist.", exception.Message);
    }

    [Fact]
    public void WhenCreatingWithoutCollaborator_ThenThrowsException()
    {
        // Arrange 
        var periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.GetInitDate()).Returns(It.IsAny<DateOnly>());
        periodDateMock.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateOnly>());

        var holidayPlanMock = new Mock<IHolidayPlan>();

        var holidayPlanRepo = new Mock<IHolidayPlanRepository>();
        holidayPlanRepo.Setup(hp => hp.HolidayPeriodExists(It.IsAny<long>(), periodDateMock.Object)).Returns(false);
        holidayPlanRepo.Setup(hp => hp.GetById(It.IsAny<long>())).Returns(holidayPlanMock.Object);

        var collaboratorMock = new Mock<ICollaborator>();
        collaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        var collaboratorRepo = new Mock<ICollaboratorRepository>();
        collaboratorRepo.Setup(c => c.GetById(It.IsAny<long>())).Returns((Collaborator?)null);


        var holidayPeriodFactory = new HolidayPeriodFactory(holidayPlanRepo.Object, collaboratorRepo.Object);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                holidayPeriodFactory.Create(It.IsAny<long>(), periodDateMock.Object)

        );
        Assert.Equal("Collaborator doesn't exist.", exception.Message);
    }

    [Fact]
    public void WhenCreatingWithOutOfBoundsCollaboratorContract_ThenThrowsException()
    {
        // Arrange 
        var periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.GetInitDate()).Returns(It.IsAny<DateOnly>());
        periodDateMock.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateOnly>());

        var holidayPlanMock = new Mock<IHolidayPlan>();

        var holidayPlanRepo = new Mock<IHolidayPlanRepository>();
        holidayPlanRepo.Setup(hp => hp.HolidayPeriodExists(It.IsAny<long>(), periodDateMock.Object)).Returns(false);
        holidayPlanRepo.Setup(hp => hp.GetById(It.IsAny<long>())).Returns(holidayPlanMock.Object);

        var collaboratorMock = new Mock<ICollaborator>();
        collaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(false);

        var collaboratorRepo = new Mock<ICollaboratorRepository>();
        collaboratorRepo.Setup(c => c.GetById(It.IsAny<long>())).Returns(collaboratorMock.Object);


        var holidayPeriodFactory = new HolidayPeriodFactory(holidayPlanRepo.Object, collaboratorRepo.Object);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                holidayPeriodFactory.Create(It.IsAny<long>(), periodDateMock.Object)

        );
        Assert.Equal("Collaborator's contract out of bounds.", exception.Message);
    }



    [Fact]
    public void WhenCreatingHolidayPeriodWithIHolidayPeriodVisitor_ThenHolidayPeriodIsCreatedCorrectly()
    {
        //arrange
        var visitor = new Mock<IHolidayPeriodVisitor>();
        visitor.Setup(v => v.Id).Returns(It.IsAny<long>());
        visitor.Setup(v => v.PeriodDate).Returns(It.IsAny<PeriodDate>());

        var holidayPlanRepo = new Mock<IHolidayPlanRepository>();
        var collaboratorRepo = new Mock<ICollaboratorRepository>();
        var holidayPeriodFactory = new HolidayPeriodFactory(holidayPlanRepo.Object, collaboratorRepo.Object);

        //act
        var result = holidayPeriodFactory.Create(visitor.Object);

        //assert
    }
}

