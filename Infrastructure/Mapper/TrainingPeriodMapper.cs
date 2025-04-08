using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper
{
    public class TrainingPeriodMapper
    {
        private PeriodDateMapper _periodDateMapper;

        public TrainingPeriodMapper(PeriodDateMapper periodDateMapper)
        {
            _periodDateMapper = periodDateMapper;
        }

        public TrainingPeriod ToDomain(TrainingPeriodDataModel trainingPeriodDM)
        {
            IPeriodDate periodDate = _periodDateMapper.ToDomain(trainingPeriodDM.PeriodDate);
            TrainingPeriod trainingPeriod = new TrainingPeriod(periodDate);

            trainingPeriod.SetId(trainingPeriodDM.Id);

            return trainingPeriod;
        }

        public IEnumerable<TrainingPeriod> ToDomain(IEnumerable<TrainingPeriodDataModel> trainingPeriodsDM)
        {
            return trainingPeriodsDM.Select(ToDomain);
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