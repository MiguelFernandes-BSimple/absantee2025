using AutoMapper;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public abstract class GenericRepositoryEF<TDomain, TDataModel> : IGenericRepository<TDomain, TDataModel> where TDomain : class where TDataModel : class
    {
        protected readonly DbContext _context;
        private readonly IMapper _mapper;
        public GenericRepositoryEF(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(TDomain entity)
        {
            var dataModel = _mapper.Map<TDomain, TDataModel>(entity);
            _context.Set<TDataModel>().Add(dataModel);
        }

        public async Task AddAsync(TDomain entity)
        {
            var dataModel = _mapper.Map<TDomain, TDataModel>(entity);
            _context.Set<TDataModel>().Add(dataModel);
            await SaveChangesAsync();
        }

        public void AddRange(IEnumerable<TDomain> entities)
        {
            var dataModels = entities.Select(e => _mapper.Map<TDomain, TDataModel>(e));
            _context.Set<TDataModel>().AddRange(dataModels);
        }

        public async Task AddRangeAsync(IEnumerable<TDomain> entities)
        {
            var dataModels = entities.Select(e => _mapper.Map<TDomain, TDataModel>(e));
            _context.Set<TDataModel>().AddRange(dataModels);
            await SaveChangesAsync();
        }

        public IEnumerable<TDomain> GetAll()
        {
            var dataModels = _context.Set<TDataModel>().ToList();
            return dataModels.Select(d => _mapper.Map<TDataModel, TDomain>(d));
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync()
        {
            var dataModels = await _context.Set<TDataModel>().ToListAsync();
            return dataModels.Select(d => _mapper.Map<TDataModel, TDomain>(d));
        }

        public abstract TDomain? GetById(Guid id);

        public abstract Task<TDomain?> GetByIdAsync(Guid id);

        public void Remove(TDomain entity)
        {
            var dataModel = _mapper.Map<TDomain, TDataModel>(entity);
            _context.Set<TDataModel>().Remove(dataModel);
        }

        public async Task RemoveAsync(TDomain entity)
        {
            var dataModel = _mapper.Map<TDomain, TDataModel>(entity);
            _context.Set<TDataModel>().Remove(dataModel);
            await SaveChangesAsync();
        }

        public void RemoveRange(IEnumerable<TDomain> entities)
        {
            var dataModels = entities.Select(e => _mapper.Map<TDomain, TDataModel>(e));
            _context.Set<TDataModel>().RemoveRange(dataModels);
        }

        public async Task RemoveRangeAsync(IEnumerable<TDomain> entities)
        {
            var dataModels = entities.Select(e => _mapper.Map<TDomain, TDataModel>(e));
            _context.Set<TDataModel>().RemoveRange(dataModels);
            await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
