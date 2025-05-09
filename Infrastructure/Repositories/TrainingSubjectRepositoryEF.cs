using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class TrainingSubjectRepositoryEF : GenericRepositoryEF<TrainingSubject, TrainingSubjectDataModel>, ITrainingSubjectRepository
{
    private readonly IMapper _mapper;
    public TrainingSubjectRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public override TrainingSubject? GetById(Guid id)
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

    public override async Task<TrainingSubject?> GetByIdAsync(Guid id)
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
}
