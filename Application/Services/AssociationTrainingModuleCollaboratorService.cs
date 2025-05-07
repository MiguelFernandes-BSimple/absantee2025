using Application.DTO.AssociationTrainingModuleCollaborator;
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

    public async Task<Result<AssociationTrainingModuleCollaboratorDTO>> Add(Guid tmId, CreateAssociationTrainingModuleCollaboratorDTO assocDTO)
    {
        AssociationTrainingModuleCollaborator tmc;

        try
        {
            tmc = await _assocTMCFactory.Create(tmId, assocDTO.CollaboratorId);
            tmc = await _assocTMCRepository.AddAsync(tmc);
        }
        catch (ArgumentException a)
        {
            return Result<AssociationTrainingModuleCollaboratorDTO>.Failure(Error.BadRequest(a.Message));
        }
        catch (Exception e)
        {
            return Result<AssociationTrainingModuleCollaboratorDTO>.Failure(Error.BadRequest(e.Message));
        }

        var result = _mapper.Map<AssociationTrainingModuleCollaborator, AssociationTrainingModuleCollaboratorDTO>(tmc);
        return Result<AssociationTrainingModuleCollaboratorDTO>.Success(result);
    }
}