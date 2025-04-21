using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;

namespace Application.Services
{
    public class TrainingModuleService
    {
        private ITrainingModuleRepository _trainingModuleRepository;
        private ITrainingSubjectRepository _trainingSubjectRepository;
        private ICollaboratorRepository _collabRepository;
        private ITrainingModuleFactory _trainingModuleFactory;

        public TrainingModuleService(ITrainingModuleRepository trainingModuleRepository, ITrainingSubjectRepository trainingSubjectRepository, ICollaboratorRepository collabRepository, ITrainingModuleFactory trainingModuleFactory)
        {
            _trainingModuleRepository = trainingModuleRepository;
            _trainingSubjectRepository = trainingSubjectRepository;
            _collabRepository = collabRepository;
            _trainingModuleFactory = trainingModuleFactory;
        }

        public async Task Add(long trainingSubjectId, List<PeriodDateTime> periods)
        {
            ITrainingSubject? ts = await _trainingSubjectRepository.GetByIdAsync(trainingSubjectId);

            if (ts == null)
                throw new ArgumentException("Invalid inputs");

            var tm = await _trainingModuleFactory.Create(trainingSubjectId, periods);
            await _trainingModuleRepository.AddAsync(tm);
        }
    }
}
