using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.HolidayPeriodTests;
public class HolidayPeriodFactoryTests
{
    [Fact]
    public void WhenCreatingWithValidData_ThenObjectIsInstantiated()
    {
        // Arrange
        var start = new DateOnly(2024, 4, 10);
        var end = new DateOnly(2024, 4, 15);
        var periodDate = new PeriodDate(start, end);

        var holidayPlanMock = new Mock<IHolidayPlan>();

        var holidayPlanRepo = new Mock<IHolidayPlanRepository>();
        holidayPlanRepo.Setup(hp => hp.CanInsertHolidayPeriod(It.IsAny<long>(), It.IsAny<IHolidayPeriod>())).Returns(true);
        holidayPlanRepo.Setup(hp => hp.GetById(It.IsAny<long>())).Returns(holidayPlanMock.Object);

        var collaboratorMock = new Mock<ICollaborator>();
        collaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<PeriodDateTime>())).Returns(true);

        var collaboratorRepo = new Mock<ICollaboratorRepository>();
        collaboratorRepo.Setup(c => c.GetById(It.IsAny<long>())).Returns(collaboratorMock.Object);

        var factory = new HolidayPeriodFactory(holidayPlanRepo.Object, collaboratorRepo.Object);

        // Act
        var result = factory.Create(123L, periodDate);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenCreatingWithExistingHolidayPeriod_ThenThrowsException()
    {
        var start = new DateOnly(2024, 4, 10);
        var end = new DateOnly(2024, 4, 15);
        var periodDate = new PeriodDate(start, end);

        var holidayPlanMock = new Mock<IHolidayPlan>();

        var holidayPlanRepo = new Mock<IHolidayPlanRepository>();
        holidayPlanRepo.Setup(hp => hp.CanInsertHolidayPeriod(It.IsAny<long>(), It.IsAny<IHolidayPeriod>())).Returns(false);
        holidayPlanRepo.Setup(hp => hp.GetById(It.IsAny<long>())).Returns(holidayPlanMock.Object);

        var collaboratorMock = new Mock<ICollaborator>();
        collaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<PeriodDateTime>())).Returns(true);

        var collaboratorRepo = new Mock<ICollaboratorRepository>();
        collaboratorRepo.Setup(c => c.GetById(It.IsAny<long>())).Returns(collaboratorMock.Object);


        var holidayPeriodFactory = new HolidayPeriodFactory(holidayPlanRepo.Object, collaboratorRepo.Object);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                holidayPeriodFactory.Create(It.IsAny<long>(), periodDate)

        );
        Assert.Equal("Holiday Period already exists for this Holiday Plan.", exception.Message);
    }

    [Fact]
    public void WhenCreatingWithoutHolidayPlan_ThenThrowsException()
    {
        // Arrange 
        var start = new DateOnly(2024, 4, 10);
        var end = new DateOnly(2024, 4, 15);
        var periodDate = new PeriodDate(start, end);

        var holidayPlanMock = new Mock<IHolidayPlan>();

        var holidayPlanRepo = new Mock<IHolidayPlanRepository>();
        holidayPlanRepo.Setup(hp => hp.CanInsertHolidayPeriod(It.IsAny<long>(), It.IsAny<IHolidayPeriod>())).Returns(true);
        holidayPlanRepo.Setup(hp => hp.GetById(It.IsAny<long>())).Returns((HolidayPlan?)null);

        var collaboratorMock = new Mock<ICollaborator>();
        collaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<PeriodDateTime>())).Returns(true);

        var collaboratorRepo = new Mock<ICollaboratorRepository>();
        collaboratorRepo.Setup(c => c.GetById(It.IsAny<long>())).Returns(collaboratorMock.Object);


        var holidayPeriodFactory = new HolidayPeriodFactory(holidayPlanRepo.Object, collaboratorRepo.Object);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                holidayPeriodFactory.Create(It.IsAny<long>(), periodDate)

        );
        Assert.Equal("Holiday Plan doesn't exist.", exception.Message);
    }

    [Fact]
    public void WhenCreatingWithoutCollaborator_ThenThrowsException()
    {
        // Arrange 
        var start = new DateOnly(2024, 4, 10);
        var end = new DateOnly(2024, 4, 15);
        var periodDate = new PeriodDate(start, end);

        var holidayPlanMock = new Mock<IHolidayPlan>();

        var holidayPlanRepo = new Mock<IHolidayPlanRepository>();
        holidayPlanRepo.Setup(hp => hp.CanInsertHolidayPeriod(It.IsAny<long>(), It.IsAny<IHolidayPeriod>())).Returns(true);
        holidayPlanRepo.Setup(hp => hp.GetById(It.IsAny<long>())).Returns(holidayPlanMock.Object);

        var collaboratorMock = new Mock<ICollaborator>();
        collaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<PeriodDateTime>())).Returns(true);

        var collaboratorRepo = new Mock<ICollaboratorRepository>();
        collaboratorRepo.Setup(c => c.GetById(It.IsAny<long>())).Returns((Collaborator?)null);


        var holidayPeriodFactory = new HolidayPeriodFactory(holidayPlanRepo.Object, collaboratorRepo.Object);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                holidayPeriodFactory.Create(It.IsAny<long>(), periodDate)

        );
        Assert.Equal("Collaborator doesn't exist.", exception.Message);
    }

    [Fact]
    public void WhenCreatingWithOutOfBoundsCollaboratorContract_ThenThrowsException()
    {
        // Arrange 
        var start = new DateOnly(2024, 4, 10);
        var end = new DateOnly(2024, 4, 15);
        var periodDate = new PeriodDate(start, end);
        var holidayPlanMock = new Mock<IHolidayPlan>();

        var holidayPlanRepo = new Mock<IHolidayPlanRepository>();
        holidayPlanRepo.Setup(hp => hp.CanInsertHolidayPeriod(It.IsAny<long>(), It.IsAny<IHolidayPeriod>())).Returns(true);
        holidayPlanRepo.Setup(hp => hp.GetById(It.IsAny<long>())).Returns(holidayPlanMock.Object);

        var collaboratorMock = new Mock<ICollaborator>();
        collaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<PeriodDateTime>())).Returns(false);

        var collaboratorRepo = new Mock<ICollaboratorRepository>();
        collaboratorRepo.Setup(c => c.GetById(It.IsAny<long>())).Returns(collaboratorMock.Object);


        var holidayPeriodFactory = new HolidayPeriodFactory(holidayPlanRepo.Object, collaboratorRepo.Object);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                holidayPeriodFactory.Create(It.IsAny<long>(), periodDate)

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
        Assert.NotNull(result);
    }
}

