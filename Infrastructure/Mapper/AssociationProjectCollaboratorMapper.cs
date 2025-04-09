using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper
{
    public class AssociationProjectCollaboratorMapper
    {
        private PeriodDateMapper _periodDateMapper;
        private ITrustedAssociationProjectCollaboratorFactory _trustedAssociationProjectCollaboratorFactory;

        public AssociationProjectCollaboratorMapper(PeriodDateMapper periodDateMapper, ITrustedAssociationProjectCollaboratorFactory trustedAssociationProjectCollaboratorFactory)
        {
            _periodDateMapper = periodDateMapper;
            _trustedAssociationProjectCollaboratorFactory = trustedAssociationProjectCollaboratorFactory;
        }

        public AssociationProjectCollaborator ToDomain(AssociationProjectCollaboratorDataModel apcModel)
        {
            IPeriodDate periodDate = _periodDateMapper.ToDomain(apcModel.Period);
            var apcDomain = _trustedAssociationProjectCollaboratorFactory.Create(apcModel.Id, apcModel.CollaboratorId, apcModel.ProjectId, periodDate);
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