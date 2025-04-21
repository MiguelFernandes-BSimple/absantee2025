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
    public AssociationTrainingModuleCollaboratorFactory(ICollaboratorRepository collaboratorRepository, ITrainingModuleRepository TrainingModuleRepository)
    {
        _collaboratorRepository = collaboratorRepository;
        _trainingModuleRepository = TrainingModuleRepository;
    }

    public async Task<AssociationTrainingModuleCollaborator> Create(long collaboratorId, long trainingModuleId, PeriodDateTime periodDateTime)
    {
        var trainingModule = await _trainingModuleRepository.GetByIdAsync(trainingModuleId);
        var collaborator = await _collaboratorRepository.GetByIdAsync(collaboratorId);

        if (trainingModule == null)
            throw new ArgumentException("Training Module must exists");

        if (collaborator == null)
            throw new ArgumentException("Collaborator must exists");

        return new AssociationTrainingModuleCollaborator(collaboratorId, trainingModuleId, periodDateTime);
    }
    public AssociationTrainingModuleCollaborator Create(IAssociationTrainingModuleCollaboratorVisitor AssociationTrainingModuleCollaboratorVisitor)
    {
        return new AssociationTrainingModuleCollaborator(
                    AssociationTrainingModuleCollaboratorVisitor.CollaboratorId,
                    AssociationTrainingModuleCollaboratorVisitor.TrainingModuleId,
                    AssociationTrainingModuleCollaboratorVisitor.PeriodDateTime);
    }

}