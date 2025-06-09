using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure;
using Infrastructure.DataModel;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class AssociationProjectCollaboratorRepositoryEF : GenericRepositoryEF<IAssociationProjectCollaborator, AssociationProjectCollaborator, AssociationProjectCollaboratorDataModel>, IAssociationProjectCollaboratorRepository
{
    private readonly IMapper _mapper;

    public AssociationProjectCollaboratorRepositoryEF(AbsanteeContext context, IMapper associationProjectCollaboratorMapper)
        : base(context, associationProjectCollaboratorMapper)
    {
        _mapper = associationProjectCollaboratorMapper;
    }

    public override IAssociationProjectCollaborator? GetById(Guid id)
    {
        var assocDM = _context.Set<AssociationProjectCollaboratorDataModel>()
                              .FirstOrDefault(a => a.Id == id);

        if (assocDM == null)
            return null;

        var assoc = _mapper.Map<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>(assocDM);
        return assoc;
    }

    public override async Task<IAssociationProjectCollaborator?> GetByIdAsync(Guid id)
    {
        var assocDM = await _context.Set<AssociationProjectCollaboratorDataModel>()
                              .FirstOrDefaultAsync(a => a.Id == id);

        if (assocDM == null)
            return null;

        var assoc = _mapper.Map<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>(assocDM);
        return assoc;
    }

    public async Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByCollaboratorAsync(Guid collabId)
    {
        IEnumerable<AssociationProjectCollaboratorDataModel> assocDM =
            await _context.Set<AssociationProjectCollaboratorDataModel>()
                          .Where(a => a.CollaboratorId == collabId)
                          .ToListAsync();

        IEnumerable<AssociationProjectCollaborator> assocs =
            assocDM.Select(_mapper.Map<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>);

        return assocs;
    }

    public async Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAsync(Guid projectId)
    {
        IEnumerable<AssociationProjectCollaboratorDataModel> assocDM =
            await _context.Set<AssociationProjectCollaboratorDataModel>()
                          .Where(a => a.ProjectId == projectId)
                          .ToListAsync();

        IEnumerable<AssociationProjectCollaborator> assocs =
            assocDM.Select(_mapper.Map<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>);

        return assocs;
    }

    public async Task<IAssociationProjectCollaborator?> FindByProjectAndCollaboratorAsync(Guid projectId, Guid collaboratorId)
    {
        try
        {
            AssociationProjectCollaboratorDataModel? assocDM =
                await FindByCollaboratorAndProject(collaboratorId, projectId).FirstOrDefaultAsync();

            if (assocDM == null)
                return null;

            AssociationProjectCollaborator result = _mapper.Map<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>(assocDM);

            return result;
        }
        catch
        {
            return null;
        }
    }

    public async Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndCollaboratorAsync(Guid projectId, Guid collaboratorId)
    {
        IEnumerable<AssociationProjectCollaboratorDataModel> assocsDM =
            await FindByCollaboratorAndProject(collaboratorId, projectId).ToListAsync();


        IEnumerable<AssociationProjectCollaborator> result = assocsDM.Select(_mapper.Map<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>);

        return result;
    }

    public async Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndIntersectingPeriodAsync(Guid projectId, PeriodDate periodDate)
    {
        IEnumerable<AssociationProjectCollaboratorDataModel> assocDM =
            await _context.Set<AssociationProjectCollaboratorDataModel>()
                          .Where(a => a.ProjectId == projectId
                                && a.PeriodDate.InitDate <= periodDate.FinalDate
                                && periodDate.InitDate <= a.PeriodDate.FinalDate)
                          .ToListAsync();

        IEnumerable<AssociationProjectCollaborator> assocs =
            assocDM.Select(a => _mapper.Map<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>(a));

        return assocs;
    }

    public async Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndCollaboratorAndBetweenPeriodAsync(Guid projectId, Guid collaboratorId, PeriodDate periodDate)
    {
        IEnumerable<AssociationProjectCollaboratorDataModel> assocDM =
            await _context.Set<AssociationProjectCollaboratorDataModel>()
                          .Where(a => a.ProjectId == projectId
                                && a.CollaboratorId == collaboratorId
                                && a.PeriodDate.InitDate <= periodDate.FinalDate
                                && periodDate.InitDate <= a.PeriodDate.FinalDate)
                          .ToListAsync();

        IEnumerable<AssociationProjectCollaborator> assocs =
            assocDM.Select(a => _mapper.Map<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>(a));

        return assocs;
    }

    public async Task<bool> CanInsert(PeriodDate periodDate, Guid collaboratorId, Guid projectId)
    {
        try
        {
            var assocDMs = FindByCollaboratorAndProject(collaboratorId, projectId);

            int count = assocDMs.Count();

            bool intersect = await assocDMs.Where(a => a.PeriodDate.InitDate <= periodDate.FinalDate &&
                                                    a.PeriodDate.FinalDate >= periodDate.InitDate)
                                        .AnyAsync();

            return !intersect;
        }
        catch
        {
            return false;
        }
    }

    private IQueryable<AssociationProjectCollaboratorDataModel> FindByCollaboratorAndProject(Guid collabId, Guid projectId)
    {
        var result = _context.Set<AssociationProjectCollaboratorDataModel>()
                             .Where(a => a.CollaboratorId == collabId && a.ProjectId == projectId);

        return result;
    }
}