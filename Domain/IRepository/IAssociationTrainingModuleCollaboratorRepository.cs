using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.IRepository
{
    public interface IAssociationTrainingModuleCollaboratorRepository
    {
        Task<IEnumerable<IAssociationTrainingModuleCollaborator>> GetByTrainingModuleIds(IEnumerable<long> trainingModuleIds);
    }
}