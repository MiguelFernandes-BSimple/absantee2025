using Domain.Interfaces;
using Domain.Visitor;

namespace Infrastructure.DataModel;

public class TrainingSubjectDataModel : ITrainingSubjectVisitor
{
    public Guid Id { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }

    public TrainingSubjectDataModel()
    {
    }

    public TrainingSubjectDataModel(ITrainingSubject trainingSubject)
    {
        Id = trainingSubject.Id;
        Subject = trainingSubject.Subject;
        Description = trainingSubject.Description;
    }

}
