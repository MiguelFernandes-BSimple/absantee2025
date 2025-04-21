using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.Mapper;

namespace Infrastructure.DataModel;

[Table("TrainingModule")]
public class TrainingModuleDataModel : ITrainingModuleVisitor
{
    private IMapper<ITrainingPeriod, TrainingPeriodDataModel> _mapper;
    public long Id { get; set; }
    public long SubjectID { get; set; }
    public List<TrainingPeriodDataModel> TrainingPeriodsdDM { get; set; }

    public List<ITrainingPeriod> GetTrainingPeriods()
    {
        return _mapper.ToDomain(TrainingPeriodsdDM).ToList();
    }

    public TrainingModuleDataModel(ITrainingModule trainingModule, IMapper<ITrainingPeriod, TrainingPeriodDataModel> mapper)
    {
        _mapper = mapper;
        Id = trainingModule.GetId();
        SubjectID = trainingModule.GetSubjectId();
        TrainingPeriodsdDM = mapper.ToDataModel(trainingModule.GetTrainingPeriods()).ToList();
    }

    public TrainingModuleDataModel()
    {
    }
}