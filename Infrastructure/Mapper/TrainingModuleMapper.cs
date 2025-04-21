using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper
{
    public class TrainingModuleMapper : IMapper<TrainingModule, TraningModuleDataModel>
    {
        private readonly TrainingModuleFactory _tmFactory;


        public TraningModuleDataModel ToDataModel(TrainingModule domainEntity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TraningModuleDataModel> ToDataModel(IEnumerable<TrainingModule> dataModels)
        {
            throw new NotImplementedException();
        }

        public TrainingModule ToDomain(TraningModuleDataModel dataModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TrainingModule> ToDomain(IEnumerable<TraningModuleDataModel> dataModels)
        {
            throw new NotImplementedException();
        }
    }
}