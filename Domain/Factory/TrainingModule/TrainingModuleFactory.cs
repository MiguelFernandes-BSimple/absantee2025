using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public class TrainingModuleFactory : ITrainingModuleFactory
{

    public TrainingModule Create(long SubjectID, List<ITrainingPeriod> trainingPeriods)
    {
        return new TrainingModule(SubjectID, trainingPeriods);
    }

    public TrainingModule Create(ITrainingModuleVisitor trainingModuleVisitor)
    {
        return new TrainingModule(trainingModuleVisitor.Id, trainingModuleVisitor.SubjectID, trainingModuleVisitor.GetTrainingPeriods());
    }
}