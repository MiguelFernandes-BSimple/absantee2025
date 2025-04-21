using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;

public class TrainingSubjectDataModel : ITrainingSubjectVisitor
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public TrainingSubjectDataModel(TrainingSubject subject) {
        Id = subject.GetId();
        Title = subject.GetTitle();
        Description = subject.GetDescription();
    }

    public TrainingSubjectDataModel() {

    }
}