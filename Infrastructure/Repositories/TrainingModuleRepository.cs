using Domain.IRepository;
using Domain.Interfaces;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TrainingModuleRepository : GenericRepository<ITrainingModule, TraningModuleDataModel>, ITrainingModuleRepository
    {
        private readonly TrainingModuleMapper _mapper;

        public TrainingModuleRepository(AbsanteeContext context, TrainingModuleMapper mapper) : base(context, (IMapper<ITrainingModule, TraningModuleDataModel>)mapper)
        {
            _mapper = mapper;
        }

        public override ITrainingModule? GetById(long id)
        {
            var tmDm = _context.Set<TraningModuleDataModel>().FirstOrDefault(tm => tm.id == id);

            if(tmDm == null) return null;

            var trainingModule = _mapper.ToDomain(tmDm);
            return trainingModule;
        }

        public override async Task<ITrainingModule?> GetByIdAsync(long id)
        {
            var tmDm = await _context.Set<TraningModuleDataModel>().FirstOrDefaultAsync(tm => tm.id == id);

            if(tmDm == null) return null;

            var trainingModule = _mapper.ToDomain(tmDm);
            return trainingModule;
        }
    }
}