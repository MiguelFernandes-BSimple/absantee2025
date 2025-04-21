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
    public TrainingModule Create(Subject assunto, List<PeriodDateTime> periodos)
    {
        TrainingModule trainingModule = new TrainingModule(assunto,periodos);

        return trainingModule;
    }

    public TrainingModule Create(ITrainingModuleVisitor trainingModuleVisitor)
    {
         return new TrainingModule(trainingModuleVisitor.Id,trainingModuleVisitor.Assunto,trainingModuleVisitor.Periodos);
    }
}
