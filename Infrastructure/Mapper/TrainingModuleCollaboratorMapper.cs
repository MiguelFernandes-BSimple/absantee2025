using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper
{
    public class TrainingModuleCollaboratorMapper : IMapper<TrainingModuleCollaborators, TrainingModuleCollaboratorDataModel>
    {
        private readonly ITrainingModuleCollaboratorsFactory _factory;

        public TrainingModuleCollaboratorMapper(ITrainingModuleCollaboratorsFactory factory)
        {
            _factory = factory;
        }

        public TrainingModuleCollaboratorDataModel ToDataModel(TrainingModuleCollaborators domainEntity)
        {
            return new TrainingModuleCollaboratorDataModel(domainEntity);
        }

        public IEnumerable<TrainingModuleCollaboratorDataModel> ToDataModel(IEnumerable<TrainingModuleCollaborators> dataModels)
        {
            return dataModels.Select(ToDataModel);
        }

        public TrainingModuleCollaborators ToDomain(TrainingModuleCollaboratorDataModel dataModel)
        {
            return _factory.Create(dataModel);
        }

        public IEnumerable<TrainingModuleCollaborators> ToDomain(IEnumerable<TrainingModuleCollaboratorDataModel> dataModels)
        {
            return dataModels.Select(ToDomain);
        }
    }
}
