using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;

namespace Infrastructure.DataModel;

[Table("HolidayPeriod")]
public class HolidayPeriodDataModel
{
    public long Id { get; set; }
    public PeriodDateDataModel PeriodDate { get; set; }

    public HolidayPeriodDataModel() { }

    public HolidayPeriodDataModel(HolidayPeriod holidayPeriod)
    {
        Id = holidayPeriod.GetId();
        PeriodDate = new PeriodDateDataModel(holidayPeriod.GetPeriodDate());
    }
}