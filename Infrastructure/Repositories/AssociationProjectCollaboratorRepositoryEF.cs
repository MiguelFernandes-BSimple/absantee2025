using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class AssociationProjectCollaboratorRepositoryEF : GenericRepository<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>, IAssociationProjectCollaboratorRepository
{
    private readonly IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor> _associationProjectCollaboratorMapper;

    public AssociationProjectCollaboratorRepositoryEF(AbsanteeContext context, IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor> associationProjectCollaboratorMapper)
        : base(context, associationProjectCollaboratorMapper)
    {
        _associationProjectCollaboratorMapper = associationProjectCollaboratorMapper;
    }

    public override IAssociationProjectCollaborator? GetById(long id)
    {
        try
        {
            var assocDM = _context.Set<AssociationProjectCollaboratorDataModel>()
                                  .FirstOrDefault(a => a.Id == id);

            if (assocDM == null)
                return null;

            var assoc = _associationProjectCollaboratorMapper.ToDomain(assocDM);
            return assoc;
        }
        catch
        {
            throw;
        }
    }

    public override async Task<IAssociationProjectCollaborator?> GetByIdAsync(long id)
    {
        try
        {
            var assocDM = await _context.Set<AssociationProjectCollaboratorDataModel>()
                                  .FirstOrDefaultAsync(a => a.Id == id);

            if (assocDM == null)
                return null;

            var assoc = _associationProjectCollaboratorMapper.ToDomain(assocDM);
            return assoc;
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAsync(long projectId)
    {
        try
        {
            IEnumerable<AssociationProjectCollaboratorDataModel> assocDM =
                await _context.Set<AssociationProjectCollaboratorDataModel>()
                              .Where(a => a.ProjectId == projectId)
                              .ToListAsync();

            IEnumerable<IAssociationProjectCollaborator> assocs =
                _associationProjectCollaboratorMapper.ToDomain(assocDM);

            return assocs;
        }
        catch
        {
            throw;
        }
    }

    public async Task<IAssociationProjectCollaborator?> FindByProjectAndCollaboratorAsync(long projectId, long collaboratorId)
    {
        try
        {
            AssociationProjectCollaboratorDataModel? assocDM =
                await FindByCollaboratorAndProject(collaboratorId, projectId).FirstOrDefaultAsync();

            if (assocDM == null)
                return null;

            IAssociationProjectCollaborator result = _associationProjectCollaboratorMapper.ToDomain(assocDM);

            return result;
        }
        catch
        {
            return null;
        }
    }

    public async Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndCollaboratorAsync(long projectId, long collaboratorId)
    {
        try
        {
            IEnumerable<AssociationProjectCollaboratorDataModel> assocsDM =
                await FindByCollaboratorAndProject(collaboratorId, projectId).ToListAsync();


            IEnumerable<IAssociationProjectCollaborator> result = _associationProjectCollaboratorMapper.ToDomain(assocsDM);

            return result;
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndBetweenPeriodAsync(long projectId, PeriodDate periodDate)
    {
        try
        {
            IEnumerable<AssociationProjectCollaboratorDataModel> assocDM =
                await _context.Set<AssociationProjectCollaboratorDataModel>()
                              .Where(a => a.ProjectId == projectId
                                    && a.Period.GetInitDate() <= periodDate.GetFinalDate()
                                    && periodDate.GetInitDate() <= a.Period.GetFinalDate())
                              .ToListAsync();

            IEnumerable<IAssociationProjectCollaborator> assocs =
                _associationProjectCollaboratorMapper.ToDomain(assocDM);

            return assocs;
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> CanInsert(PeriodDate periodDate, long collaboratorId, long projectId)
    {
        try
        {
            var assocDMs = FindByCollaboratorAndProject(collaboratorId, projectId);

            bool result = await assocDMs.Where(a => a.Period.GetInitDate() <= periodDate.GetFinalDate() &&
                                                    a.Period.GetFinalDate() >= periodDate.GetInitDate())
                                        .AnyAsync();

            return result;
        }
        catch
        {
            return false;
        }
    }

    private IQueryable<AssociationProjectCollaboratorDataModel> FindByCollaboratorAndProject(long collabId, long projectId)
    {
        var result = _context.Set<AssociationProjectCollaboratorDataModel>()
                             .Where(a => a.CollaboratorId == collabId && a.ProjectId == projectId);

        return result;
    }
}