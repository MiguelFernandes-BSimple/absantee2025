using Domain.Models;

namespace Infrastructure.DataModel
{
    public class AssociationProjectCollaboratorDataModel
    {
        public long Id { get; set; }
        public long CollaboratorId { get; set; }
        public long ProjectId { get; set; }
        public PeriodDateDataModel Period { get; set; }
        public Collaborator Collaborator { get; set; }
        public Project Project { get; set; }

        public AssociationProjectCollaboratorDataModel() { }

        public AssociationProjectCollaboratorDataModel(AssociationProjectCollaborator apc)
        {
            Id = apc.GetId();
            CollaboratorId = apc.GetCollaboratorId();
            ProjectId = apc.GetProjectId();
            Period = new PeriodDateDataModel((PeriodDate)apc.GetPeriodDate());
            Collaborator = (Collaborator)apc.GetCollaborator();
            Project = (Project)apc.GetProject();
        }
    }
}