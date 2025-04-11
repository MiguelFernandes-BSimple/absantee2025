using Domain.Factory.TrainingPeriodFactory;
using Domain.Models;
using Infrastructure.DataModel;
using Domain.Visitor;
using Domain.Interfaces;

namespace Infrastructure.Mapper
{
    public class TrainingPeriodMapper : IMapper<TrainingPeriod, TrainingPeriodDataModel>
    {

        private ITrainingPeriodFactory _trainingPeriodFactory;

        public TrainingPeriodMapper(ITrainingPeriodFactory trainingPeriodFactory)
        {
            _trainingPeriodFactory = trainingPeriodFactory;
        }

        public TrainingPeriod ToDomain(TrainingPeriodDataModel trainingPeriodDataModel)
        {
            return _trainingPeriodFactory.Create(trainingPeriodDataModel);
        }

        public IEnumerable<TrainingPeriod> ToDomain(IEnumerable<TrainingPeriodDataModel> trainingPeriodDataModels)
        {
            return trainingPeriodDataModels.Select(ToDomain);
        }

        public TrainingPeriodDataModel ToDataModel(TrainingPeriod trainingPeriod)
        {
            return new TrainingPeriodDataModel(trainingPeriod);
        }

        public IEnumerable<TrainingPeriodDataModel> ToDataModel(IEnumerable<TrainingPeriod> trainingPeriods)
        {
            return trainingPeriods.Select(ToDataModel);
        }
    }
}