using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Visitor;
using Domain.Models;

namespace Domain.Factory;

public interface IAssociationCollabTrainingModuleFactory
{
    AssociationCollabTrainingModule Create(long collabId, long trainingModuleId);

    AssociationCollabTrainingModule Create (IAssociationCollabTrainingModuleVisitor visitor);
}
