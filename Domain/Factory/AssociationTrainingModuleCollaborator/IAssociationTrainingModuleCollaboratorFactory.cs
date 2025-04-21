using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Factory;
public interface IAssociationTrainingModuleCollaboratorFactory
{
    long Id { get; }
    long TrainingModuleId { get; }
    long CollaboratorId { get; }
}
