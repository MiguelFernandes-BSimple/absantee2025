namespace Infrastructure.Mapper;

using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

public class TrainingManagerMapper : IMapper<TrainingManager, TrainingManagerDataModel>
{
    public TrainingManager ToDomain(TrainingManagerDataModel trainingManagerDM)
    {
        TrainingManager trainingManager = new TrainingManager(trainingManagerDM.UserId, trainingManagerDM.PeriodDateTime);

        trainingManager.SetId(trainingManagerDM.Id);

        return trainingManager;
    }

    public IEnumerable<TrainingManager> ToDomain(IEnumerable<TrainingManagerDataModel> trainingManagerDM)
    {
        return trainingManagerDM.Select(ToDomain);
    }

    public TrainingManagerDataModel ToDataModel(TrainingManager trainingManager)
    {
        return new TrainingManagerDataModel(trainingManager);
    }

    public IEnumerable<TrainingManagerDataModel> ToDataModel(IEnumerable<TrainingManager> trainingManagers)
    {
        return trainingManagers.Select(ToDataModel);
    }
}