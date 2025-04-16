using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;

[Table("HolidayPeriod")]
public class HolidayPeriodDataModel : IHolidayPeriodVisitor
{
    public long Id { get; set; }
    public PeriodDate PeriodDate { get; set; }

    public HolidayPeriodDataModel(IHolidayPeriod holidayPeriod)
    {
        Id = holidayPeriod.GetId();
        PeriodDate = (PeriodDate)holidayPeriod.GetPeriodDate();
    }
}