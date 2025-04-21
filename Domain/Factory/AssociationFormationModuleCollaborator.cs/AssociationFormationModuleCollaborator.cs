using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public class AssociationFormationModuleCollaboratorFactory : IAssociationFormationModuleCollaboratorFactory
{
    private ICollaboratorRepository _collaboratorRepository;
    private IFormationModuleRepository _formationModuleRepository;
    private IAssociationFormationModuleCollaboratorRepository _associationFormationModuleRepository;
    public AssociationFormationModuleCollaboratorFactory(ICollaboratorRepository collaboratorRepository, IFormationModuleRepository formationModuleRepository, IAssociationFormationModuleCollaboratorRepository associationFormationModuleCollaboratorRepository)
    {
        _collaboratorRepository = collaboratorRepository;
        _formationModuleRepository = formationModuleRepository;
        _associationFormationModuleRepository = associationFormationModuleCollaboratorRepository;
    }

    public async Task<AssociationFormationModuleCollaborator> Create(long collaboratorId, long formationModuleId, PeriodDate periodDate)
    {
        ICollaborator? collaborator = _collaboratorRepository.GetById(collaboratorId);
        IFormationModule? formationModule = _formationModuleRepository.GetById(formationModuleId);

        if (collaborator == null)
        {
            throw new ArgumentException("Collaborator does not exist.");
        }

        if (formationModule == null)
        {
            throw new ArgumentException("Formation module does not exist.");
        }

        PeriodDateTime periodDateTime = new PeriodDateTime(periodDate);

        if (!collaborator.ContractContainsDates(periodDateTime))
            throw new ArgumentException("Formation module is not between collaborator contact period.");

        return new AssociationFormationModuleCollaborator(collaboratorId, formationModuleId, periodDate);
    }


    public AssociationFormationModuleCollaborator Create(IAssociationFormationModuleCollaboratorVisitor visitor)
    {
        return new AssociationFormationModuleCollaborator(visitor.CollaboratorId, visitor.FormationModuleId, visitor.Period);
    }

}