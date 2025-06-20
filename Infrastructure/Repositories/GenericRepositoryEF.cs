using AutoMapper;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public abstract class GenericRepositoryEF<TInterface, TDomain, TDataModel>
        where TInterface : class
        where TDomain : class, TInterface
        where TDataModel : class
    {
        protected readonly DbContext _context;
        private readonly IMapper _mapper;
        public GenericRepositoryEF(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<TInterface> GetAll()
        {
            var dataModels = _context.Set<TDataModel>().ToList();
            return dataModels.Select(d => _mapper.Map<TDataModel, TDomain>(d));
        }

        public async Task<IEnumerable<TInterface>> GetAllAsync()
        {
            var dataModels = await _context.Set<TDataModel>().ToListAsync();
            var returned = dataModels.Select(d => _mapper.Map<TDataModel, TDomain>(d));
            return returned;
        }

        public abstract TInterface? GetById(Guid id);

        public abstract Task<TInterface?> GetByIdAsync(Guid id);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
