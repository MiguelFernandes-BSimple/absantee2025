using Moq;
using Infrastructure.Repositories;
using Domain.Interfaces;
using Infrastructure.Mapper;
using Domain.Models;
using Infrastructure.DataModel;
using Domain.Visitor;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class FindHolidayPeriodsByCollaboratorAsync
{
    [Fact]
    public async Task WhenFindingHolidayPeriodsByCollaboratorAsync_ThenReturnsCorrectPeriods()
    {
        // Arrange
        var holidayPlanDM1 = new Mock<IHolidayPlanVisitor>();
        var holidayPlanDM2 = new Mock<IHolidayPlanVisitor>();
        var holidayPlanDM3 = new Mock<IHolidayPlanVisitor>();
        var holidayPlans = new List<HolidayPlanDataModel>
            {
                (HolidayPlanDataModel)holidayPlanDM1.Object,
                (HolidayPlanDataModel)holidayPlanDM2.Object,
                (HolidayPlanDataModel)holidayPlanDM3.Object
            }.AsQueryable();

        var mockSet = new Mock<DbSet<HolidayPlanDataModel>>();
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Provider).Returns(holidayPlans.Provider);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Expression).Returns(holidayPlans.Expression);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.ElementType).Returns(holidayPlans.ElementType);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.GetEnumerator()).Returns(holidayPlans.GetEnumerator());

        var absanteeMock = new Mock<IAbsanteeContext>();
        absanteeMock.Setup(a => a.HolidayPlans).Returns(mockSet.Object);

        holidayPlanDM1.Setup(hp => hp.CollaboratorId).Returns(1);
        holidayPlanDM2.Setup(hp => hp.CollaboratorId).Returns(2);
        holidayPlanDM3.Setup(hp => hp.CollaboratorId).Returns(3);

        var expected = new List<IHolidayPeriod>()
        {
            new Mock<IHolidayPeriod>().Object,
            new Mock<IHolidayPeriod>().Object
        };

        holidayPlanDM3.Setup(hp => hp.GetHolidayPeriods()).Returns(expected);

        var holidayPlanMapperMock = new Mock<IMapper<HolidayPlan, HolidayPlanDataModel>>();
        var holidayPlanRepo = new HolidayPlanRepositoryEF((AbsanteeContext)absanteeMock.Object, (HolidayPlanMapper)holidayPlanMapperMock.Object);

        // Act
        var result = await holidayPlanRepo.FindHolidayPeriodsByCollaboratorAsync(3);

        // Assert
        Assert.True(result.SequenceEqual(expected));
    }

    [Fact]
    public async Task WhenNotFindingHolidayPeriodsByCollaboratorAsync_ThenReturnsEmptyList()
    {
        // Arrange
        var holidayPlanDM1 = new Mock<IHolidayPlanVisitor>();
        var holidayPlanDM2 = new Mock<IHolidayPlanVisitor>();
        var holidayPlanDM3 = new Mock<IHolidayPlanVisitor>();
        var holidayPlans = new List<HolidayPlanDataModel>
            {
                (HolidayPlanDataModel)holidayPlanDM1.Object,
                (HolidayPlanDataModel)holidayPlanDM2.Object,
                (HolidayPlanDataModel)holidayPlanDM3.Object
            }.AsQueryable();

        var mockSet = new Mock<DbSet<HolidayPlanDataModel>>();
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Provider).Returns(holidayPlans.Provider);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Expression).Returns(holidayPlans.Expression);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.ElementType).Returns(holidayPlans.ElementType);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.GetEnumerator()).Returns(holidayPlans.GetEnumerator());

        var absanteeMock = new Mock<IAbsanteeContext>();
        absanteeMock.Setup(a => a.HolidayPlans).Returns(mockSet.Object);

        holidayPlanDM1.Setup(hp => hp.CollaboratorId).Returns(1);
        holidayPlanDM2.Setup(hp => hp.CollaboratorId).Returns(2);
        holidayPlanDM3.Setup(hp => hp.CollaboratorId).Returns(3);

        var holidayPlanMapperMock = new Mock<IMapper<HolidayPlan, HolidayPlanDataModel>>();
        var holidayPlanRepo = new HolidayPlanRepositoryEF((AbsanteeContext)absanteeMock.Object, (HolidayPlanMapper)holidayPlanMapperMock.Object);

        // Act
        var result = await holidayPlanRepo.FindHolidayPeriodsByCollaboratorAsync(4);

        // Assert
        Assert.Null(result);
    }
}