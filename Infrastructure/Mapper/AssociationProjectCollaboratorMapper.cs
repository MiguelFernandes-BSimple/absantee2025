using Domain.Factory;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper
{
    public class AssociationProjectCollaboratorMapper
    {
        private IAssociationProjectCollaboratorFactory _associationProjectCollaboratorFactory;

        public AssociationProjectCollaboratorMapper(IAssociationProjectCollaboratorFactory associationProjectCollaboratorFactory)
        {
            _associationProjectCollaboratorFactory = associationProjectCollaboratorFactory;
        }

        public AssociationProjectCollaborator ToDomain(AssociationProjectCollaboratorDataModel apcModel)
        {
            var apcDomain = _associationProjectCollaboratorFactory.Create(apcModel);
            return apcDomain;
        }

        public IEnumerable<AssociationProjectCollaborator> ToDomain(IEnumerable<AssociationProjectCollaboratorDataModel> apcModels)
        {
            return apcModels.Select(ToDomain);
        }

        public AssociationProjectCollaboratorDataModel ToDataModel(IAssociationProjectCollaborator apc)
        {
            return new AssociationProjectCollaboratorDataModel(apc);
        }

        public IEnumerable<AssociationProjectCollaboratorDataModel> ToDataModel(IEnumerable<IAssociationProjectCollaborator> apcs)
        {
            return apcs.Select(ToDataModel);
        }
    }
}