using Domain.Interfaces;
using Moq;
using Infrastructure.Repositories;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Domain.Visitor;
using Infrastructure.Mapper;
using Domain.Models;


namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class HolidayPlanRepositoryAddHolidayPlanAsyncTests
{
    [Fact]
    public async Task WhenAddingCorrectHolidayPlanToRepositoryAsync_ThenReturnTrue()
    {
        // Arrange 
        long collabId = 1;
        var HolidayPlanDM1 = new Mock<IHolidayPlanVisitor>();
        var HolidayPlanDM2 = new Mock<IHolidayPlanVisitor>();

        var options = new DbContextOptionsBuilder<AbsanteeContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
                        .Options;
        using var context = new AbsanteeContext(options);
        Mock<IHolidayPlan> doubleHolidayPlan = new Mock<IHolidayPlan>();

        doubleHolidayPlan.Setup(hp => hp.GetCollaboratorId()).Returns(collabId);
        HolidayPlanDM1.Setup(hp => hp.Id).Returns(2);
        HolidayPlanDM2.Setup(hp => hp.Id).Returns(collabId);

        Mock<HolidayPlanMapper> doubleHolidayPlanMapper = new Mock<HolidayPlanMapper>();
        doubleHolidayPlanMapper.Setup(hpm => hpm.ToDataModel((HolidayPlan)doubleHolidayPlan.Object)).Returns((HolidayPlanDataModel)HolidayPlanDM1.Object);

        var holidayPeriodMapper = new Mock<HolidayPeriodMapper>();

        HolidayPlanRepositoryEF HolidayPlanRepositoryEF = new HolidayPlanRepositoryEF(context, doubleHolidayPlanMapper.Object, holidayPeriodMapper.Object);
        // Act
        bool result = await HolidayPlanRepositoryEF.AddHolidayPlanAsync(doubleHolidayPlan.Object);
        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task WhenAddingHolidayPlanWithRepeatedCollaboratorToRepositoryAsync_ThenReturnFalse()
    {
        // Arrange 
        Mock<ICollaborator> doubleCollab = new Mock<ICollaborator>();
        long collabId = 1;
        var HolidayPlanDM1 = new Mock<IHolidayPlanVisitor>();
        var HolidayPlanDM2 = new Mock<IHolidayPlanVisitor>();
        var HolidayPlanDM3 = new Mock<IHolidayPlanVisitor>();

        var holidayPlanDataModels = new List<HolidayPlanDataModel>
            {
                (HolidayPlanDataModel)HolidayPlanDM1.Object,
                (HolidayPlanDataModel)HolidayPlanDM2.Object,
                (HolidayPlanDataModel)HolidayPlanDM3.Object,

            }.AsQueryable();
        Mock<IHolidayPlan> doubleHolidayPlan = new Mock<IHolidayPlan>(); doubleHolidayPlan.Setup(hp => hp.GetCollaboratorId()).Returns(collabId);
        HolidayPlanDM1.Setup(hp => hp.Id).Returns(2);
        HolidayPlanDM2.Setup(hp => hp.Id).Returns(2);
        HolidayPlanDM3.Setup(hp => hp.Id).Returns(2);

        var mockSet = new Mock<DbSet<HolidayPlanDataModel>>();
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Provider).Returns(holidayPlanDataModels.Provider);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Expression).Returns(holidayPlanDataModels.Expression);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.ElementType).Returns(holidayPlanDataModels.ElementType);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.GetEnumerator()).Returns(holidayPlanDataModels.GetEnumerator());

        var absanteeMock = new Mock<IAbsanteeContext>();
        absanteeMock.Setup(a => a.HolidayPlans).Returns(mockSet.Object);
        Mock<HolidayPlanMapper> doubleHolidayPlanMapper = new Mock<HolidayPlanMapper>();
        doubleHolidayPlanMapper.Setup(hpm => hpm.ToDataModel((HolidayPlan)doubleHolidayPlan.Object)).Returns((HolidayPlanDataModel)HolidayPlanDM1.Object);

        var holidayPeriodMapper = new Mock<HolidayPeriodMapper>();


        HolidayPlanRepositoryEF HolidayPlanRepositoryEF = new HolidayPlanRepositoryEF((AbsanteeContext)absanteeMock.Object, doubleHolidayPlanMapper.Object, holidayPeriodMapper.Object);
        // Act
        bool result = await HolidayPlanRepositoryEF.AddHolidayPlanAsync(doubleHolidayPlan.Object);
        // Assert
        Assert.False(result);

    }
}