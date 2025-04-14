using Domain.Interfaces;
using Domain.IRepository;
using Infrastructure;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class AssociationProjectCollaboratorRepositoryEF : GenericRepository<IAssociationProjectCollaborator, AssociationProjectCollaboratorDataModel>, IAssociationProjectCollaboratorRepository
{
    private readonly AssociationProjectCollaboratorMapper _associationProjectCollaboratorMapper;

    public AssociationProjectCollaboratorRepositoryEF(AbsanteeContext context, AssociationProjectCollaboratorMapper associationProjectCollaboratorMapper)
        : base(context, (IMapper<IAssociationProjectCollaborator, AssociationProjectCollaboratorDataModel>)associationProjectCollaboratorMapper)
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
                await _context.Set<AssociationProjectCollaboratorDataModel>()
                              .Where(a => a.ProjectId == projectId && a.CollaboratorId == collaboratorId)
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

    public async Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndCollaboratorAsync(long projectId, long collaboratorId)
    {
        try
        {
            IEnumerable<AssociationProjectCollaboratorDataModel> assocsDM =
                await _context.Set<AssociationProjectCollaboratorDataModel>()
                              .Where(a => a.ProjectId == projectId && a.CollaboratorId == collaboratorId)
                              .ToListAsync();


            IEnumerable<IAssociationProjectCollaborator> result = _associationProjectCollaboratorMapper.ToDomain(assocsDM);

            return result;
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndBetweenPeriodAsync(long projectId, IPeriodDate periodDate)
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
}