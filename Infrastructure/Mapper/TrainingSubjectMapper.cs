using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Mapper;

public class TrainingSubjectMapper : IMapper<TrainingSubject, TrainingSubjectDataModel>
{
    private ITrainingSubjectFactory _factory;

    public TrainingSubjectMapper(ITrainingSubjectFactory factory)
    {
        _factory = factory;
    }

    public TrainingSubjectDataModel ToDataModel(TrainingSubject domainEntity)
    {
        return new TrainingSubjectDataModel(domainEntity);
    }

    public IEnumerable<TrainingSubjectDataModel> ToDataModel(IEnumerable<TrainingSubject> dataModels)
    {
        return dataModels.Select(ToDataModel);
    }

    public TrainingSubject ToDomain(TrainingSubjectDataModel dataModel)
    {
        var tsDomain = _factory.Create(dataModel);

        return tsDomain;
    }

    public IEnumerable<TrainingSubject> ToDomain(IEnumerable<TrainingSubjectDataModel> dataModels)
    {
        return dataModels.Select(ToDomain);
    }
}