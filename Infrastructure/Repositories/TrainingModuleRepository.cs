using Domain.IRepository;
using Domain.Interfaces;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class TrainingModuleRepository : GenericRepository<ITraningModule, TraningModuleDataModel>, ITrainingModuleRepository
    {
        private IMapper<ITraningModule, TraningModuleDataModel> _mapper;

        public TrainingModuleRepository(DbContext context, IMapper<ITraningModule, TraningModuleDataModel> mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

    }
}