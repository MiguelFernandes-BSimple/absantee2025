using Domain.Models;
using Infrastructure.Mapper;

namespace Infrastructure.DataModel;

public class TrainingSubjectMapper : IMapper<TrainingSubject, TrainingSubjectDataModel> {
    public TrainingSubjectDataModel ToDataModel(TrainingSubject ts)
    {
        return new TrainingSubjectDataModel(ts);
    }

    public IEnumerable<TrainingSubjectDataModel> ToDataModel(IEnumerable<TrainingSubject> ts)
    {
        return ts.Select(ToDataModel);
    }

    public TrainingSubject ToDomain(TrainingSubjectDataModel tsdm)
    {
        return new TrainingSubject(tsdm.Id, tsdm.Title, tsdm.Description);
    }

    public IEnumerable<TrainingSubject> ToDomain(IEnumerable<TrainingSubjectDataModel> tsdm)
    {
        return tsdm.Select(ToDomain);
    }
}
