using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public class TrainingSubjectFactory : ITrainingSubjectFactory
{
    private readonly ITrainingSubjectRepository _repository;

    public TrainingSubjectFactory(ITrainingSubjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<TrainingSubject> Create(Guid id, string subject, string description)
    {
        if (await _repository.IsDuplicated(subject))
            throw new ArgumentException("Subject must be unique");

        return new TrainingSubject(id, subject, description);
    }

    public TrainingSubject Create(ITrainingSubjectVisitor trainingSubjectVisitor)
    {
        return new TrainingSubject(trainingSubjectVisitor.Id, trainingSubjectVisitor.Subject, trainingSubjectVisitor.Description);
    }
}
