using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AssociationTrainingModuleCollaboratorRepositoryEF : GenericRepositoryEF<AssociationTrainingModuleCollaborator, AssociationTrainingModuleCollaboratorDataModel>, IAssociationTrainingModuleCollaboratorsRepository
    {
        private readonly IMapper _mapper;
        public AssociationTrainingModuleCollaboratorRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public override AssociationTrainingModuleCollaborator? GetById(Guid id)
        {
            var trainingModuleCollabDM = _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                                    .FirstOrDefault(t => t.Id == id);

            if (trainingModuleCollabDM == null)
                return null;

            return _mapper.Map<AssociationTrainingModuleCollaboratorDataModel, AssociationTrainingModuleCollaborator>(trainingModuleCollabDM);
        }

        public override async Task<AssociationTrainingModuleCollaborator?> GetByIdAsync(Guid id)
        {
            var trainingModuleCollabDM = await _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                                    .FirstOrDefaultAsync(t => t.Id == id);

            if (trainingModuleCollabDM == null)
                return null;

            return _mapper.Map<AssociationTrainingModuleCollaboratorDataModel, AssociationTrainingModuleCollaborator>(trainingModuleCollabDM);
        }

        public async Task<IEnumerable<AssociationTrainingModuleCollaborator>> GetByTrainingModuleIds(IEnumerable<Guid> trainingModuleIds)
        {
            var trainingModuleCollaboratorsDMs = await _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                                                .Where(t => trainingModuleIds.Contains(t.TrainingModuleId))
                                                .ToListAsync();

            var trainingModuleCollaborators = trainingModuleCollaboratorsDMs.Select(_mapper.Map<AssociationTrainingModuleCollaboratorDataModel, AssociationTrainingModuleCollaborator>);

            return trainingModuleCollaborators;
        }
    }
}
