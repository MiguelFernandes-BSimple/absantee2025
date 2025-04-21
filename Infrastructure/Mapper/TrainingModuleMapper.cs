using Domain.Factory;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class TrainingModuleMapper : IMapper<ITrainingModule, TrainingModuleDataModel>
{
    private readonly TrainingModuleFactory _trainingModuleFactory;
    private readonly PeriodDateTime _periodDateTime;

    public TrainingModuleMapper(TrainingModuleFactory trainingModuleFactory, PeriodDateTime periodDateTime)
    {
        _trainingModuleFactory = trainingModuleFactory;
        _periodDateTime = periodDateTime;
    }

    public ITrainingModule ToDomain(TrainingModuleDataModel trainingModuleDM)
    {
        TrainingModule trainingModule = _trainingModuleFactory.Create(trainingModuleDM);

        return trainingModule;
    }

    public IEnumerable<ITrainingModule> ToDomain(IEnumerable<TrainingModuleDataModel> trainingModuleDM)
    {
        return trainingModuleDM.Select(ToDomain);
    }

    public TrainingModuleDataModel ToDataModel(ITrainingModule trainingModules)
    {
        return new TrainingModuleDataModel(trainingModules);

    }

    public IEnumerable<TrainingModuleDataModel> ToDataModel(IEnumerable<ITrainingModule> trainingModules)
    {
        return trainingModules.Select(ToDataModel);
    }
}