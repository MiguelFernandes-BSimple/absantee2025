using Domain.Interfaces;
using Domain.IRepository;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class AssociationProjectCollaboratorRepositoryEF : GenericRepository<IAssociationProjectCollaborator>, IAssociationProjectCollaboratorRepository
{
    private AssociationProjectCollaboratorMapper _associationProjectCollaboratorMapper;

    public AssociationProjectCollaboratorRepositoryEF(DbContext context, AssociationProjectCollaboratorMapper associationProjectCollaboratorMapper) : base(context)
    {
        _associationProjectCollaboratorMapper = associationProjectCollaboratorMapper;
    }

    public async Task<bool> AddAsync(IAssociationProjectCollaborator newAssociation)
    {
        try
        {
            AssociationProjectCollaboratorDataModel assocDataModel = _associationProjectCollaboratorMapper.ToDataModel(newAssociation);

            EntityEntry<AssociationProjectCollaboratorDataModel> assocDMEntityEntry =
                _context.Set<AssociationProjectCollaboratorDataModel>().Add(assocDataModel);

            await _context.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAsync(IProject project)
    {
        try
        {
            IEnumerable<AssociationProjectCollaboratorDataModel> assocDM =
                await _context.Set<AssociationProjectCollaboratorDataModel>()
                              .Where(a => a.ProjectId == project.GetId())
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

    public async Task<IAssociationProjectCollaborator?> FindByProjectAndCollaboratorAsync(IProject project, ICollaborator collaborator)
    {
        try
        {
            AssociationProjectCollaboratorDataModel? assocDM =
                await _context.Set<AssociationProjectCollaboratorDataModel>()
                              .Where(a => a.ProjectId == project.GetId() && a.CollaboratorId == collaborator.GetId())
                              .FirstOrDefaultAsync();

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

    public async Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndBetweenPeriodAsync(IProject project, IPeriodDate periodDate)
    {
        try
        {
            IEnumerable<AssociationProjectCollaboratorDataModel> assocDM =
                await _context.Set<AssociationProjectCollaboratorDataModel>()
                              .Where(a => a.ProjectId == project.GetId()
                                    && a.Period._initDate <= periodDate.GetFinalDate()
                                    && periodDate.GetInitDate() <= a.Period._finalDate)
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

    public IEnumerable<IAssociationProjectCollaborator> FindAllByProject(IProject project)
    {
        try
        {
            IEnumerable<AssociationProjectCollaboratorDataModel> assocDM =
                _context.Set<AssociationProjectCollaboratorDataModel>()
                        .Where(a => a.ProjectId == project.GetId());

            IEnumerable<IAssociationProjectCollaborator> assocs =
                _associationProjectCollaboratorMapper.ToDomain(assocDM);

            return assocs;
        }
        catch
        {
            throw;
        }
    }

    public IEnumerable<IAssociationProjectCollaborator> FindAllByProjectAndBetweenPeriod(IProject project, IPeriodDate periodDate)
    {
        try
        {
            IEnumerable<AssociationProjectCollaboratorDataModel> assocDM =
            _context.Set<AssociationProjectCollaboratorDataModel>()
                    .Where(a => a.ProjectId == project.GetId()
                        && a.Period._initDate <= periodDate.GetFinalDate()
                        && periodDate.GetInitDate() <= a.Period._finalDate);

            IEnumerable<IAssociationProjectCollaborator> assocs =
                _associationProjectCollaboratorMapper.ToDomain(assocDM);

            return assocs;
        }
        catch
        {
            throw;
        }
    }

    public IAssociationProjectCollaborator? FindByProjectAndCollaborator(IProject project, ICollaborator collaborator)
    {
        try
        {
            AssociationProjectCollaboratorDataModel? assocDM =
                _context.Set<AssociationProjectCollaboratorDataModel>()
                        .Where(a => a.ProjectId == project.GetId() && a.CollaboratorId == collaborator.GetId())
                        .FirstOrDefault();

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

    public IAssociationProjectCollaborator? FindByProjectAndCollaborator(long projectId, long collaboratorId)
    {
        try
        {
            AssociationProjectCollaboratorDataModel? assocDM =
                _context.Set<AssociationProjectCollaboratorDataModel>()
                        .Where(a => a.ProjectId == projectId && a.CollaboratorId == collaboratorId)
                        .FirstOrDefault();

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

    public IEnumerable<IAssociationProjectCollaborator> FindAllByProjectAndBetweenPeriod(long project, IPeriodDate periodDate)
    {
        try
        {
            IEnumerable<AssociationProjectCollaboratorDataModel> assocDM =
            _context.Set<AssociationProjectCollaboratorDataModel>()
                    .Where(a => a.ProjectId == project
                        && a.Period._initDate <= periodDate.GetFinalDate()
                        && periodDate.GetInitDate() <= a.Period._finalDate);

            IEnumerable<IAssociationProjectCollaborator> assocs =
                _associationProjectCollaboratorMapper.ToDomain(assocDM);

            return assocs;
        }
        catch
        {
            throw;
        }
    }
}