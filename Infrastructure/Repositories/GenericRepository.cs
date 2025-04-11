using Domain.IRepository;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public abstract class GenericRepository<TDomain, TDataModel> : IGenericRepository<TDomain, TDataModel> where TDomain : class where TDataModel : class
    {
        protected readonly DbContext _context;
        private readonly IMapper<TDomain, TDataModel> _mapper;
        public GenericRepository(DbContext context, IMapper<TDomain, TDataModel> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(TDomain entity)
        {
            var dataModel = _mapper.ToDataModel(entity);
            _context.Set<TDataModel>().Add(dataModel);
        }

        public void AddRange(IEnumerable<TDomain> entities)
        {
            var dataModels = entities.Select(e => _mapper.ToDataModel(e));
            _context.Set<TDataModel>().AddRange(dataModels);
        }

        public IEnumerable<TDomain> GetAll()
        {
            var dataModels = _context.Set<TDataModel>().ToList();
            return _mapper.ToDomain(dataModels);
        }

        public abstract TDomain? GetById(long id);

        public void Remove(TDomain entity)
        {
            var dataModel = _mapper.ToDataModel(entity);
            _context.Set<TDataModel>().Remove(dataModel);
        }

        public void RemoveRange(IEnumerable<TDomain> entities)
        {
            var dataModels = _mapper.ToDataModel(entities);
            _context.Set<TDataModel>().RemoveRange(dataModels);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
