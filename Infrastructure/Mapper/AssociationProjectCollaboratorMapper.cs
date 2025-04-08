using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper
{
    public class AssociationProjectCollaboratorMapper
    {
        public AssociationProjectCollaborator ToDomain(AssociationProjectCollaboratorDataModel apcModel)
        {
            var periodDate = new PeriodDate(apcModel.Period._initDate, apcModel.Period._finalDate);
            var apcDomain = new AssociationProjectCollaborator(apcModel.CollaboratorId, apcModel.ProjectId, periodDate, apcModel.Collaborator, apcModel.Project);

            apcDomain.SetId(apcModel.Id);

            return apcDomain;
        }

        public IEnumerable<AssociationProjectCollaborator> ToDomain(IEnumerable<AssociationProjectCollaboratorDataModel> apcModels)
        {
            return apcModels.Select(ToDomain);
        }

        public AssociationProjectCollaboratorDataModel ToDataModel(AssociationProjectCollaborator apc)
        {
            return new AssociationProjectCollaboratorDataModel(apc);
        }

        public IEnumerable<AssociationProjectCollaboratorDataModel> ToDataModel(IEnumerable<AssociationProjectCollaborator> apcs)
        {
            return apcs.Select(ToDataModel);
        }
    }
}