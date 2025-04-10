using Domain.Interfaces;
using Domain.Visitor;

namespace Infrastructure.DataModel
{
    public class AssociationProjectCollaboratorDataModel : IAssociationProjectCollaboratorVisitor
    {
        public long Id { get; set; }
        public long CollaboratorId { get; set; }
        public long ProjectId { get; set; }
        public IPeriodDate Period { get; set; }

        public AssociationProjectCollaboratorDataModel() { }

        public AssociationProjectCollaboratorDataModel(IAssociationProjectCollaborator apc)
        {
            Id = apc.GetId();
            CollaboratorId = apc.GetCollaboratorId();
            ProjectId = apc.GetProjectId();
            Period = apc.GetPeriodDate();
        }
    }
}