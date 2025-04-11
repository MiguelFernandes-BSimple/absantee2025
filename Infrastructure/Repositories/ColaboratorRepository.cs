using Domain.IRepository;
using Domain.Interfaces;
using Domain.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Infrastructure.DataModel;
using System.Threading.Tasks;
using Infrastructure.Mapper;

namespace Infrastructure.Repositories;

public class CollaboratorRepository : GenericRepository<ICollaborator, CollaboratorDataModel>, ICollaboratorRepository
{
    private readonly CollaboratorMapper _mapper;
    public CollaboratorRepository(AbsanteeContext context, CollaboratorMapper mapper) : base(context, (IMapper<ICollaborator, CollaboratorDataModel>)mapper)
    {
        _mapper = mapper;
    }

    public async Task<bool> IsRepeated(ICollaborator collaborator)
    {
        return await this._context.Set<CollaboratorDataModel>()
                .AnyAsync(c => c.UserID == collaborator.GetUserId()
                    && c.PeriodDateTime.GetInitDate() <= collaborator.GetPeriodDateTime().GetFinalDate()
                    && collaborator.GetPeriodDateTime().GetInitDate() <= c.PeriodDateTime.GetFinalDate());
    }

    public override ICollaborator? GetById(long id)
    {
        var collabDM = this._context.Set<CollaboratorDataModel>()
                            .FirstOrDefault(c => c.Id == id);

        if (collabDM == null)
            return null;

        var collab = _mapper.ToDomain(collabDM);
        return collab;
    }

    public override async Task<ICollaborator?> GetByIdAsync(long id)
    {
        var collabDM = await this._context.Set<CollaboratorDataModel>()
                            .FirstOrDefaultAsync(c => c.Id == id);

        if (collabDM == null)
            return null;

        var collab = _mapper.ToDomain(collabDM);
        return collab;
    }

    public async Task<IEnumerable<ICollaborator>> GetByIdsAsync(IEnumerable<long> ids)
    {
        var collabsDm = await this._context.Set<CollaboratorDataModel>()
                    .Where(c => ids.Contains(c.Id))
                    .ToListAsync();

        var collabs = _mapper.ToDomain(collabsDm);

        return collabs;
    }

    public async Task<IEnumerable<ICollaborator>> GetByUsersIdsAsync(IEnumerable<long> ids)
    {
        var collabsDm = await this._context.Set<CollaboratorDataModel>()
                    .Where(c => ids.Contains(c.UserID))
                    .ToListAsync();

        var collabs = _mapper.ToDomain(collabsDm);

        return collabs;
    }
}