using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.IRepository;

namespace Domain.Factory.AssociationCollabTrainingModule
{
    public class AssociationCollabTrainingModuleFactory : IAssociationProjectCollaboratorFactory
    {
        private readonly ITrainingModuleRepository _trainingModuleFactory;
        private readonly ICollaboratorRepository _collabRepository;
    }
}