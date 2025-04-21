using Domain.Models;
using Infrastructure.DataModel;
using Domain.Factory;
using Domain.Factory.TrainingPeriodFactory;
using Domain.Interfaces;

namespace Infrastructure.Mapper
{
    public class FormationPeriodMapper : IMapper<IFormationPeriod, FormationPeriodDataModel>
    {

        private readonly FormationPeriodFactory _formationPeriodFactory;

        public FormationPeriodMapper(FormationPeriodFactory formationPeriodFactory)
        {
            _formationPeriodFactory = formationPeriodFactory;
        }

        public IFormationPeriod ToDomain(FormationPeriodDataModel formationPeriodDataModel)
        {
            return _formationPeriodFactory.Create(formationPeriodDataModel);
        }

        public IEnumerable<IFormationPeriod> ToDomain(IEnumerable<FormationPeriodDataModel> formationPeriodDataModel)
        {
            return formationPeriodDataModel.Select(ToDomain);
        }

        public FormationPeriodDataModel ToDataModel(IFormationPeriod formationPeriod)
        {
            return new FormationPeriodDataModel(formationPeriod);
        }

        public IEnumerable<FormationPeriodDataModel> ToDataModel(IEnumerable<IFormationPeriod> formationPeriods)
        {
            return formationPeriods.Select(ToDataModel);
        }
    }
}