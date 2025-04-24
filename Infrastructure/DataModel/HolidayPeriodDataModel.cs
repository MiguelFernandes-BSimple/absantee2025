using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;

[Table("HolidayPeriod")]
public class HolidayPeriodDataModel : IHolidayPeriodVisitor
{
    public Guid Id { get; }
    public PeriodDate PeriodDate { get; }

    public HolidayPeriodDataModel(IHolidayPeriod holidayPeriod)
    {
        Id = holidayPeriod.Id;
        PeriodDate = holidayPeriod.PeriodDate;
    }

    public HolidayPeriodDataModel()
    {
    }
}