using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper
{
    public class TrainingModuleMapper : IMapper<TrainingModule, TraningModuleDataModel>
    {
        private readonly TrainingModuleFactory _tmFactory;

        public TrainingModuleMapper(TrainingModuleFactory tmFactory)
        {
            _tmFactory = tmFactory;
        }

        public TrainingModule ToDomain(TraningModuleDataModel dataModel)
        {
            return _tmFactory.Create(dataModel);
        }

        public IEnumerable<TrainingModule> ToDomain(IEnumerable<TraningModuleDataModel> dataModels)
        {
            return dataModels.Select(tm => ToDomain(tm));
        }

        public TraningModuleDataModel ToDataModel(TrainingModule domainEntity)
        {
            return new TraningModuleDataModel(domainEntity);
        }

        public IEnumerable<TraningModuleDataModel> ToDataModel(IEnumerable<TrainingModule> dataModels)
        {
            return dataModels.Select(tm => ToDataModel(tm));
        }
    }
}