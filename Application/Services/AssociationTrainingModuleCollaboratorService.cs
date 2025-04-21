using Domain.Factory;
using Domain.Models;
using Domain.IRepository;

namespace Application.Services;
public class AssociationTrainingModuleCollaboratorService
{
    private IAssociationTrainingModuleCollaboratorRepository _assocRepository;
    private IAssociationTrainingModuleCollaboratorFactory _assocFactory;

    public AssociationTrainingModuleCollaboratorService(IAssociationTrainingModuleCollaboratorRepository assocRepository, IAssociationTrainingModuleCollaboratorFactory assocFactory)
    {
        _assocRepository = assocRepository;
        _assocFactory = assocFactory;
    }

    public async Task Add(long collabId, long trainingModuleId, PeriodDateTime periodDateTime)
    {
        var assoc = await _assocFactory.Create(collabId, trainingModuleId, periodDateTime);
        await _assocRepository.AddAsync(assoc);
    }
}

