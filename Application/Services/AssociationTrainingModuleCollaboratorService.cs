using Application.DTO.AssociationTrainingModuleCollaborator;
using Application.DTO.TrainingModule;
using AutoMapper;
using Domain.Factory;
using Domain.IRepository;
using Domain.Models;

namespace Application.Services;

public class AssociationTrainingModuleCollaboratorService
{
    public IAssociationTrainingModuleCollaboratorsRepository _assocTMCRepository;
    public IAssociationTrainingModuleCollaboratorFactory _assocTMCFactory;
    public IMapper _mapper;

    public AssociationTrainingModuleCollaboratorService(IAssociationTrainingModuleCollaboratorsRepository associationTrainingModuleCollaboratorsRepository, IAssociationTrainingModuleCollaboratorFactory associationTrainingModuleCollaboratorFactory, IMapper mapper)
    {
        _assocTMCRepository = associationTrainingModuleCollaboratorsRepository;
        _assocTMCFactory = associationTrainingModuleCollaboratorFactory;
        _mapper = mapper;
    }

    public async Task<AssociationTrainingModuleCollaboratorDTO> Add(Guid tmId, CreateAssociationTrainingModuleCollaboratorDTO assocDTO)
    {
        AssociationTrainingModuleCollaborator tmc;

        tmc = await _assocTMCFactory.Create(tmId, assocDTO.CollaboratorId);
        tmc = await _assocTMCRepository.AddAsync(tmc);

        return _mapper.Map<AssociationTrainingModuleCollaborator, AssociationTrainingModuleCollaboratorDTO>(tmc);
    }
}