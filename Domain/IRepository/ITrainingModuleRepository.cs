using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository
{
    public interface ITrainingModuleRepository : IGenericRepository <ITrainingModule, ITrainingModuleVisitor>
    {
        Task <IEnumerable<ITrainingModule>> GetBySubject( long subjectId);
        ITrainingModule? GetById(long id);

        Task <ITrainingModule?> GetByIdAsync(long id);

    }
}