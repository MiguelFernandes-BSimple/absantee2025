using Domain.Factory.TrainingPeriodFactory;
using Domain.Models;
using Infrastructure.DataModel;
using Domain.Visitor;
using Domain.Interfaces;

namespace Infrastructure.Mapper
{
    public class TrainingPeriodMapper : IMapper<ITrainingPeriod, TrainingPeriodDataModel>
    {

        private ITrainingPeriodFactory _trainingPeriodFactory;

        public TrainingPeriodMapper(TrainingPeriodFactory trainingPeriodFactory)
        {
            _trainingPeriodFactory = trainingPeriodFactory;
        }

        public ITrainingPeriod ToDomain(TrainingPeriodDataModel trainingPeriodDataModel)
        {
            TrainingPeriod trainingPeriod = _trainingPeriodFactory.Create(trainingPeriodDataModel);
            return trainingPeriod;
        }

        public IEnumerable<ITrainingPeriod> ToDomain(IEnumerable<TrainingPeriodDataModel> trainingPeriodDataModels)
        {
            return trainingPeriodDataModels.Select(ToDomain);
        }

        public TrainingPeriodDataModel ToDataModel(ITrainingPeriod trainingPeriod)
        {
            return new TrainingPeriodDataModel(trainingPeriod);
        }

        public IEnumerable<TrainingPeriodDataModel> ToDataModel(IEnumerable<ITrainingPeriod> trainingPeriods)
        {
            return trainingPeriods.Select(ToDataModel);
        }
    }
}