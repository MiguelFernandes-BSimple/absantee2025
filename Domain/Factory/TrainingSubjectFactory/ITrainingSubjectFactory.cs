using Domain.Models;

public interface ITrainingSubjectFactory {
    Task<TrainingSubject> Create(string title, string description);
    //TrainingSubject Create(ITrainingSubjectVisitor visitor);
}
