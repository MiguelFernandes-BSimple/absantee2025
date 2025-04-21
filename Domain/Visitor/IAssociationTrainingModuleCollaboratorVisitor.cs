using Domain.Models;

namespace Domain.Visitor
{
    public interface IAssociationTrainingModuleCollaboratorVisitor
    {
        public long Id { get; set; }
        public long CollaboratorId { get; set; }
        public long TrainingModuleId { get; set; }
        public PeriodDateTime PeriodDateTime { get; set; }
    }
}