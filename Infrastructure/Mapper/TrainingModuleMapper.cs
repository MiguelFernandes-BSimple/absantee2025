using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;
public class TrainingModuleMapper : IMapper<TrainingModule, TrainingModuleDataModel>
{
    private ITrainingModuleFactory _trainingModuleFactory;

    public TrainingModuleMapper(ITrainingModuleFactory trainingModuleFactory)
    {
        _trainingModuleFactory = trainingModuleFactory;
    }

    public TrainingModule ToDomain(TrainingModuleDataModel tmModel)
    {
        var tmDomain = _trainingModuleFactory.Create(tmModel);
        return tmDomain;
    }

    public IEnumerable<TrainingModule> ToDomain(IEnumerable<TrainingModuleDataModel> tmModels)
    {
        return tmModels.Select(ToDomain);
    }

    public TrainingModuleDataModel ToDataModel(TrainingModule tm)
    {
        return new TrainingModuleDataModel(tm);
    }

    public IEnumerable<TrainingModuleDataModel> ToDataModel(IEnumerable<TrainingModule> tms)
    {
        return tms.Select(ToDataModel);
    }

}
