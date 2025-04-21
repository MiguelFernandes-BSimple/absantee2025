using Domain.Models;
using Domain.Visitor;


namespace Domain.Factory.TrainingModuleFactory;


public interface ITrainingModuleFactory
{
    public TrainingModule Create(long subjectId,List<PeriodDateTime> periodos);

    public TrainingModule Create(ITrainingModuleVisitor trainingModuleVisitor);
}