using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Visitor;

namespace Domain.IRepository
{
    public interface IAssociationTrainingModuleCollaboratorsRepository : IGenericRepository<IAssociationTrainingModuleCollaborator, AssociationTrainingModuleCollaboratorVisitor>
    {
        Task<IEnumerable<IAssociationTrainingModuleCollaborator>> GetByTrainingModuleIds(IEnumerable<Guid> trainingModuleIds);
    }
}
