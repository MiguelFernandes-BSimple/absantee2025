using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.DataModel;

[Table("HolidayPeriod")]
public class HolidayPeriodDataModel
{
    public long Id { get; set; }
    public PeriodDateDataModel PeriodDate { get; set; }

    public HolidayPeriodDataModel()
    {
    }
}