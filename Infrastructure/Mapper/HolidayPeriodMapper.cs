using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class HolidayPeriodMapper
{
    public HolidayPeriod ToDomain(HolidayPeriodDataModel holidayPeriodDM)
    {
        IPeriodDate periodDate = new PeriodDate(holidayPeriodDM.PeriodDate._initDate, holidayPeriodDM.PeriodDate._finalDate);
        HolidayPeriod holidayPeriod = new HolidayPeriod(periodDate);

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