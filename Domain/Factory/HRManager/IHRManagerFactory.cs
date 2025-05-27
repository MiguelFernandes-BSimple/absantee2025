using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public interface IHRManagerFactory
    {
        Task<HRManager> Create(Guid userId, PeriodDateTime periodDateTime);
        Task<HRManager> Create(Guid userId, DateTime initDate);
        public HRManager Create(IUser user, PeriodDateTime periodDateTime);
        HRManager Create(IHRManagerVisitor visitor);
    }
}