namespace Domain.Models;

public class TrainingModule {
    private long _id;
    private long _subjectId;
    public List<PeriodDateTime> _periods;

    public TrainingModule(long subjectId, List<PeriodDateTime> periods) {
        _subjectId = subjectId;
        _periods = periods;
    }

    public TrainingModule(long id, long subjectId, List<PeriodDateTime> periods) {
        _id = id;
        _subjectId = subjectId;
        _periods = periods;
    }

    public long GetId() {
        return _id;
    }

    public long GetSubjectId() {
        return _subjectId;
    }

    public List<PeriodDateTime> GetPeriods() {
        return _periods;
    }
}
