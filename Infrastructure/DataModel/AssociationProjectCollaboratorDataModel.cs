using Domain.Interfaces;

namespace Infrastructure.DataModel
{
    public class AssociationProjectCollaboratorDataModel
    {
        public long Id { get; set; }
        public long CollaboratorId { get; set; }
        public long ProjectId { get; set; }
        public PeriodDateDataModel Period { get; set; }

        public AssociationProjectCollaboratorDataModel() { }

        public AssociationProjectCollaboratorDataModel(IAssociationProjectCollaborator apc)
        {
            Id = apc.GetId();
            CollaboratorId = apc.GetCollaboratorId();
            ProjectId = apc.GetProjectId();
            Period = new PeriodDateDataModel(apc.GetPeriodDate());
        }
    }
}