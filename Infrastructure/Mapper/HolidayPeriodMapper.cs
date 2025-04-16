using Domain.Factory;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class HolidayPeriodMapper : IMapper<IHolidayPeriod, HolidayPeriodDataModel>
{
    private readonly HolidayPeriodFactory _factory;

    public HolidayPeriodMapper(HolidayPeriodFactory factory)
    {
        _factory = factory;
    }

    public IHolidayPeriod ToDomain(HolidayPeriodDataModel holidayPeriodDM)
    {
        HolidayPeriod holidayPeriod = _factory.Create(holidayPeriodDM);

        return holidayPeriod;
    }

    public IEnumerable<IHolidayPeriod> ToDomain(IEnumerable<HolidayPeriodDataModel> holidayPeriodsDM)
    {
        return holidayPeriodsDM.Select(ToDomain);
    }

    public HolidayPeriodDataModel ToDataModel(IHolidayPeriod holidayPeriod)
    {
        return new HolidayPeriodDataModel(holidayPeriod);
    }

    public IEnumerable<HolidayPeriodDataModel> ToDataModel(IEnumerable<IHolidayPeriod> holidayPeriods)
    {
        return holidayPeriods.Select(ToDataModel);
    }
}