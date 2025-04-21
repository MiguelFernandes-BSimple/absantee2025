using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public class TrainingManagerFactory : ITrainingManagerFactory
    {
        private readonly IUserRepository _userRepository;

        public TrainingManagerFactory(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<TrainingManager> Create(long userId, PeriodDateTime periodDateTime)
        {
            IUser? user = _userRepository.GetById((int)userId);

            if (user == null)
                throw new ArgumentException("User dont exists");

            if (user.DeactivationDateIsBefore(periodDateTime.GetFinalDate()))
                throw new ArgumentException("User deactivation date is before collaborator contract end date.");

            if (user.IsDeactivated())
                throw new ArgumentException("User is deactivated.");

            TrainingManager trainingManager = new TrainingManager(userId, periodDateTime);

            return trainingManager;
        }

        public async Task <TrainingManager> Create( long userId, DateTime initDate){
            
            var periodDateTime = new PeriodDateTime(initDate, DateTime.MaxValue);
            return await Create(userId, periodDateTime);
        }

        public TrainingManager Create(ITrainingManagerVisitor visitor)
        {
            return new TrainingManager(visitor.Id, visitor.UserID, visitor.PeriodDateTime);
        }
    }
}
