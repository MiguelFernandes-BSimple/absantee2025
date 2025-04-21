using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class TrainingModuleRepository : GenericRepository<ITrainingModule, ITrainingModuleVisitor>, ITrainingModuleRepository
{

    private readonly IMapper<ITrainingModule,ITrainingModuleVisitor> _trainingModuleMapper;

    public TrainingModuleRepository(AbsanteeContext context, IMapper<ITrainingModule,ITrainingModuleVisitor> trainingModuleMapper)
        : base(context, trainingModuleMapper)
    {
        _trainingModuleMapper = trainingModuleMapper;
    }

    public override ITrainingModule? GetById(long id)
    {
        try{
            var trainingModuleDM = _context.Set<TrainingModuleDataModel>(). FirstOrDefault(a => a.Id == id);

            if(trainingModuleDM == null)
                return null;

            var trainingModule = _trainingModuleMapper.ToDomain(trainingModuleDM);
            return trainingModule;
        }
        catch{
            throw;
        }
    }

    public override async Task<ITrainingModule?> GetByIdAsync(long id)
    {
        try{
                var trainingModuleDM = await _context.Set<TrainingModuleDataModel>()
                                  .FirstOrDefaultAsync(a => a.Id == id);

                if(trainingModuleDM == null)
                    return null;

                var trainingModule = _trainingModuleMapper.ToDomain(trainingModuleDM);
                return trainingModule;

      } catch{
        throw;
      }
    }

    public async Task<IEnumerable<ITrainingModule>> GetBySubject(long subjectId)
    {
        try
    {

        var trainingModulesDM = await _context.Set<TrainingModuleDataModel>()
            .Where(tm => tm.Id == subjectId)
            .ToListAsync();

        var trainingModules = trainingModulesDM
            .Select(_trainingModuleMapper.ToDomain)
            .Where(domain => domain != null)
            .ToList();

        return trainingModules;
    }
    catch
    {
        throw;
    }
    }
}