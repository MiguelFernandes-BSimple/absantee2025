using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public interface IHRManagerFactory
    {
        Task<HRManager> Create(long userId, PeriodDateTime periodDateTime);
        Task<HRManager> Create(long userId, DateTime initDate);

        HRManager Create(IHRManagerVisitor visitor);
    }
}