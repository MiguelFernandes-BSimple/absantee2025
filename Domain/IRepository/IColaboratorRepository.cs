using Domain.Interfaces;
using Domain.Models;

namespace Domain.IRepository;

public interface ICollaboratorRepository : IGenericRepository<ICollaborator>
{
    Task<bool> isRepeated(ICollaborator collaborator);
    Task<ICollaborator> AddAsync(ICollaborator collaborator);
}