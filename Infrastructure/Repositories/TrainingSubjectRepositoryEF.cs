using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TrainingSubjectRepositoryEF : GenericRepositoryEF<ITrainingSubject, TrainingSubject, TrainingSubjectDataModel>, ITrainingSubjectRepository
{
    private readonly IMapper _mapper;
    public TrainingSubjectRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public TrainingSubjectRepositoryEF(DbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public ITrainingSubject Add(ITrainingSubject entity)
    {
        throw new NotImplementedException();
    }

    public Task<ITrainingSubject> AddAsync(ITrainingSubject entity)
    {
        throw new NotImplementedException();
    }

    public void AddRange(IEnumerable<ITrainingSubject> entities)
    {
        throw new NotImplementedException();
    }

    public Task AddRangeAsync(IEnumerable<ITrainingSubject> entities)
    {
        throw new NotImplementedException();
    }

    public override ITrainingSubject? GetById(Guid id)
    {
        try
        {
            var tsDM = _context.Set<TrainingSubjectDataModel>()
                               .FirstOrDefault();

            if (tsDM == null)
                return null;

            var ts = _mapper.Map<TrainingSubjectDataModel, TrainingSubject>(tsDM);
            return ts;
        }
        catch
        {
            throw;
        }
    }

    public override async Task<ITrainingSubject?> GetByIdAsync(Guid id)
    {
        try
        {
            var tsDM = await _context.Set<TrainingSubjectDataModel>()
                               .FirstOrDefaultAsync(ts => ts.Id == id);

            if (tsDM == null)
                return null;

            var ts = _mapper.Map<TrainingSubjectDataModel, TrainingSubject>(tsDM);
            return ts;
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> IsDuplicated(string subject)
    {
        return await _context.Set<TrainingSubjectDataModel>()
                       .AnyAsync(t => t.Subject.Equals(subject));
    }

    public void Remove(ITrainingSubject entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(ITrainingSubject entity)
    {
        throw new NotImplementedException();
    }

    public void RemoveRange(IEnumerable<ITrainingSubject> entities)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRangeAsync(IEnumerable<ITrainingSubject> entities)
    {
        throw new NotImplementedException();
    }
}
