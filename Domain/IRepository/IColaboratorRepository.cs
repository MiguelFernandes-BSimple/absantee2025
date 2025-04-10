using Domain.Interfaces;
using Domain.Models;

namespace Domain.IRepository;

public interface ICollaboratorRepository : IGenericRepository<ICollaborator>
{
    bool isRepeated(ICollaborator collaborator);
}