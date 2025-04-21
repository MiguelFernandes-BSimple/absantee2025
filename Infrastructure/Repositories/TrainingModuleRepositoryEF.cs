using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories;

public class TrainingModuleRepositoryEF : GenericRepository<ITrainingModule, TrainingModuleDataModel>, ITrainingModuleRepository
{
    private readonly TrainingModuleMapper _mapper;
    public TrainingModuleRepositoryEF(AbsanteeContext context, TrainingModuleMapper mapper) : base(context, (IMapper<ITrainingModule, TrainingModuleDataModel>)mapper)
    {
        _mapper = mapper;
    }

    public override ITrainingModule? GetById(long id)
    {
        var trainingModuleDM = _context.Set<TrainingModuleDataModel>()
                                    .FirstOrDefault(t => t.Id == id);

        if (trainingModuleDM == null)
            return null;

        var TrainingModule = _mapper.ToDomain(trainingModuleDM);
        return TrainingModule;
    }

    public override async Task<ITrainingModule?> GetByIdAsync(long id)
    {
        var trainingModuleDM = await _context.Set<TrainingModuleDataModel>()
                                    .FirstOrDefaultAsync(t => t.Id == id);

        if (trainingModuleDM == null)
            return null;

        var TrainingModule = _mapper.ToDomain(trainingModuleDM);
        return TrainingModule;
    }

    public async Task<IEnumerable<ITrainingModule>> GetBySubjectIdAndFinished(long subjectId, DateTime date)
    {
        var trainingModulesDMs = await _context.Set<TrainingModuleDataModel>()
                                        .Where(t => t.SubjectID == subjectId
                                                && t.PeriodDateTime.All(p => p._finalDate >= date))
                                        .ToListAsync();

        var trainingModules = _mapper.ToDomain(trainingModulesDMs);

        return trainingModules;
    }
}
