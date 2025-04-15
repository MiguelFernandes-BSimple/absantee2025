using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
namespace Application.Tests.HolidayPlanServiceTests;

public class FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates
{
    //UC20 
    [Fact]
    public async Task WhenGivenCorrectValues_ThenReturnOverlappingHolidayPeriodBetweenTwoCollabsInPeriod()
    {
        //arrange

        //collab1
        Mock<IHolidayPeriod> holidayPeriod1 = new Mock<IHolidayPeriod>();
        var holidayPeriodsList1 = new List<IHolidayPeriod> { holidayPeriod1.Object };

        //colab2
        Mock<IHolidayPeriod> holidayPeriod2 = new Mock<IHolidayPeriod>();
        var holidayPeriodsList2 = new List<IHolidayPeriod> { holidayPeriod2.Object };

        holidayPeriod1.Setup(hp => hp.Intersects(holidayPeriod2.Object)).Returns(true);
        holidayPeriod2.Setup(hp => hp.Intersects(holidayPeriod1.Object)).Returns(true);        

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(1, It.IsAny<IPeriodDate>())).ReturnsAsync(holidayPeriodsList1);
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(2, It.IsAny<IPeriodDate>())).ReturnsAsync(holidayPeriodsList2);

        var expected = new List<IHolidayPeriod>() { holidayPeriod1.Object, holidayPeriod2.Object };

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        //act
        var result = await service.FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDatesAsync(1, 2, It.IsAny<IPeriodDate>());

        //assert
        Assert.True(expected.SequenceEqual(result));
    }

    [Fact]
    public async Task WhenGivenSearchPeriodOutsideOverlappingHoliadyPeriod_ThenReturnEmpty()
    {
        //arrange

        //collab1
        Mock<IHolidayPeriod> holidayPeriod1 = new Mock<IHolidayPeriod>();
        var holidayPeriodsList1 = new List<IHolidayPeriod> { holidayPeriod1.Object };

        //colab2
        Mock<IHolidayPeriod> holidayPeriod2 = new Mock<IHolidayPeriod>();
        var holidayPeriodsList2 = new List<IHolidayPeriod> { holidayPeriod2.Object };

        holidayPeriod1.Setup(hp => hp.Intersects(holidayPeriod2.Object)).Returns(false);

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(It.IsAny<long>(), It.IsAny<IPeriodDate>())).ReturnsAsync(holidayPeriodsList1);
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(It.IsAny<long>(), It.IsAny<IPeriodDate>())).ReturnsAsync(holidayPeriodsList2);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        //act
        var result = await service.FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDatesAsync(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<IPeriodDate>());

        //assert
        Assert.Empty(result);
    }
}