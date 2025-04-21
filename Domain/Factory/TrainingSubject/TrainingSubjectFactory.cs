using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public class TrainingSubjectFactory : ITrainingSubjectFactory
{
    private readonly ITrainingSubjectRepository _trainingSubjectRepository;
    public TrainingSubjectFactory(ITrainingSubjectRepository trainingSubjectRepository)
    {
        _trainingSubjectRepository = trainingSubjectRepository;
    }

    public async Task<TrainingSubject> Create(string title, string description)
    {
        // Unicity test
        ITrainingSubject? ts = await _trainingSubjectRepository.FindByTitle(title);

        if (ts != null)
            throw new ArgumentException("Invalid inputs");

        return new TrainingSubject(title, description);
    }

    public TrainingSubject Create(ITrainingSubjectVisitor trainingPeriodVisitor)
    {
        return new TrainingSubject(trainingPeriodVisitor.Title, trainingPeriodVisitor.Description);
    }
}