using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public class AssociationTrainingModuleCollaboratorFactory : IAssociationTrainingModuleCollaboratorFactory
{
    private readonly IAssociationTrainingModuleCollaboratorRepository _assocTCRepository;
    private readonly ITrainingModuleRepository _trainingModuleRepository;
    private readonly ICollaboratorRepository _collabRepository;
    public AssociationTrainingModuleCollaboratorFactory(IAssociationTrainingModuleCollaboratorRepository associationTrainingModuleCollaboratorRepository, ITrainingModuleRepository trainingModuleRepository, ICollaboratorRepository collaboratorRepository)
    {
        _assocTCRepository = associationTrainingModuleCollaboratorRepository;
        _trainingModuleRepository = trainingModuleRepository;
        _collabRepository = collaboratorRepository;
    }

    public async Task<AssociationTrainingModuleCollaborator> Create(long trainingModuleId, long collaboratorId)
    {
        ITrainingModule? tm = await _trainingModuleRepository.GetByIdAsync(trainingModuleId);
        ICollaborator? collab = await _collabRepository.GetByIdAsync(collaboratorId);

        if (tm == null || collab == null)
            throw new ArgumentException("Invalid inputs");

        //Unicity test
        IAssociationTrainingModuleCollaborator? repeated = await _assocTCRepository.FindByCollaborator(collaboratorId);

        if (repeated != null)
            throw new ArgumentException("Invalid inputs");

        return new AssociationTrainingModuleCollaborator(trainingModuleId, collaboratorId);
    }

    public AssociationTrainingModuleCollaborator Create(IAssociationTrainingModuleCollaboratorVisitor associationTrainingModuleCollaboratorVisitor)
    {
        return new AssociationTrainingModuleCollaborator(associationTrainingModuleCollaboratorVisitor.TrainingModuleId, associationTrainingModuleCollaboratorVisitor.CollaboratorId);
    }
}