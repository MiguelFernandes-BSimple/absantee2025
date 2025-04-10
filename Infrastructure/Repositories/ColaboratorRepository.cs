using Domain.IRepository;
using Domain.Interfaces;
using Domain.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Infrastructure.DataModel;

namespace Infrastructure.Repositories;

public class CollaboratorRepository : GenericRepository<ICollaborator>, ICollaboratorRepository
{
    public CollaboratorRepository(DbContext context) : base(context)
    {
    }

    public bool isRepeated(ICollaborator collaborator)
    {
        return this._context.Set<CollaboratorDataModel>()
                .Any(c => c.UserID == collaborator.GetUserId()
                    && c.PeriodDateTime.GetInitDate() <= collaborator.GetPeriodDateTime().GetFinalDate()
                    && collaborator.GetPeriodDateTime().GetInitDate() <= c.PeriodDateTime.GetFinalDate());
    }
}