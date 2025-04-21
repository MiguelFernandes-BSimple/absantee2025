using Domain.IRepository;
using Domain.Interfaces;
using Domain.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Infrastructure.DataModel;
using System.Threading.Tasks;
using Infrastructure.Mapper;
using Domain.Visitor;

namespace Infrastructure.Repositories;

public class CollaboratorRepository : GenericRepository<ICollaborator, ICollaboratorVisitor>, ICollaboratorRepository
{
    private readonly IMapper<ICollaborator, ICollaboratorVisitor> _mapper;
    public CollaboratorRepository(AbsanteeContext context, IMapper<ICollaborator, ICollaboratorVisitor> mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public async Task<bool> IsRepeated(ICollaborator collaborator)
    {
        return await this._context.Set<CollaboratorDataModel>()
                .AnyAsync(c => c.UserID == collaborator._userId
                    && c.PeriodDateTime._initDate <= collaborator._periodDateTime._finalDate
                    && collaborator._periodDateTime._initDate <= c.PeriodDateTime._finalDate);
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

    public async Task<IEnumerable<ICollaborator>> GetAllActiveCollaboratorsNotOnList(IEnumerable<long> collabs)
    {
        var result = await _context.Set<CollaboratorDataModel>()
                                   .Where(c => collabs.Any(c2 => c2 == c.Id) && c.PeriodDateTime._finalDate > DateTime.Now)
                                   .ToListAsync();

        var toDomain = _mapper.ToDomain(result);

        return toDomain;
    }
}