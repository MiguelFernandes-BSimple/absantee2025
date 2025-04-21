using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class AssociationTrainingModuleCollaboratorRepositoryEF : GenericRepository<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor>, IAssociationTrainingModuleCollaboratorRepository
{
    private readonly IMapper<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor> _associationTrainingModuleCollaboratorMapper;

    public AssociationTrainingModuleCollaboratorRepositoryEF(AbsanteeContext context, IMapper<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor> associationTrainingModuleCollaboratorMapper)
        : base(context, associationTrainingModuleCollaboratorMapper)
    {
        _associationTrainingModuleCollaboratorMapper = associationTrainingModuleCollaboratorMapper;
    }

    public override IAssociationTrainingModuleCollaborator? GetById(long id)
    {
        try
        {
            var assocDM = _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                                  .FirstOrDefault(a => a.Id == id);

            if (assocDM == null)
                return null;

            var assoc = _associationTrainingModuleCollaboratorMapper.ToDomain(assocDM);
            return assoc;
        }
        catch
        {
            throw new Exception("Exception");
        }
    }

    public override async Task<IAssociationTrainingModuleCollaborator?> GetByIdAsync(long id)
    {
        try
        {
            var assocDM = await _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                                  .FirstOrDefaultAsync(a => a.Id == id);

            if (assocDM == null)
                return null;

            var assoc = _associationTrainingModuleCollaboratorMapper.ToDomain(assocDM);
            return assoc;
        }
        catch
        {
            throw new Exception("Exception");
        }
    }

    public async Task<bool> CanInsert(long collaboratorId, long trainingModuleId, PeriodDateTime periodDateTime)
    {
        try
        {
            var assocDMs = FindByCollaboratorAndTrainingModule(collaboratorId, trainingModuleId);

            bool intersect = await assocDMs.AnyAsync(a => a.PeriodDateTime._initDate <= periodDateTime._finalDate &&
                                                    a.PeriodDateTime._finalDate >= periodDateTime._initDate);

            return !intersect;
        }
        catch
        {
            return false;
        }
    }

    private IQueryable<AssociationTrainingModuleCollaboratorDataModel> FindByCollaboratorAndTrainingModule(long collaboratorId, long trainingModuleId)
    {
        var result = _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                             .Where(a => a.CollaboratorId == collaboratorId && a.TrainingModuleId == trainingModuleId);

        return result;
    }

    public async Task<IEnumerable<IAssociationTrainingModuleCollaborator>> FindAllByTrainingModuleAsync(long trainingModuleId)
    {
        try
        {
            IEnumerable<AssociationTrainingModuleCollaboratorDataModel> assocDM =
                await _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                              .Where(a => a.TrainingModuleId == trainingModuleId)
                              .ToListAsync();

            IEnumerable<IAssociationTrainingModuleCollaborator> assocs =
                _associationTrainingModuleCollaboratorMapper.ToDomain(assocDM);

            return assocs;
        }
        catch
        {
            throw new Exception("Exception");
        }
    }

    public async Task<IEnumerable<IAssociationTrainingModuleCollaborator>> FindAllByTrainingModuleAfterDateAsync(long trainingModuleId, DateTime dateTime)
    {
        try
        {
            IEnumerable<AssociationTrainingModuleCollaboratorDataModel> assocDM =
                await _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                              .Where(a => a.TrainingModuleId == trainingModuleId &&
                                    a.PeriodDateTime._finalDate <= dateTime)
                              .ToListAsync();

            IEnumerable<IAssociationTrainingModuleCollaborator> assocs =
                _associationTrainingModuleCollaboratorMapper.ToDomain(assocDM);

            return assocs;
        }
        catch
        {
            throw new Exception("Exception");
        }
    }
}