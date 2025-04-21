using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface ITrainingSubjectFactory
{
    public Task<TrainingSubject> Create(string title, string description);
    public TrainingSubject Create(ITrainingSubjectVisitor trainingSubjectVisitor);
}