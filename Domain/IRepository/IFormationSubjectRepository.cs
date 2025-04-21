using Domain.Visitor;

namespace Domain.IRepository;

public interface IFormationSubjectRepository : IGenericRepository<IFormationSubject, IFormationSubjectVisitor>
{
    Task<IFormationSubject?> GetByTitleAsync(string title);
}