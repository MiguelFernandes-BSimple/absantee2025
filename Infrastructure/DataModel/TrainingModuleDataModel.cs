using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.Mapper;

namespace Infrastructure.DataModel;

[Table("TrainingModule")]
public class TrainingModuleDataModel : ITrainingModuleVisitor
{
    public long Id { get; set; }
    public long SubjectID { get; set; }
    public List<PeriodDateTime> PeriodDateTime { get; set; }

    public TrainingModuleDataModel(ITrainingModule trainingModule)
    {
        Id = trainingModule.GetId();
        SubjectID = trainingModule.GetSubjectId();
        PeriodDateTime = trainingModule.GetPeriodDateTimes().ToList();
    }

    public TrainingModuleDataModel()
    {
    }
}