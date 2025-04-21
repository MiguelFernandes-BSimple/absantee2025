using Domain.Factory;
using Domain.Interfaces;

namespace Domain.Models;

public class TrainingModule : ITrainingModule
{
    private long _id;
    private long _SubjectID;
    private List<ITrainingPeriod> _TrainingPeriods;

    public TrainingModule(long SubjectID, List<ITrainingPeriod> TrainingPeriods)
    {
        this._SubjectID = SubjectID;
        this._TrainingPeriods = TrainingPeriods;
    }

    public TrainingModule(long id, long SubjectID, List<ITrainingPeriod> TrainingPeriods)
    {
        this._id = id;
        this._SubjectID = SubjectID;
        this._TrainingPeriods = TrainingPeriods;

    }

    public long GetId()
    {
        return _id;
    }

    public long GetSubjectId()
    {
        return _SubjectID;
    }

    public List<ITrainingPeriod> GetTrainingPeriods()
    {
        return new List<ITrainingPeriod>(_TrainingPeriods);
    }


}