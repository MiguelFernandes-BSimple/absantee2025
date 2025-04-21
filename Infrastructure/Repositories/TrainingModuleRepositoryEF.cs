using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class TrainingModuleRepositoryEF : GenericRepository<ITrainingModule, ITrainingModuleVisitor>, ITrainingModuleRepository
{
    private readonly IMapper<ITrainingModule, ITrainingModuleVisitor> _mapper;
    public TrainingModuleRepositoryEF(AbsanteeContext context, IMapper<ITrainingModule, ITrainingModuleVisitor> mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public async Task<IEnumerable<ITrainingModule>> FindAllBySubject(long trainingSubjectId)
    {
        var bySubjectId = await _context.Set<TrainingModuleDataModel>()
                                   .Where(tm => tm.TrainingSubjectId == trainingSubjectId)
                                   .ToListAsync();

        var tms = _mapper.ToDomain(bySubjectId);

        return tms;
    }

    public async Task<IEnumerable<ITrainingModule>> FindAllBySubjectAndAfterPeriod(long trainingSubjectId, PeriodDateTime period)
    {
        var bySubjectId = await _context.Set<TrainingModuleDataModel>()
                                   .Where(tm => tm.TrainingSubjectId == trainingSubjectId)
                                   .ToListAsync();

        var result = bySubjectId.Where(tms => tms.Periods.All(p => p._finalDate > period._finalDate)).ToList();

        var tms = _mapper.ToDomain(result);

        return tms;
    }

    public override ITrainingModule? GetById(long id)
    {
        var tmDM = _context.Set<TrainingModuleDataModel>()
                           .FirstOrDefault(t => t.Id == id);

        if (tmDM == null)
            return null;

        var tm = _mapper.ToDomain(tmDM);
        return tm;
    }

    public override async Task<ITrainingModule?> GetByIdAsync(long id)
    {
        var tmDM = await _context.Set<TrainingModuleDataModel>()
                           .FirstOrDefaultAsync(t => t.Id == id);

        if (tmDM == null)
            return null;

        var tm = _mapper.ToDomain(tmDM);
        return tm;
    }
}