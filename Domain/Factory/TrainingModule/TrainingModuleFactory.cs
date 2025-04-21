using Domain.Factory.TrainingModuleFactory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;





namespace Domain.Factory.TrainingModuleFactory;


public class TrainingModuleFactory : ITrainingModuleFactory
{
    private readonly ITrainingModuleRepository _trainingModuleRepository;
    
    public TrainingModuleFactory(ITrainingModuleRepository trainingModuleRepository){
        _trainingModuleRepository = trainingModuleRepository;
    }
    public TrainingModule Create(long subjectId, List<PeriodDateTime> periodos)
    {
        TrainingModule trainingModule = new TrainingModule(subjectId,periodos);

        return trainingModule;
    }

    public TrainingModule Create(ITrainingModuleVisitor trainingModuleVisitor)
    {
         return new TrainingModule(trainingModuleVisitor.Id,trainingModuleVisitor.subjectId,trainingModuleVisitor.Periodos);
    }
}
