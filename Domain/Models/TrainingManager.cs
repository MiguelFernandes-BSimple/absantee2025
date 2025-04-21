using Domain.Interfaces;

namespace Domain.Models;

public class TrainingManager : ITrainingManager
{
    public long _id;
    public long _userId;
    public PeriodDateTime _periodDateTime;

    public TrainingManager(long userId, PeriodDateTime periodDateTime)
    {
        _userId = userId;
        _periodDateTime = periodDateTime;
    }

    public TrainingManager(long id, long userId, PeriodDateTime periodDateTime) 
        : this(userId, periodDateTime)
    {
         _id = id;
    }

    public long GetId()
    {
        return _id;
    }

    public void SetId(long id)
    {
        _id = id;
    }
    public long GetUserId()
    {
        return _userId;
    }

    public PeriodDateTime GetPeriodDateTime()
    {
        return _periodDateTime;
    }
}