using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
namespace Domain.Factory;

public class TrainingManagerFactory : ITrainingManagerFactory
{
    private IUserRepository _userRepository;

    public TrainingManagerFactory(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<TrainingManager> Create(long userId, PeriodDateTime periodDateTime)
    {
        IUser? user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new ArgumentException("User does not exist");

        else if (user.DeactivationDateIsBefore(periodDateTime.GetInitDate()))
            throw new ArgumentException("Deactivation date is before init date");

        else if (user.IsDeactivated())
            throw new ArgumentException("User is deactivated");
        var TrainingManager = new TrainingManager(userId, periodDateTime);
        return TrainingManager;
    }

    public async Task<TrainingManager> Create(long userId, DateTime initDate)
    {
        var periodDateTime = new PeriodDateTime(initDate, DateTime.MaxValue);
        return await Create(userId, periodDateTime);
    }

    public TrainingManager Create(ITrainingManagerVisitor tmv)
    {
        return new TrainingManager(tmv.UserId, tmv.PeriodDateTime);
    }
}
