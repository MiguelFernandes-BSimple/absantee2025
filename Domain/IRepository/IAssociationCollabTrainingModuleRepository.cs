using Domain.Interfaces;
using Domain.Visitor;

namespace Domain.IRepository
{
    public interface IAssociationCollabTrainingModuleRepository : IGenericRepository<IAssociationCollabTrainingModule, IAssociationCollabTrainingModuleVisitor>
    {
        public bool CheckIfCanAdd(long collabId, long trainingModuleId);
    }
}