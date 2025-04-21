using Domain.Interfaces;
using Domain.Visitor;

namespace Domain.IRepository;

public interface ITrainingSubjectRepository : IGenericRepository<ITrainingSubject, ITrainingSubjectVisitor> {
    
}
