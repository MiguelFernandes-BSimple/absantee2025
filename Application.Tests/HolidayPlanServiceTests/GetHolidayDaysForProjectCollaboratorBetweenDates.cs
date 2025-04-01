using Domain.Interfaces;
using Infrastructure.Interfaces;
using Application.Services;
using Moq;
namespace Application.Tests.HolidayPlanServiceTests;

public class GetHolidayDaysForProjectCollaboratorBetweenDates
{
    [Fact]
    public void WhenNoAssociationForCollaborator_ThenThrowException()
    {
        // Arrange
        var projectMock = new Mock<IProject>();
        var collaboratorMock = new Mock<ICollaborator>();
        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        associationRepoMock
            .Setup(a => a.FindByProjectAndCollaborator(It.IsAny<IProject>(), It.IsAny<ICollaborator>()))
            .Returns((IAssociationProjectCollaborator?)null);

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, Mock.Of<IHolidayPlanRepository>());

        // Act & Assert
        var exception = Assert.Throws<Exception>(() =>
            holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(
                projectMock.Object,
                collaboratorMock.Object,
                new DateOnly(2025, 6, 1),
                new DateOnly(2025, 6, 10)
            )
        );
        Assert.Equal("No association found for the project and collaborator", exception.Message);
    }


    public static IEnumerable<object[]> GetHolidayDaysForProjectCollaboratorBetweenDatesData()
    {
        yield return new object[]
        {
             new DateOnly(2025, 7, 15),
             new DateOnly(2025, 8, 1),
             new DateOnly(2025, 7, 20),
             new DateOnly(2025, 7, 25),
             6,
        };

        yield return new object[]
        {
             new DateOnly(2025, 7, 15),
             new DateOnly(2025, 8, 1),
             new DateOnly(2025, 7, 5),
             new DateOnly(2025, 7, 20),
             6,
        };

        yield return new object[]
        {
             new DateOnly(2025, 7, 15),
             new DateOnly(2025, 8, 1),
             new DateOnly(2025, 7, 25),
             new DateOnly(2025, 8, 1),
             7,
        };

        yield return new object[]
        {
             new DateOnly(2025, 7, 15),
             new DateOnly(2025, 8, 1),
             new DateOnly(2025, 9, 1),
             new DateOnly(2025, 9, 10),
             0,
        };

        yield return new object[]
        {
             new DateOnly(2025, 7, 15),
             new DateOnly(2025, 7, 15),
             new DateOnly(2025, 7, 15),
             new DateOnly(2025, 7, 15),
             1,
        };
    }

    [Theory]
    [MemberData(nameof(GetHolidayDaysForProjectCollaboratorBetweenDatesData))]
    public void WhenGettingHolidayDaysForProjectCollaboratorBetweenDates_ThenReturnsCorrectHolidayDays(
        DateOnly initDate,
        DateOnly endDate,
        DateOnly holidayInitDate,
        DateOnly holidayEndDate,
        int expectedHolidayDays
    )
    {
        // Arrange
        var collaboratorMock = new Mock<ICollaborator>();
        var projectMock = new Mock<IProject>();

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        associationRepoMock
            .Setup(a => a.FindByProjectAndCollaborator(projectMock.Object, collaboratorMock.Object))
            .Returns(associationMock.Object);

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        var holidarPeriodList = new List<IHolidayPeriod>(){
                holidayPeriodMock.Object
            };

        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindHolidayPeriodsByCollaborator(collaboratorMock.Object))
                        .Returns(holidarPeriodList);


        holidayPeriodMock.Setup(hp => hp.GetInitDate()).Returns(holidayInitDate);
        holidayPeriodMock.Setup(hp => hp.GetFinalDate()).Returns(holidayEndDate);
        holidayPeriodMock
            .Setup(hp =>
                hp.GetNumberOfCommonUtilDaysBetweenPeriods(
                    It.IsAny<DateOnly>(),
                    It.IsAny<DateOnly>()
                )
            )
                .Returns(expectedHolidayDays);


        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);

        // Act
        var result = holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(
            projectMock.Object,
            collaboratorMock.Object,
            initDate,
            endDate
        );

        // Assert
        Assert.Equal(expectedHolidayDays, result);
    }
    [Theory]
    [InlineData("2025-08-01", "2025-07-15")]  // initDate > endDate
    public void WhenInitDateIsGreaterThanEndDate_ThenReturnsZero(string initDateStr, string endDateStr)
    {
        DateOnly initDate = DateOnly.Parse(initDateStr);
        DateOnly endDate = DateOnly.Parse(endDateStr);
        // Arrange
        var collaboratorMock = new Mock<ICollaborator>();
        var projectMock = new Mock<IProject>();

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        associationRepoMock
            .Setup(a => a.FindByProjectAndCollaborator(projectMock.Object, collaboratorMock.Object))
            .Returns(associationMock.Object);

        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindHolidayPeriodsByCollaborator(collaboratorMock.Object))
                        .Returns(new List<IHolidayPeriod>());

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);

        // Act
        var result = holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(
            projectMock.Object,
            collaboratorMock.Object,
            initDate,
            endDate
        );

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void GetHolidayDaysForProjectCollaboratorBetweenDates_ReturnsCorrectDays()
    {
        // Arrange
        var mockProject = new Mock<IProject>();
        var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        var mockHolidayPlanRepo = new Mock<IHolidayPlanRepository>();

        var mockAssociation = new Mock<IAssociationProjectCollaborator>();
        var mockHolidayPlan = new Mock<IHolidayPlan>();
        var mockHolidayPeriod = new Mock<IHolidayPeriod>();

        var initDate = new DateOnly(2024, 6, 1);
        var endDate = new DateOnly(2024, 6, 10);

        mockHolidayPeriod.Setup(p => p.GetInitDate()).Returns(new DateOnly(2024, 6, 3));
        mockHolidayPeriod.Setup(p => p.GetFinalDate()).Returns(new DateOnly(2024, 6, 8));
        mockHolidayPeriod.Setup(p => p.GetDurationInDays(initDate, endDate)).Returns(6);

        mockHolidayPlan.Setup(hp => hp.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { mockHolidayPeriod.Object });

        mockHolidayPlanRepo
            .Setup(repo => repo.GetHolidayPlansByAssociations(mockAssociation.Object))
            .Returns(new List<IHolidayPlan> { mockHolidayPlan.Object });

        mockAssociationRepo
            .Setup(repo => repo.FindAllByProject(mockProject.Object))
            .Returns(new List<IAssociationProjectCollaborator> { mockAssociation.Object });

        var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayPlanRepo.Object);

        // Act
        var totalHolidayDays = service.GetHolidayDaysForProjectCollaboratorBetweenDates(mockProject.Object, initDate, endDate);

        // Assert
        Assert.Equal(6, totalHolidayDays);
    }
}