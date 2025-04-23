using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure;
using Infrastructure.DataModel;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class AssociationProjectCollaboratorRepositoryEF : GenericRepository<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>, IAssociationProjectCollaboratorRepository
{
    private readonly IMapper _mapper;

    public AssociationProjectCollaboratorRepositoryEF(AbsanteeContext context, IMapper associationProjectCollaboratorMapper)
        : base(context, associationProjectCollaboratorMapper)
    {
        _mapper = associationProjectCollaboratorMapper;
    }

    public override IAssociationProjectCollaborator? GetById(long id)
    {
        try
        {
            var assocDM = _context.Set<AssociationProjectCollaboratorDataModel>()
                                  .FirstOrDefault(a => a.Id == id);

            if (assocDM == null)
                return null;

            var assoc = _mapper.Map<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>(assocDM);
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

            var assoc = _mapper.Map<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>(assocDM);
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
                assocDM.Select(a => _mapper.Map<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>(a));

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

            IAssociationProjectCollaborator result = _mapper.Map<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>(assocDM);

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


            IEnumerable<IAssociationProjectCollaborator> result = assocsDM.Select(a => _mapper.Map<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>(a));

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
                                    && a.PeriodDate.initDate <= periodDate.finalDate
                                    && periodDate.initDate <= a.PeriodDate.finalDate)
                              .ToListAsync();

            IEnumerable<IAssociationProjectCollaborator> assocs =
                assocDM.Select(a => _mapper.Map<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>(a));

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

            int count = assocDMs.Count();

            bool intersect = await assocDMs.Where(a => a.PeriodDate.initDate <= periodDate.finalDate &&
                                                    a.PeriodDate.finalDate >= periodDate.initDate)
                                        .AnyAsync();

            return !intersect;
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