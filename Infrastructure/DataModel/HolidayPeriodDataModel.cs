using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;

[Table("HolidayPeriods")]
public class HolidayPeriodDataModel : IHolidayPeriodVisitor
{
    public Guid Id { get; set; }
    public PeriodDate PeriodDate { get; set; }

    public HolidayPeriodDataModel(IHolidayPeriod holidayPeriod)
    {
        Id = holidayPeriod.Id;
        PeriodDate = holidayPeriod.PeriodDate;
    }

    public HolidayPeriodDataModel()
    {
    }
}