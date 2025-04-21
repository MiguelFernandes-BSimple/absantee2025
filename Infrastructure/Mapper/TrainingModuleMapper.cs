using Domain.Factory;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class TrainingModuleMapper : IMapper<ITrainingModule, TrainingModuleDataModel>
{
    private readonly TrainingModuleFactory _trainingModuleFactory;
    private readonly TrainingPeriodMapper _trainingPeriodMapper;

    public TrainingModuleMapper(TrainingModuleFactory trainingModuleFactory, TrainingPeriodMapper trainingPeriodMapper)
    {
        _trainingModuleFactory = trainingModuleFactory;
        _trainingPeriodMapper = trainingPeriodMapper;
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
        return new TrainingModuleDataModel(trainingModules, _trainingPeriodMapper);

    }

    public IEnumerable<TrainingModuleDataModel> ToDataModel(IEnumerable<ITrainingModule> trainingModules)
    {
        return trainingModules.Select(ToDataModel);
    }
}