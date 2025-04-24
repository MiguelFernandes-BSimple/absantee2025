using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AssociationTrainingModuleCollaboratorRepository : GenericRepository<IAssociationTrainingModuleCollaborator, AssociationTrainingModuleCollaboratorVisitor>, IAssociationTrainingModuleCollaboratorsRepository
    {
        private readonly IMapper _mapper;
        public AssociationTrainingModuleCollaboratorRepository(AbsanteeContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public override IAssociationTrainingModuleCollaborator? GetById(Guid id)
        {
            var trainingModuleCollabDM = _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                                    .FirstOrDefault(t => t.Id == id);

            if (trainingModuleCollabDM == null)
                return null;

            return _mapper.Map<AssociationTrainingModuleCollaboratorDataModel, AssociationTrainingModuleCollaborator>(trainingModuleCollabDM);
        }

        public override async Task<IAssociationTrainingModuleCollaborator?> GetByIdAsync(Guid id)
        {
            var trainingModuleCollabDM = await _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                                    .FirstOrDefaultAsync(t => t.Id == id);

            if (trainingModuleCollabDM == null)
                return null;

            return _mapper.Map<AssociationTrainingModuleCollaboratorDataModel, AssociationTrainingModuleCollaborator>(trainingModuleCollabDM);
        }

        public async Task<IEnumerable<IAssociationTrainingModuleCollaborator>> GetByTrainingModuleIds(IEnumerable<Guid> trainingModuleIds)
        {
            var trainingModuleCollaboratorsDMs = await _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                                                .Where(t => trainingModuleIds.Contains(t.TrainingModuleId))
                                                .ToListAsync();

            var trainingModuleCollaborators = trainingModuleCollaboratorsDMs.Select(t => _mapper.Map<AssociationTrainingModuleCollaboratorDataModel, AssociationTrainingModuleCollaborator>(t));

            return trainingModuleCollaborators;
        }
    }
}
