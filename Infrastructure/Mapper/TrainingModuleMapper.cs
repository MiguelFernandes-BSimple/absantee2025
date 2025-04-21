using Domain.Factory;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper
{
    public class TrainingModuleMapper : IMapper<TrainingModule, TrainingModuleDataModel>
    {
        private readonly ITrainingModuleFactory _factory;

        public TrainingModuleMapper(ITrainingModuleFactory factory)
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
            var trainingModule = _factory.Create(dataModel);
            return trainingModule;
        }

        public IEnumerable<TrainingModule> ToDomain(IEnumerable<TrainingModuleDataModel> dataModels)
        {
            return dataModels.Select(ToDomain);
        }
    }
}
