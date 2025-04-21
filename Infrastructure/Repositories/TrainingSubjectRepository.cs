using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TrainingSubjectRepository : GenericRepository<ITrainingSubject, ITrainingSubjectVisitor>, ITrainingSubjectRepository
{
    private readonly IMapper<ITrainingSubject, ITrainingSubjectVisitor> _mapper;
    public TrainingSubjectRepository(AbsanteeContext context, IMapper<ITrainingSubject, ITrainingSubjectVisitor> mapper) : base(context, mapper) {
        _mapper = mapper;
    }

    public override ITrainingSubject? GetById(long id)
    {
        var subjectDM = _context.Set<TrainingSubjectDataModel>()
            .FirstOrDefault(ts => ts.Id == id);

        if(subjectDM == null)
            return null;
        
        var subject = _mapper.ToDomain(subjectDM);
        return subject;
    }

    public override async Task<ITrainingSubject?> GetByIdAsync(long id)
    {
        var subjectDM = await _context.Set<TrainingSubjectDataModel>()
            .FirstOrDefaultAsync(ts => ts.Id == id);

        if(subjectDM == null)
            return null;
        
        var subject = _mapper.ToDomain(subjectDM);
        return subject;
    }
}
