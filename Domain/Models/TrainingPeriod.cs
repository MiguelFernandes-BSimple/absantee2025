using Domain.Interfaces;
namespace Domain.Models;

public class TrainingPeriod : ITrainingPeriod
{
    private long _id;
    private PeriodDate _periodDate;

    public TrainingPeriod(PeriodDate periodDate)
    {
        if (periodDate.IsInitDateSmallerThan(DateOnly.FromDateTime(DateTime.Now)))
            throw new ArgumentException("Period date cannot start in the past.");

        _periodDate = periodDate;
    }

    public TrainingPeriod(long id, PeriodDate periodDate)
    {
        _id = id;
        _periodDate = periodDate;
    }

    public long GetId()
    {
        return _id;
    }

    public PeriodDate GetPeriodDate()
    {
        return _periodDate;
    }
}
