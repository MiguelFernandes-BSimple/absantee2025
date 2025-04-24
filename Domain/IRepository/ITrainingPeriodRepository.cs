using Domain.Interfaces;
using Domain.Visitor;

namespace Domain.IRepository
{
    public interface ITrainingPeriodRepository : IGenericRepository<ITrainingPeriod, ITrainingPeriodVisitor>
    {
    }
}
