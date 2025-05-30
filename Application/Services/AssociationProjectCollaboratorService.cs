using Domain.Factory;
using Domain.Models;
using Domain.IRepository;
using AutoMapper;
using Application.DTO;

namespace Application.Services
{
    public class AssociationProjectCollaboratorService
    {
        private IAssociationProjectCollaboratorRepository _assocRepository;
        private IAssociationProjectCollaboratorFactory _associationProjectCollaboratorFactory;
        private readonly IMapper _mapper;

        public AssociationProjectCollaboratorService(IAssociationProjectCollaboratorRepository assocRepository, IAssociationProjectCollaboratorFactory associationProjectCollaboratorFactory, IMapper mapper)
        {
            _assocRepository = assocRepository;
            _associationProjectCollaboratorFactory = associationProjectCollaboratorFactory;
            _mapper = mapper;
        }

        public async Task<Result<AssociationProjectCollaboratorDTO>> Add(PeriodDate periodDate, Guid collabId, Guid projectId)
        {
            try
            {
                var assoc = await _associationProjectCollaboratorFactory.Create(periodDate, collabId, projectId);
                var assocCreated = await _assocRepository.AddAsync(assoc);
                var result = _mapper.Map<AssociationProjectCollaborator, AssociationProjectCollaboratorDTO>((AssociationProjectCollaborator)assocCreated);
                return Result<AssociationProjectCollaboratorDTO>.Success(result);
            } catch(ArgumentException a)
            {
                return Result<AssociationProjectCollaboratorDTO>.Failure(Error.BadRequest(a.Message));
            }
            catch (Exception ex)
            {
                return Result<AssociationProjectCollaboratorDTO>.Failure(Error.InternalServerError(ex.Message));
            }
        }
    }
}
