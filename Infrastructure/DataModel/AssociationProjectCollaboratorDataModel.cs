using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel
{
    public class AssociationProjectCollaboratorDataModel : IAssociationProjectCollaboratorVisitor
    {
        public Guid Id { get; set; }
        public Guid CollaboratorId { get; set; }
        public Guid ProjectId { get; set; }
        public PeriodDate PeriodDate { get; set; }


        public AssociationProjectCollaboratorDataModel()
        {
        }

        public AssociationProjectCollaboratorDataModel(IAssociationProjectCollaborator apc)
        {
            Id = apc.Id;
            CollaboratorId = apc.CollaboratorId;
            ProjectId = apc.ProjectId;
            PeriodDate = apc.PeriodDate;
        }
    }
}