using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public class HRManagerFactory : IHRManagerFactory
{

    private readonly IUserRepository _userRepository;

    public HRManagerFactory(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<HRManager> Create(long userId, IPeriodDateTime periodDateTime)
    {
        IUser? user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new ArgumentException("User does not exist");

        if (user.DeactivationDateIsBefore(periodDateTime.GetInitDate()))
            throw new ArgumentException("Deactivation date is before init date");

        if (user.IsDeactivated())
            throw new ArgumentException("User is deactivated");

        HRManager hrManager = new HRManager(userId, periodDateTime);

        return hrManager;
    }

    public async Task<HRManager> Create(long userId, DateTime initDate)
    {
        var periodDateTime = new PeriodDateTime(initDate, DateTime.MaxValue);

        return await Create(userId, periodDateTime);
    }

    public HRManager Create(IHRManagerVisitor visitor)
    {
        return new HRManager(visitor.UserId, visitor.PeriodDateTime);
    }
}
