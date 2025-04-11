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

    public async Task<ICollaborator> AddAsync(ICollaborator collaborator)
    {
        try
        {
            CollaboratorDataModel collabDM = new CollaboratorDataModel(collaborator);
            var colabEntityEntry = this._context.Set<CollaboratorDataModel>().Add(collabDM);

            await this._context.SaveChangesAsync();

            collabDM = colabEntityEntry.Entity;
            Collaborator collab = _mapper.ToDomain(collabDM);

            return collab;
        }
        catch (Exception)
        {
            throw;
        }
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
}