using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TrainingModuleCollaboratorsRepository : GenericRepository<ITrainingModuleCollaborators, ITrainingModuleCollaboratorsVisitor>, ITrainingModuleCollaboratorsRepository
    {
        private readonly IMapper<ITrainingModuleCollaborators, ITrainingModuleCollaboratorsVisitor> _mapper;
        public TrainingModuleCollaboratorsRepository(DbContext context, IMapper<ITrainingModuleCollaborators, ITrainingModuleCollaboratorsVisitor> mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public override ITrainingModuleCollaborators? GetById(long id)
        {
            var trainingModuleCollabDM = _context.Set<TrainingModuleCollaboratorDataModel>()
                                    .FirstOrDefault(t => t.Id == id);

            if (trainingModuleCollabDM == null)
                return null;

            return _mapper.ToDomain(trainingModuleCollabDM);
        }

        public override async Task<ITrainingModuleCollaborators?> GetByIdAsync(long id)
        {
            var trainingModuleCollabDM = await _context.Set<TrainingModuleCollaboratorDataModel>()
                                    .FirstOrDefaultAsync(t => t.Id == id);

            if (trainingModuleCollabDM == null)
                return null;

            return _mapper.ToDomain(trainingModuleCollabDM);
        }

        public async Task<IEnumerable<ITrainingModuleCollaborators>> GetByTrainingModuleIds(IEnumerable<long> trainingModuleIds)
        {
            var trainingModuleCollaboratorsDMs = await _context.Set<TrainingModuleCollaboratorDataModel>()
                                                .Where(t => trainingModuleIds.Contains(t.TrainingModuleId))
                                                .ToListAsync();

            var trainingModuleCollaborators = _mapper.ToDomain(trainingModuleCollaboratorsDMs);

            return trainingModuleCollaborators;
        }
    }
}
