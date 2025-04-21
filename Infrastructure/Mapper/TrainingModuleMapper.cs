using Domain.Factory;
using Domain.Factory.TrainingModuleFactory;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;


public class TrainingModuleMapper : IMapper<TrainingModule, TrainingModuleDataModel>
{
    ITrainingModuleFactory _trainingModuleFactory;


    public TrainingModuleMapper(ITrainingModuleFactory trainingModuleFactory)
    {
        _trainingModuleFactory = trainingModuleFactory;
    }
    
    public TrainingModule ToDomain(TrainingModuleDataModel trainingModuleDM)
    {
        var trainingModuleDomain = _trainingModuleFactory.Create(trainingModuleDM);
        return trainingModuleDomain;
    }

    public IEnumerable<TrainingModule> ToDomain(IEnumerable<TrainingModuleDataModel> trainingModuleDM )
    {
        return trainingModuleDM.Select(ToDomain);
    }

    public TrainingModuleDataModel ToDataModel(TrainingModule trainingModule)
    {
        return  new TrainingModuleDataModel(trainingModule);
    }

    public IEnumerable<TrainingModuleDataModel> ToDataModel(IEnumerable<TrainingModule> trainingModules)
    {
        return trainingModules.Select(ToDataModel);
    }   
}