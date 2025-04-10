using Domain.Interfaces;
namespace Domain.Models;

public class TrainingPeriod : ITrainingPeriod
{
    private long _id;
    private IPeriodDate _periodDate;

    public TrainingPeriod(IPeriodDate periodDate)
    {
        if (CheckInputValues(periodDate))
        {
            _periodDate = periodDate;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    private bool CheckInputValues(IPeriodDate periodDate)
    {

        if (periodDate.IsInitDateSmallerThan(DateOnly.FromDateTime(DateTime.Now)))
            return false;

        return true;
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
