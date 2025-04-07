using Domain.Models;

namespace Infrastructure.DataModel
{
    public class AssociationProjectCollaboratorDataModel
    {
        public long Id { get; set; }
        public PeriodDateDataModel Period { get; set; }
        public long CollaboratorId { get; set; }
        public long ProjectId { get; set; }

        public AssociationProjectCollaboratorDataModel() { }

    }
}