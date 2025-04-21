using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper
{
    public class AssociationFormationModuleCollaboratorMapper : IMapper<AssociationFormationModuleCollaborator, AssociationFormationModuleCollaboratorDataModel>
    {
        private IAssociationFormationModuleCollaboratorFactory _associationFormationModuleCollaboratorFactory;

        public AssociationFormationModuleCollaboratorMapper(IAssociationFormationModuleCollaboratorFactory associationFormationModuleCollaboratorFactory)
        {
            _associationFormationModuleCollaboratorFactory = associationFormationModuleCollaboratorFactory;
        }

        public AssociationFormationModuleCollaborator ToDomain(AssociationFormationModuleCollaboratorDataModel amcModel)
        {
            var amcDomain = _associationFormationModuleCollaboratorFactory.Create(amcModel);
            return amcDomain;
        }

        public IEnumerable<AssociationFormationModuleCollaborator> ToDomain(IEnumerable<AssociationFormationModuleCollaboratorDataModel> amcModels)
        {
            return amcModels.Select(ToDomain);
        }

        public AssociationFormationModuleCollaboratorDataModel ToDataModel(AssociationFormationModuleCollaborator amc)
        {
            return new AssociationFormationModuleCollaboratorDataModel(amc);
        }

        public IEnumerable<AssociationFormationModuleCollaboratorDataModel> ToDataModel(IEnumerable<AssociationFormationModuleCollaborator> amcs)
        {
            return amcs.Select(ToDataModel);
        }

    }
}