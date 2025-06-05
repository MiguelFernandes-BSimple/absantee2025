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
        private IProjectRepository _projectRepository;
        private ICollaboratorRepository _collaboratorRepository;
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AssociationProjectCollaboratorService(IAssociationProjectCollaboratorRepository assocRepository, IAssociationProjectCollaboratorFactory associationProjectCollaboratorFactory, ICollaboratorRepository collabRepository, IUserRepository userRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _assocRepository = assocRepository;
            _associationProjectCollaboratorFactory = associationProjectCollaboratorFactory;
            _collaboratorRepository = collabRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
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

        public async Task<Result<AssociationProjectCollaboratorDTO>> AddByCollaborator(PeriodDate periodDate, Guid collabId, Guid projectId)
        {
            try
            {
                var assoc = await _associationProjectCollaboratorFactory.Create(periodDate, collabId, projectId);
                var assocCreated = await _assocRepository.AddAsync(assoc);
                var project = await _projectRepository.GetByIdAsync(projectId);
                var result = _mapper.Map<AssociationProjectCollaborator, AssociationProjectCollaboratorDTO>((AssociationProjectCollaborator)assocCreated);
                result.ProjectAcronym = project!.Acronym;
                return Result<AssociationProjectCollaboratorDTO>.Success(result);
            }
            catch (ArgumentException a)
            {
                return Result<AssociationProjectCollaboratorDTO>.Failure(Error.BadRequest(a.Message));
            }
            catch (Exception ex)
            {
                return Result<AssociationProjectCollaboratorDTO>.Failure(Error.InternalServerError(ex.Message));
            }
        }

        public async Task<Result<AssociationProjectCollaboratorDTO>> AddByProject(PeriodDate periodDate, Guid collabId, Guid projectId)
        {
            try
            {
                var assoc = await _associationProjectCollaboratorFactory.Create(periodDate, collabId, projectId);
                var assocCreated = await _assocRepository.AddAsync(assoc);
                var collab = await _collaboratorRepository.GetByIdAsync(collabId);
                var user = await _userRepository.GetByIdAsync(collab!.UserId);
                var result = _mapper.Map<AssociationProjectCollaborator, AssociationProjectCollaboratorDTO>((AssociationProjectCollaborator)assocCreated);
                result.CollaboratorEmail = user!.Email;
                return Result<AssociationProjectCollaboratorDTO>.Success(result);
            }
            catch (ArgumentException a)
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
