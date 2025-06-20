namespace Application.IPublisher;

using Domain.Models;
public interface IMessagePublisher
{
    Task PublishCreatedTrainingModuleMessageAsync(Guid Id, Guid subjectId, PeriodDateTime periodDateTime);
}