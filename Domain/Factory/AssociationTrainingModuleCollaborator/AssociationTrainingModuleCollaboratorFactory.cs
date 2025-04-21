using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public class AssociationTrainingModuleCollaboratorFactory : IAssociationTrainingModuleCollaboratorFactory
{
    private ICollaboratorRepository _collaboratorRepository;
    private ITrainingModuleRepository _trainingModuleRepository;
    private IAssociationTrainingModuleCollaboratorRepository _assocRepository;

    public AssociationTrainingModuleCollaboratorFactory(ICollaboratorRepository collaboratorRepository, ITrainingModuleRepository trainingModuleRepository, IAssociationTrainingModuleCollaboratorRepository assocRepository)
    {
        _collaboratorRepository = collaboratorRepository;
        _trainingModuleRepository = trainingModuleRepository;
        _assocRepository = assocRepository;
    }

    public async Task<AssociationTrainingModuleCollaborator> Create(long collaboratorId, long trainingModuleId, PeriodDateTime periodDateTime)
    {
        ICollaborator? collaborator = _collaboratorRepository.GetById(collaboratorId);
        ITrainingModule? trainingModule = _trainingModuleRepository.GetById(trainingModuleId);

        if (collaborator == null || trainingModule == null)
            throw new ArgumentException("Invalid arguments");

        if (!collaborator.ContractContainsDates(periodDateTime))
            throw new ArgumentException("Invalid arguments");

        bool canInsert = await _assocRepository.CanInsert(collaboratorId, trainingModuleId, periodDateTime);

        if (!canInsert)
            throw new ArgumentException("Invalid arguments");

        return new AssociationTrainingModuleCollaborator(collaboratorId, trainingModuleId, periodDateTime);
    }

    public AssociationTrainingModuleCollaborator Create(IAssociationTrainingModuleCollaboratorVisitor assocVisitor)
    {
        return new AssociationTrainingModuleCollaborator(assocVisitor.CollaboratorId, assocVisitor.TrainingModuleId, assocVisitor.PeriodDateTime);
    }
}