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

        public async Task<AssociationProjectCollaboratorDTO> Add(PeriodDate periodDate, Guid collabId, Guid projectId)
        {
            var assoc = await _associationProjectCollaboratorFactory.Create(periodDate, collabId, projectId);
            var assocCreated = await _assocRepository.AddAsync(assoc);
            return _mapper.Map<AssociationProjectCollaborator, AssociationProjectCollaboratorDTO>(assocCreated);
        }
    }
}
