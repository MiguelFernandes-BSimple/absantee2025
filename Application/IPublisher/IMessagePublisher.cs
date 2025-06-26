namespace Application.IPublisher;

using Domain.Models;
public interface IMessagePublisher
{
    Task PublishCreatedTrainingModuleMessageAsync(Guid Id, Guid subjectId, List<PeriodDateTime> periods);
    Task PublishCreatedTrainingSubjectMessageAsync(Guid id, String description, String subject);
    Task PublishCreatedTrainingPeriodMessageAsync(Guid id, PeriodDate periodDate);

}