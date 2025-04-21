using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class TrainingModuleMapper : IMapper<TrainingModule, TrainingModuleDataModel> {
    public TrainingModuleDataModel ToDataModel(TrainingModule tm)
    {
        return new TrainingModuleDataModel(tm);
    }

    public IEnumerable<TrainingModuleDataModel> ToDataModel(IEnumerable<TrainingModule> tm)
    {
        return tm.Select(ToDataModel);
    }

    public TrainingModule ToDomain(TrainingModuleDataModel tmdm)
    {
        return new TrainingModule(tmdm.Id, tmdm.SubjectId, tmdm.Periods);
    }

    public IEnumerable<TrainingModule> ToDomain(IEnumerable<TrainingModuleDataModel> tmdm)
    {
        return tmdm.Select(ToDomain);
    }
}
