using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Mapper
{
    public class AssociationCollaboratorTrainingModuleMapper : IMapper<AssociationCollaboratorTrainingModule, AssociationCollaboratorTrainingModuleDataModel>
    {
        private readonly IAssociationCollaboratorTrainingModuleFactory _associationCollaboratorTrainingModuleFactory;

        public AssociationCollaboratorTrainingModuleMapper(IAssociationCollaboratorTrainingModuleFactory associationCollaboratorTrainingModuleFactory)
        {
            _associationCollaboratorTrainingModuleFactory = associationCollaboratorTrainingModuleFactory;
        }

        public AssociationCollaboratorTrainingModule ToDomain(AssociationCollaboratorTrainingModuleDataModel actmModel)
        {
            var actmDomain = _associationCollaboratorTrainingModuleFactory.Create(actmModel);
            return actmDomain;
        }

        public IEnumerable<AssociationCollaboratorTrainingModule> ToDomain(IEnumerable<AssociationCollaboratorTrainingModuleDataModel> actmModels)
        {
            return actmModels.Select(ToDomain);
        }

        public AssociationCollaboratorTrainingModuleDataModel ToDataModel(AssociationCollaboratorTrainingModule actm)
        {
            return new AssociationCollaboratorTrainingModuleDataModel(actm);
        }

        public IEnumerable<AssociationCollaboratorTrainingModuleDataModel> ToDataModel(IEnumerable<AssociationCollaboratorTrainingModule> actms)
        {
            return actms.Select(ToDataModel);
        }
    }
}
