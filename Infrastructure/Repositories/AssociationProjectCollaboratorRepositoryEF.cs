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