using Domain.Interfaces;
namespace Domain.Models;

public class TrainingPeriod : ITrainingPeriod
{
    private long _id;
    private IPeriodDate _periodDate;

    public TrainingPeriod(IPeriodDate periodDate)
    {
        if (periodDate.IsInitDateSmallerThan(DateOnly.FromDateTime(DateTime.Now)))
            throw new ArgumentException("Period date cannot start in the past.");

        _periodDate = periodDate;
    }

    public TrainingPeriod(long id, IPeriodDate periodDate)
    {
        _id = id;
        _periodDate = periodDate;
    }

    public long GetId()
    {
        return _id;
    }

    public IPeriodDate GetPeriodDate()
    {
        return _periodDate;
    }
}
