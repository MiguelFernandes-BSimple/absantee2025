using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;

namespace Infrastructure.DataModel;

[Table("HolidayPeriod")]
public class HolidayPeriodDataModel
{
    public long Id { get; set; }
    public PeriodDate PeriodDate { get; set; }

    public HolidayPeriodDataModel(HolidayPeriod holidayPeriod)
    {
        Id = holidayPeriod.GetId();
        PeriodDate = (PeriodDate)holidayPeriod.GetPeriodDate();
    }
}