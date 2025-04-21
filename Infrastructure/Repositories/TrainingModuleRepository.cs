using Domain.IRepository;
using Domain.Interfaces;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TrainingModuleRepository : GenericRepository<ITrainingModule, TraningModuleDataModel>, ITrainingModuleRepository
    {
        private IMapper<ITrainingModule, TraningModuleDataModel> _mapper;

        public TrainingModuleRepository(AbsanteeContext context, TrainingModuleMapper mapper) : base(context, (IMapper<ITrainingModule, TraningModuleDataModel>)mapper)
        {
            _mapper = mapper;
        }

        public override ITrainingModule? GetById(long id)
        {
            throw new NotImplementedException();
        }

        public override Task<ITrainingModule?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}