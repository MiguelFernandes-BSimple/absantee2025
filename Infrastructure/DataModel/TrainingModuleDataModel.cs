using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;



[Table("TrainingModule")]

public class TrainingModuleDataModel : ITrainingModuleVisitor
{
    public long Id { get; set; }

    public Subject Assunto { get; set; }

    public List<PeriodDateTime> Periodos { get; set; }


    public TrainingModuleDataModel(ITrainingModule trainingModule)
    {
        Id = trainingModule.GetId();
        Assunto = trainingModule.GetAssunto();
        Periodos = trainingModule.GetPeriodos();
    }

    public TrainingModuleDataModel()
    {}

}