using Domain.Interfaces;
using Domain.IRepository;
using Domain.Visitor;
using Infrastructure;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class TrainingSubjectRepositoryEF : GenericRepository<ITrainingSubject, ITrainingSubjectVisitor>, ITrainingSubjectRepository
{
    private readonly IMapper<ITrainingSubject, ITrainingSubjectVisitor> _mapper;
    public TrainingSubjectRepositoryEF(AbsanteeContext context, IMapper<ITrainingSubject, ITrainingSubjectVisitor> mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }
    public async Task<ITrainingSubject?> FindByTitle(string title)
    {
        var result = await _context.Set<TrainingSubjectDataModel>()
                             .FirstOrDefaultAsync(t => t.Title == title);

        if (result == null)
            return null;

        var ts = _mapper.ToDomain(result);

        return ts;
    }

    public override ITrainingSubject? GetById(long id)
    {
        var tsDM = _context.Set<TrainingSubjectDataModel>()
                           .FirstOrDefault(t => t.Id == id);

        if (tsDM == null)
            return null;

        var ts = _mapper.ToDomain(tsDM);
        return ts;
    }

    public override async Task<ITrainingSubject?> GetByIdAsync(long id)
    {
        var tsDM = await _context.Set<TrainingSubjectDataModel>()
                           .FirstOrDefaultAsync(t => t.Id == id);

        if (tsDM == null)
            return null;

        var ts = _mapper.ToDomain(tsDM);
        return ts;
    }
}