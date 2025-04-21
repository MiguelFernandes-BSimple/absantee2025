using Domain.Interfaces;
namespace Domain.Models;

public class FormationPeriod : IFormationPeriod
{
    private long _id;
    public PeriodDate _periodDate { get; set; }

    public FormationPeriod(PeriodDate periodDate)
    {
        if (periodDate.IsInitDateSmallerThan(DateOnly.FromDateTime(DateTime.Now)))
            throw new ArgumentException("Period date cannot start in the past.");

        _periodDate = periodDate;
    }

    public FormationPeriod(long id, PeriodDate periodDate)
    {
        _id = id;
        _periodDate = periodDate;
    }

    public long GetId()
    {
        return _id;
    }
}
