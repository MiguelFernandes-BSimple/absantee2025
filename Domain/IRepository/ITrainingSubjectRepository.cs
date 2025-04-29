
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;
public interface ITrainingSubjectRepository : IGenericRepository<TrainingSubject, ITrainingSubjectVisitor>
{
    Task<bool> IsDuplicated(string subject);
}
