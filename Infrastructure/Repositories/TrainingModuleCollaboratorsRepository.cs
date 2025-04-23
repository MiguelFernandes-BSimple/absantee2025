using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TrainingModuleCollaboratorsRepository : GenericRepository<ITrainingModuleCollaborators, ITrainingModuleCollaboratorsVisitor>, ITrainingModuleCollaboratorsRepository
    {
        private readonly IMapper _mapper;
        public TrainingModuleCollaboratorsRepository(AbsanteeContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public override ITrainingModuleCollaborators? GetById(long id)
        {
            var trainingModuleCollabDM = _context.Set<TrainingModuleCollaboratorDataModel>()
                                    .FirstOrDefault(t => t.Id == id);

            if (trainingModuleCollabDM == null)
                return null;

            return _mapper.Map<TrainingModuleCollaboratorDataModel, TrainingModuleCollaborators>(trainingModuleCollabDM);
        }

        public override async Task<ITrainingModuleCollaborators?> GetByIdAsync(long id)
        {
            var trainingModuleCollabDM = await _context.Set<TrainingModuleCollaboratorDataModel>()
                                    .FirstOrDefaultAsync(t => t.Id == id);

            if (trainingModuleCollabDM == null)
                return null;

            return _mapper.Map<TrainingModuleCollaboratorDataModel, TrainingModuleCollaborators>(trainingModuleCollabDM);
        }

        public async Task<IEnumerable<ITrainingModuleCollaborators>> GetByTrainingModuleIds(IEnumerable<long> trainingModuleIds)
        {
            var trainingModuleCollaboratorsDMs = await _context.Set<TrainingModuleCollaboratorDataModel>()
                                                .Where(t => trainingModuleIds.Contains(t.TrainingModuleId))
                                                .ToListAsync();

            var trainingModuleCollaborators = trainingModuleCollaboratorsDMs.Select(t => _mapper.Map<TrainingModuleCollaboratorDataModel, TrainingModuleCollaborators>(t));

            return trainingModuleCollaborators;
        }
    }
}
