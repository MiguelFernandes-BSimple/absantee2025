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

    public HRManager Create(long userId, IPeriodDateTime periodDateTime)
    {
        IUser? user = _userRepository.GetById(userId);

        if (user == null)
            throw new ArgumentException("User does not exist");

        if (user.DeactivationDateIsBefore(periodDateTime.GetInitDate()))
            throw new ArgumentException("Deactivation date is before init date");

        if (user.IsDeactivated())
            throw new ArgumentException("User is deactivated");

        HRManager hRManager = new HRManager(userId, periodDateTime);

        return hRManager;
    }

    public HRManager Create(long userId, DateTime initDate)
    {
        var user = _userRepository.GetById(userId);

        if (user == null)
            throw new ArgumentException("User does not exist");

        else if (user.DeactivationDateIsBefore(initDate))
            throw new ArgumentException("Deactivation date is before init date");

        else if (user.IsDeactivated())
            throw new ArgumentException("User is deactivated");

        else return new HRManager(userId, initDate);
    }

    public HRManager Create(IHRManagerVisitor visitor)
    {
        return new HRManager(visitor.UserId, visitor.PeriodDateTime);
    }
}
