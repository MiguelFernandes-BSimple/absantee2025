using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class HolidayPeriodMapper
{
    private PeriodDateMapper _periodDateMapper;

    public HolidayPeriodMapper(PeriodDateMapper periodDateMapper)
    {
        _periodDateMapper = periodDateMapper;
    }

    public HolidayPeriod ToDomain(HolidayPeriodDataModel holidayPeriodDM)
    {
        HolidayPeriod holidayPeriod = new HolidayPeriod(holidayPeriodDM.PeriodDate);

        holidayPeriod.SetId(holidayPeriodDM.Id);

        return holidayPeriod;
    }

    public IEnumerable<HolidayPeriod> ToDomain(IEnumerable<HolidayPeriodDataModel> holidayPeriodsDM)
    {
        return holidayPeriodsDM.Select(ToDomain);
    }

    public HolidayPeriodDataModel ToDataModel(HolidayPeriod holidayPeriod)
    {
        return new HolidayPeriodDataModel(holidayPeriod);
    }

    public IEnumerable<HolidayPeriodDataModel> ToDataModel(IEnumerable<HolidayPeriod> holidayPeriods)
    {
        return holidayPeriods.Select(ToDataModel);
    }
}