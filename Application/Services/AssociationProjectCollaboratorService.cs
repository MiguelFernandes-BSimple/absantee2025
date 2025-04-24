using Domain.Factory;
using Domain.Models;
using Domain.IRepository;

namespace Application.Services
{
    public class AssociationProjectCollaboratorService
    {
        private IAssociationProjectCollaboratorRepository _assocRepository;
        private IAssociationProjectCollaboratorFactory _associationProjectCollaboratorFactory;

        public AssociationProjectCollaboratorService(IAssociationProjectCollaboratorRepository assocRepository, IAssociationProjectCollaboratorFactory associationProjectCollaboratorFactory)
        {
            _assocRepository = assocRepository;
            _associationProjectCollaboratorFactory = associationProjectCollaboratorFactory;
        }

        public async Task<bool> Add(PeriodDate periodDate, Guid collabId, Guid projectId)
        {
            try
            {
                var assoc = await _associationProjectCollaboratorFactory.Create(periodDate, collabId, projectId);
                await _assocRepository.AddAsync(assoc);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
