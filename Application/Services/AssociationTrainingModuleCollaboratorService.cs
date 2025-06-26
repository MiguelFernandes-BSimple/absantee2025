using Application.DTO.AssociationTrainingModuleCollaborator;
using AutoMapper;
using Domain.Factory;
using Domain.Interfaces;
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
    public async Task SubmitAsync(Guid collaboratorId, Guid trainingModuleId)
    {
        var association = await _assocTMCFactory.Create(collaboratorId, trainingModuleId);
        await _assocTMCRepository.AddAsync(association);
        await _assocTMCRepository.SaveChangesAsync();
    }

    public async Task<Result<AssociationTrainingModuleCollaboratorDTO>> Add(AssociationTrainingModuleCollaboratorDTO assocTMCDTO)
    {
        IAssociationTrainingModuleCollaborator assocTMC;

        try
        {
            assocTMC = await _assocTMCFactory.Create(assocTMCDTO.CollaboratorId, assocTMCDTO.TrainingModuleId);
            assocTMC = await _assocTMCRepository.AddAsync(assocTMC);
        }
        catch (ArgumentException a)
        {
            return Result<AssociationTrainingModuleCollaboratorDTO>.Failure(Error.BadRequest(a.Message));
        }
        catch (Exception e)
        {
            return Result<AssociationTrainingModuleCollaboratorDTO>.Failure(Error.BadRequest(e.Message));
        }

        var result = _mapper.Map<AssociationTrainingModuleCollaborator, AssociationTrainingModuleCollaboratorDTO>((AssociationTrainingModuleCollaborator)assocTMC);
        if (result == null)
        {
            return Result<AssociationTrainingModuleCollaboratorDTO>.Failure(Error.InternalServerError("Mapping failed"));
        }
        return Result<AssociationTrainingModuleCollaboratorDTO>.Success(result);
    }
}