using Domain.Factory.TrainingModuleFactory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class TrainingModuleMapper : IMapper<TrainingModule, TrainingModuleDataModel>
{
    private TrainingModuleFactory _factory;

    public TrainingModuleMapper(TrainingModuleFactory factory)
    {
        _factory = factory;
    }

    public TrainingModuleDataModel ToDataModel(TrainingModule domainEntity)
    {
        return new TrainingModuleDataModel(domainEntity);
    }

    public IEnumerable<TrainingModuleDataModel> ToDataModel(IEnumerable<TrainingModule> dataModels)
    {
        return dataModels.Select(ToDataModel);
    }

    public TrainingModule ToDomain(TrainingModuleDataModel dataModel)
    {
        var atcDomain = _factory.Create(dataModel);

        return atcDomain;
    }

    public IEnumerable<TrainingModule> ToDomain(IEnumerable<TrainingModuleDataModel> dataModels)
    {
        return dataModels.Select(ToDomain);
    }
}