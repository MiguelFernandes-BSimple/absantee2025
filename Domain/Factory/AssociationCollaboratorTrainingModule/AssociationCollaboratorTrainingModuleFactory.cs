using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public class AssociationCollaboratorTrainingModuleFactory : IAssociationCollaboratorTrainingModuleFactory
{   
    private ICollaboratorRepository _collaboratorRepository;

    private ITrainingModuleRepository _trainingModuleRepository;

    private IAssociationCollaboratorTrainingModuleRepository _associationCollaboratorTrainingModuleRepository;

    public AssociationCollaboratorTrainingModuleFactory(ICollaboratorRepository collaboratorRepository,ITrainingModuleRepository trainingModuleRepository,IAssociationCollaboratorTrainingModuleRepository associationCollaboratorTrainingModuleRepository)
    {
        _collaboratorRepository = collaboratorRepository;
        _trainingModuleRepository = trainingModuleRepository;
        _associationCollaboratorTrainingModuleRepository = associationCollaboratorTrainingModuleRepository;

    }


    public async Task<AssociationCollaboratorTrainingModule> Create(long collaboratorId, long trainingModuleId, PeriodDate periodDate)
    {
        ICollaborator? collaborator = _collaboratorRepository.GetById(collaboratorId);
        ITrainingModule? trainingModule = _trainingModuleRepository.GetById(trainingModuleId);

        if (collaborator == null || trainingModule == null)
            throw new ArgumentException("Invalid arguments");
        
        PeriodDateTime periodDateTime = new PeriodDateTime(periodDate);

        if (!collaborator.ContractContainsDates(periodDateTime))
            throw new ArgumentException("Invalid arguments");

        bool canInsert = await _associationCollaboratorTrainingModuleRepository.CanInsert(collaboratorId, trainingModuleId, periodDate);

        if (!canInsert)
            throw new ArgumentException("Invalid arguments");

        return new AssociationCollaboratorTrainingModule(collaboratorId, trainingModuleId, periodDate);
        
    }

    public AssociationCollaboratorTrainingModule Create(IAssociationCollaboratorTrainingModuleVisitor associationCollaboratorTrainingModuleVisitor)
    {
        return new AssociationCollaboratorTrainingModule(
            associationCollaboratorTrainingModuleVisitor.CollaboratorId,
            associationCollaboratorTrainingModuleVisitor.TrainingModuleId,
            associationCollaboratorTrainingModuleVisitor.Period
        );
    }
}