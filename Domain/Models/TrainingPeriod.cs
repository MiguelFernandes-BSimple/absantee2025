using Domain.Interfaces;
namespace Domain.Models;

public class TrainingPeriod : ITrainingPeriod
{
    private long _id;
    private IPeriodDate _periodDate;

    public TrainingPeriod(IPeriodDate periodDate)
    {
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
    public void SetId(long id)
    {
        _id = id;
    }

    public IPeriodDate GetPeriodDate()
    {
        return _periodDate;
    }
}
