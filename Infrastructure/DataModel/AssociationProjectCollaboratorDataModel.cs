using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel
{
    public class AssociationProjectCollaboratorDataModel : IAssociationProjectCollaboratorVisitor
    {
        public long Id { get; set; }
        public long CollaboratorId { get; set; }
        public long ProjectId { get; set; }
        public PeriodDate Period { get; set; }


        public AssociationProjectCollaboratorDataModel()
        {
        }

        public AssociationProjectCollaboratorDataModel(IAssociationProjectCollaborator apc)
        {
            Id = apc._id;
            CollaboratorId = apc._collaboratorId;
            ProjectId = apc._projectId;
            Period = apc._periodDate;
        }
    }
}