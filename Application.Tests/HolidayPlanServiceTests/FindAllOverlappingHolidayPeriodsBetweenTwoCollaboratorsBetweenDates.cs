using Domain.Interfaces;
using Infrastructure.Interfaces;
using Application.Services;
using Moq;
using System.IO.Compression;
namespace Application.Tests.HolidayPlanServiceTests;

public class FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates
{
    //UC20 
    [Fact]
    public void WhenGivenCorrectValues_ThenReturnOverlappingHolidayPeriodBetweenTwoCollabsInPeriod()
    {
        //arrange
        //collab1
        Mock<ICollaborator> collab1 = new Mock<ICollaborator>();

        var periodDateDouble1 = new Mock<IPeriodDate>();

        var periodDateDouble2 = new Mock<IPeriodDate>();

        Mock<IHolidayPeriod> holidayPeriod1 = new Mock<IHolidayPeriod>();
        var holidayPeriodsList1 = new List<IHolidayPeriod> { holidayPeriod1.Object };

        var searchPeriodDateDouble = new Mock<IPeriodDate>();

        //colab2
        Mock<ICollaborator> collab2 = new Mock<ICollaborator>();

        Mock<IHolidayPeriod> holidayPeriod2 = new Mock<IHolidayPeriod>();
        var holidayPeriodsList2 = new List<IHolidayPeriod> { holidayPeriod2.Object };
        holidayPeriod1.Setup(hp => hp.Intersects(holidayPeriod2.Object)).Returns(true);

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab1.Object, searchPeriodDateDouble.Object)).Returns(holidayPeriodsList1);
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab2.Object, searchPeriodDateDouble.Object)).Returns(holidayPeriodsList2);

        var expected = new List<IHolidayPeriod>() { holidayPeriod1.Object, holidayPeriod2.Object };

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        //act
        var result = service.FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(collab1.Object, collab2.Object, searchPeriodDateDouble.Object);

        //assert
        Assert.True(expected.SequenceEqual(result));
    }

    [Fact]
    public void WhenGivenSearchPeriodOutsideOverlappingHoliadyPeriod_ThenReturnEmpty()
    {
        //arrange

        var searchPeriodDateDouble = new Mock<IPeriodDate>();

        //collab1
        Mock<ICollaborator> collab1 = new Mock<ICollaborator>();

        var periodDateDouble = new Mock<IPeriodDate>();

        Mock<IHolidayPeriod> holidayPeriod1 = new Mock<IHolidayPeriod>();
        var holidayPeriodsList1 = new List<IHolidayPeriod> { holidayPeriod1.Object };

        //colab2
        Mock<ICollaborator> collab2 = new Mock<ICollaborator>();

        var periodDateDouble2 = new Mock<IPeriodDate>();

        Mock<IHolidayPeriod> holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(hp => hp.GetPeriodDate()).Returns(periodDateDouble2.Object);
        var holidayPeriodsList2 = new List<IHolidayPeriod> { holidayPeriod2.Object };
        holidayPeriod1.Setup(hp => hp.Intersects(holidayPeriod2.Object)).Returns(false);

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab1.Object, searchPeriodDateDouble.Object)).Returns(holidayPeriodsList1);
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab2.Object, searchPeriodDateDouble.Object)).Returns(holidayPeriodsList2);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        //act
        var result = service.FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(collab1.Object, collab2.Object, searchPeriodDateDouble.Object);

        //assert
        Assert.Empty(result);
    }
}