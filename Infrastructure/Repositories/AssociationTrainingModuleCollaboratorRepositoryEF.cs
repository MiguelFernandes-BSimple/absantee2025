using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AssociationTrainingModuleCollaboratorRepositoryEF : GenericRepositoryEF<IAssociationTrainingModuleCollaborator, AssociationTrainingModuleCollaborator, AssociationTrainingModuleCollaboratorDataModel>, IAssociationTrainingModuleCollaboratorsRepository
    {
        private readonly IMapper _mapper;
        public AssociationTrainingModuleCollaboratorRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public AssociationTrainingModuleCollaboratorRepositoryEF(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public IAssociationTrainingModuleCollaborator Add(IAssociationTrainingModuleCollaborator entity)
        {
            throw new NotImplementedException();
        }

        public Task<IAssociationTrainingModuleCollaborator> AddAsync(IAssociationTrainingModuleCollaborator entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<IAssociationTrainingModuleCollaborator> entities)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<IAssociationTrainingModuleCollaborator> entities)
        {
            throw new NotImplementedException();
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

        public async Task<IEnumerable<AssociationTrainingModuleCollaborator>> GetByTrainingModuleIds(IEnumerable<Guid> trainingModuleIds)
        {
            var trainingModuleCollaboratorsDMs = await _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                                                .Where(t => trainingModuleIds.Contains(t.TrainingModuleId))
                                                .ToListAsync();

            var trainingModuleCollaborators = trainingModuleCollaboratorsDMs.Select(_mapper.Map<AssociationTrainingModuleCollaboratorDataModel, AssociationTrainingModuleCollaborator>);

            return trainingModuleCollaborators;
        }

        public void Remove(IAssociationTrainingModuleCollaborator entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IAssociationTrainingModuleCollaborator entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<IAssociationTrainingModuleCollaborator> entities)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<IAssociationTrainingModuleCollaborator> entities)
        {
            throw new NotImplementedException();
        }
    }
}
