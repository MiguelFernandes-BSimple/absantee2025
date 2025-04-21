using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public class AssociationCollabTrainingModuleFactory : IAssociationCollabTrainingModuleFactory
    {
        private readonly ITrainingModuleRepository _trainigModuleRepository;
        private readonly ICollaboratorRepository _collabRepository;
        private readonly IAssociationCollabTrainingModuleRepository _associationRepository;

        public AssociationCollabTrainingModuleFactory(ITrainingModuleRepository trainigModuleRepository, ICollaboratorRepository collabRepository, IAssociationCollabTrainingModuleRepository associationRepository)
        {
            _trainigModuleRepository = trainigModuleRepository;
            _collabRepository = collabRepository;
            _associationRepository = associationRepository;
        }

        public AssociationCollabTrainingModule Create(long collabId, long trainingModuleId)
        {
            var collabExists = _collabRepository.GetById(collabId);
            if(collabExists == null) 
                throw new ArgumentException("Collaborator does not exist");

            if(_trainigModuleRepository.GetById(trainingModuleId) == null) 
                throw new ArgumentException("Training Module does not exist");
            
            if(!_associationRepository.CheckIfCanAdd(collabId, trainingModuleId))
                throw new ArgumentException("Invalid Arguments");

            return new AssociationCollabTrainingModule(collabId, trainingModuleId);
        }

        public AssociationCollabTrainingModule Create(IAssociationCollabTrainingModuleVisitor visitor)
        {
            return new AssociationCollabTrainingModule(visitor._id, visitor._collaboratorId, visitor._trainingModuleId);
        }
    } 
}