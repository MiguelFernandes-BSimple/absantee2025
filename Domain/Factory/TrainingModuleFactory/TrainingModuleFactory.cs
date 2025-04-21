using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public class TrainingModuleFactory : ITrainingModuleFactory
{
    private readonly ISubjectRepository _subjectRepo;

    public TrainingModuleFactory(ISubjectRepository srepo)
    {
        _subjectRepo = srepo;
    }

    public TrainingModule Create(long subjectId, List<PeriodDateTime> periodsList)
    {
        if (_subjectRepo.GetById(subjectId) == null)
        {
            throw new ArgumentException("Subject does not exist");
        }

        return new TrainingModule(subjectId, periodsList);
    }

    public TrainingModule Create(ITrainingModuleVisitor visitor)
    {
        return new TrainingModule(visitor.id, visitor.subjectId, visitor.periodsList);
    }
}