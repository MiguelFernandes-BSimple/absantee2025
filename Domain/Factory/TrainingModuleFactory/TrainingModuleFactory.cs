using Domain.Models;

namespace Domain.Factory;

public class TrainingModuleFactory : ITrainingModuleFactory
{
    public TrainingModuleFactory() {
        
    }
    public async Task<TrainingModule> Create(long subjectId, List<PeriodDateTime> periods)
    {
        if (!periods.Any()) {
            throw new ArgumentException("No periods given");
        }

        if(periods.Any(p1 => periods.Any(p2 => p2.Intersects(p1) && !p1.Equals(p2)))) {
            throw new ArgumentException("Periods intercept");
        }

        if(periods.All(p => p._initDate > DateTime.Now)) {
            throw new ArgumentException("Periods must be in the future");
        }

        TrainingModule trainingModule = new TrainingModule(subjectId, periods);

        return trainingModule;
    }

    /*public TrainingModule Create(ITrainingModuleVisitor visitor) {
        return new TrainingModule(visitor.id, visitor.subjectId, visitor.Periods)
    }*/
}
