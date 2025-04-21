using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;



[Table("TrainingModule")]

public class TrainingModuleDataModel : ITrainingModuleVisitor
{
    public long Id { get; set; }

    public long subjectId { get; set; }

    public List<PeriodDateTime> Periodos { get; set; }


    public TrainingModuleDataModel(ITrainingModule trainingModule)
    {
        Id = trainingModule.GetId();
        subjectId = trainingModule.GetSubjectId();
        Periodos = trainingModule.GetPeriodos();
    }

    public TrainingModuleDataModel()
    {}

}