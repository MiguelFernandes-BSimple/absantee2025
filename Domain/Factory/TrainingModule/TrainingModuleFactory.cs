using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public class TrainingModuleFactory : ITrainingModuleFactory
    {
        private readonly ITrainingSubjectRepository _subjectRepository;
        private readonly ITrainingModuleRepository _moduleRepository;

        public TrainingModuleFactory(ITrainingSubjectRepository subjectRepository, ITrainingModuleRepository moduleRepository)
        {
            _subjectRepository = subjectRepository;
            _moduleRepository = moduleRepository;
        }

        public async Task<ITrainingModule> Create(Guid traingSubjectId, List<PeriodDateTime> periods)
        {
            var trainingSubject = await _subjectRepository.GetByIdAsync(traingSubjectId);

            if (trainingSubject == null)
                throw new ArgumentException("Training Subject must exists");

            // Unicity tests
            bool existingPeriods = await _moduleRepository.HasOverlappingPeriodsAsync(traingSubjectId, periods);

            if (existingPeriods)
                throw new ArgumentException("Training module periods intersect existing module periods for the same subject!");

            return new TrainingModule(traingSubjectId, periods);
        }

        public TrainingModule Create(ITrainingModuleVisitor trainingModuleVisitor)
        {
            return new TrainingModule(trainingModuleVisitor.Id, trainingModuleVisitor.TrainingSubjectId, trainingModuleVisitor.Periods);
        }
    }
}
