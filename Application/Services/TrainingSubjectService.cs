using Domain.Factory;
using Domain.IRepository;

namespace Application.Services
{
    public class TrainingSubjectService
    {
        private ITrainingSubjectRepository _trainingSubjectRepository;
        private ITrainingSubjectFactory _trainingSubjectFactory;

        public TrainingSubjectService(ITrainingSubjectRepository trainingSubjectRepository, ITrainingSubjectFactory trainingSubjectFactory)
        {
            _trainingSubjectRepository = trainingSubjectRepository;
            _trainingSubjectFactory = trainingSubjectFactory;
        }

        public async Task Add(string title, string description)
        {
            var ts = _trainingSubjectFactory.Create(title, description);
            await _trainingSubjectRepository.AddAsync(ts);
        }
    }
}
