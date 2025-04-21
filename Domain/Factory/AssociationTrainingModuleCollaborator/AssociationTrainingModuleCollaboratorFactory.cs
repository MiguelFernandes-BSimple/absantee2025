using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public class AssociationTrainingModuleCollaboratorFactory : IAssociationTrainingModuleCollaboratorFactory
{
    private readonly ICollaboratorRepository _collaboratorRepository;
    private readonly ITrainingModuleRepository _trainingModuleRepository;
    public AssociationTrainingModuleCollaboratorFactory(ICollaboratorRepository collaboratorRepository, ITrainingModuleCollaboratorRepository TrainingModuleCollaboratorRepository)
    {
        _collaboratorRepository = collaboratorRepository;
        _trainingModuleRepository = TrainingModuleCollaboratorRepository;
    }
    public async Task<AssociationTrainingModuleCollaborator> Create(long trainingModuleId, long collaboratorId)
    {
        var trainingModule = await _trainingModuleRepository.GetByIdAsync(trainingModuleId);
        var collab = await _collaboratorRepository.GetByIdAsync(collaboratorId);

        if (trainingModule == null)
            throw new ArgumentException("Training Module must exists");

        if (collab == null)
            throw new ArgumentException("Collaborator must exists");

        return new TrainingModuleCollaborators(trainingModuleId, collaboratorId);
    }
    public AssociationTrainingModuleCollaborator Create(IAssociationTrainingModuleCollaboratorVisitor AssociationTrainingModuleCollaboratorVisitor)
    {
        return new AssociationTrainingModuleCollaborator(
                    AssociationTrainingModuleCollaboratorVisitor.Id,
                    AssociationTrainingModuleCollaboratorVisitor.CollaboratorId,
                    AssociationTrainingModuleCollaboratorVisitor.TrainingModuleId,
                    AssociationTrainingModuleCollaboratorVisitor.PeriodDateTime);
    }
}