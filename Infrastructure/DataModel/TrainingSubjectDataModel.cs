using Domain.Interfaces;
using Domain.Visitor;

namespace Infrastructure.DataModel;

public class TrainingSubjectDataModel : ITrainingSubjectVisitor
{
    public long Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public TrainingSubjectDataModel()
    {

    }

    public TrainingSubjectDataModel(ITrainingSubject ts)
    {
        Id = ts.Id;
        Title = ts.Title;
        Description = ts.Description;
    }
}