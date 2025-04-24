using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers
{
    public class TrainingSubjectDataModelToTrainingSubjectConverter : ITypeConverter<TrainingSubjectDataModel, TrainingSubject>
    {
        private readonly ITrainingSubjectFactory _factory;

        public TrainingSubjectDataModelToTrainingSubjectConverter(ITrainingSubjectFactory factory)
        {
            _factory = factory;
        }

        public TrainingSubject Convert(TrainingSubjectDataModel source, TrainingSubject destination, ResolutionContext context)
        {
            return _factory.Create(source);
        }
    }
}
