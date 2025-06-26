using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository
{
    public interface ITrainingPeriodRepository : IGenericRepositoryEF<ITrainingPeriod, TrainingPeriod, ITrainingPeriodVisitor>
    {
    }
}
