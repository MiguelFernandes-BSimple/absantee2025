using Domain.Factory;
using Domain.Interfaces;

namespace Domain.Models;

public class TrainingModule : ITrainingModule
{
    private long _id;
    private long _SubjectID;
    private List<PeriodDateTime> _periodDateTime;

    public TrainingModule(long SubjectID, List<PeriodDateTime> periodDateTime)
    {
        _SubjectID = SubjectID;
        _periodDateTime = periodDateTime;
    }

    public TrainingModule(long id, long SubjectID, List<PeriodDateTime> periodDateTime)
    {
        _id = id;
        _SubjectID = SubjectID;
        this._periodDateTime = periodDateTime;

    }

    public long GetId()
    {
        return _id;
    }

    public long GetSubjectId()
    {
        return _SubjectID;
    }

    public List<PeriodDateTime> GetPeriodDateTimes()
    {
        return new List<PeriodDateTime>(_periodDateTime);
    }


}