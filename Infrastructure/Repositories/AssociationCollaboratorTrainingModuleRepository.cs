using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class AssociationCollaboratorTrainingModuleRepository 
    : GenericRepository<IAssociationCollaboratorTrainingModule, IAssociationCollaboratorTrainingModuleVisitor>, 
      IAssociationCollaboratorTrainingModuleRepository
{
    private readonly IMapper<IAssociationCollaboratorTrainingModule, IAssociationCollaboratorTrainingModuleVisitor> _associationCollaboratorTrainingModuleMapper;

    public AssociationCollaboratorTrainingModuleRepository(
        AbsanteeContext context, 
        IMapper<IAssociationCollaboratorTrainingModule, IAssociationCollaboratorTrainingModuleVisitor> associationCollaboratorTrainingModuleMapper)
        : base(context, associationCollaboratorTrainingModuleMapper)
    {
        _associationCollaboratorTrainingModuleMapper = associationCollaboratorTrainingModuleMapper;
    }

    public override IAssociationCollaboratorTrainingModule? GetById(long id)
    {
        try
        {
            var assocDM = _context.Set<AssociationCollaboratorTrainingModuleDataModel>()
                                  .FirstOrDefault(a => a.Id == id);

            return assocDM == null ? null : _associationCollaboratorTrainingModuleMapper.ToDomain(assocDM);
        }
        catch
        {
            throw;
        }
    }

    public override async Task<IAssociationCollaboratorTrainingModule?> GetByIdAsync(long id)
    {
        try
        {
            var assocDM = await _context.Set<AssociationCollaboratorTrainingModuleDataModel>()
                                        .FirstOrDefaultAsync(a => a.Id == id);

            return assocDM == null ? null : _associationCollaboratorTrainingModuleMapper.ToDomain(assocDM);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<IAssociationCollaboratorTrainingModule>> FindAllByAsync(long collaboratorId)
    {
        try
        {
            var assocDMs = await _context.Set<AssociationCollaboratorTrainingModuleDataModel>()
                                         .Where(a => a.CollaboratorId == collaboratorId)
                                         .ToListAsync();

            return _associationCollaboratorTrainingModuleMapper.ToDomain(assocDMs);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IAssociationCollaboratorTrainingModule?> FindByCollaboratorAndTrainingModuleAsync(long collaboratorId, long trainingModuleId)
    {
        try
        {
            var assocDM = await FindByCollaboratorAndModule(collaboratorId, trainingModuleId)
                                .FirstOrDefaultAsync();

            return assocDM == null ? null : _associationCollaboratorTrainingModuleMapper.ToDomain(assocDM);
        }
        catch
        {
            return null;
        }
    }

    public async Task<IEnumerable<IAssociationCollaboratorTrainingModule>> FindAllCollaboratorAndTrainingModuleAsync(long collaboratorId, long trainingModuleId)
    {
        try
        {
            var assocDMs = await FindByCollaboratorAndModule(collaboratorId, trainingModuleId).ToListAsync();
            return _associationCollaboratorTrainingModuleMapper.ToDomain(assocDMs);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<IAssociationCollaboratorTrainingModule>> FindAllByTrainingModuleAndBetweenPeriodAsync(long trainingModuleId, PeriodDate periodDate)
    {
        try
        {
            var assocDMs = await _context.Set<AssociationCollaboratorTrainingModuleDataModel>()
                                         .Where(a => a.TrainingModuleId == trainingModuleId &&
                                                     a.Period._initDate <= periodDate._finalDate &&
                                                     periodDate._initDate <= a.Period._finalDate)
                                         .ToListAsync();

            return _associationCollaboratorTrainingModuleMapper.ToDomain(assocDMs);
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> CanInsert(long collaboratorId, long trainingModuleId, PeriodDate periodDate)
    {
        try
        {
            var assocDMs = FindByCollaboratorAndModule(collaboratorId, trainingModuleId);

            bool intersect = await assocDMs
                .Where(a => a.Period._initDate <= periodDate._finalDate &&
                            a.Period._finalDate >= periodDate._initDate)
                .AnyAsync();

            return !intersect;
        }
        catch
        {
            return false;
        }
    }

    private IQueryable<AssociationCollaboratorTrainingModuleDataModel> FindByCollaboratorAndModule(long collabId, long trainingModuleId)
    {
        return _context.Set<AssociationCollaboratorTrainingModuleDataModel>()
                       .Where(a => a.CollaboratorId == collabId && a.TrainingModuleId == trainingModuleId);
    }


}
