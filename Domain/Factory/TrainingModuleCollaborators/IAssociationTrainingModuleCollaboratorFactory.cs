using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public interface IAssociationTrainingModuleCollaboratorFactory
    {
        Task<AssociationTrainingModuleCollaborator> Create(long trainingModuleId, long collaboratorId);
        AssociationTrainingModuleCollaborator Create(AssociationTrainingModuleCollaboratorVisitor visitor);
    }
}
