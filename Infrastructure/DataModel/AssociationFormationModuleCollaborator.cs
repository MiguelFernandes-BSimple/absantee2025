using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel
{
    public class AssociationFormationModuleCollaboratorDataModel : IAssociationFormationModuleCollaboratorVisitor
    {
        public long Id { get; set; }
        public long CollaboratorId { get; set; }
        public long FormationModuleId { get; set; }
        public PeriodDate Period { get; set; }

        public AssociationFormationModuleCollaboratorDataModel() { }

        public AssociationFormationModuleCollaboratorDataModel(IAssociationFormationModuleCollaborator amc)
        {
            Id = amc._id;
            CollaboratorId = amc._collaboratorId;
            FormationModuleId = amc._formationModuleId;
            Period = amc._periodDate;
        }

    }
}