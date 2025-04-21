using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public class TrainingModuleFactory : ITrainingModuleFactory
{
    private readonly ITrainingModuleRepository _trainingModuleRepository;
    private readonly ITrainingSubjectRepository _trainingSubjectRepository;

    public TrainingModuleFactory(ITrainingModuleRepository trainingModuleRepository, ITrainingSubjectRepository trainingSubjectRepository)
    {
        _trainingModuleRepository = trainingModuleRepository;
        _trainingSubjectRepository = trainingSubjectRepository;
    }
    public async Task<TrainingModule> Create(long trainingSubjectId, List<PeriodDateTime> periods)
    {
        // Unicity test
        // Modules for same subject cant have overlapping periods
        ITrainingSubject? ts = await _trainingSubjectRepository.GetByIdAsync(trainingSubjectId);

        if (ts == null)
            throw new ArgumentException("Invalid inputs");

        IEnumerable<ITrainingModule> tms = await _trainingModuleRepository.FindAllBySubject(trainingSubjectId);
        List<ITrainingModule> tmsList = tms.ToList();

        for (int tm = 0; tm < tms.Count(); tm++)
        {
            bool intersect = periods.Any(p => tmsList[tm].Periods.Any(p2 => p.Intersects(p2)));

            if (intersect)
                throw new ArgumentException("Invalid inputs");
        }


        return new TrainingModule(trainingSubjectId, periods);
    }

    public TrainingModule Create(ITrainingModuleVisitor trainingModuleVisitor)
    {
        return new TrainingModule(trainingModuleVisitor.TrainingSubjectId, trainingModuleVisitor.Periods);
    }
}