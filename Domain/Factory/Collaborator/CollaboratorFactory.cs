using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public class CollaboratorFactory : ICollaboratorFactory
    {

        public CollaboratorFactory()
        {
        }

        public Collaborator Create(Guid collabId, Guid userId, PeriodDateTime periodDateTime)
        {
            return new Collaborator(collabId, userId, periodDateTime);
        }
        public Collaborator Create(ICollaboratorVisitor visitor)
        {
            return new Collaborator(visitor.Id, visitor.UserId, visitor.PeriodDateTime);
        }
    }
}
