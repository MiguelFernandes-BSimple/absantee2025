using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper
{
    public class TrainingPeriodMapper
    {
        public TrainingPeriod ToDomain(TrainingPeriodDataModel trainingPeriodDM)
        {
            var trainingPeriodInitDate = trainingPeriodDM.PeriodDate._initDate;
            var trainingPeriodFinalDate = trainingPeriodDM.PeriodDate._finalDate;

            IPeriodDate periodDate = new PeriodDate(trainingPeriodInitDate, trainingPeriodFinalDate);
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