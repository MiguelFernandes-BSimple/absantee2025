using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public interface IHRManagerFactory
    {
        HRManager Create(long userId, IPeriodDateTime periodDateTime);

        HRManager Create(IHRManagerVisitor visitor);
    }
}