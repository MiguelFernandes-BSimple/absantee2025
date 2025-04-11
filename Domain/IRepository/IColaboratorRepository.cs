using Domain.Interfaces;
using Domain.Models;

namespace Domain.IRepository;

public interface ICollaboratorRepository : IGenericRepository<ICollaborator>
{
    Task<bool> IsRepeated(ICollaborator collaborator);
    Task<ICollaborator> AddAsync(ICollaborator collaborator);
}