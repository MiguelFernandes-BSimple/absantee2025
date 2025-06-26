using Domain.Models;
using WebApi;
using MassTransit;
using Application.IPublisher;

public class MassTransitPublisher : IMessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    }

    public async Task PublishCreatedTrainingModuleMessageAsync(Guid id, Guid subjectId, List<PeriodDateTime> periods)
    {
        var eventMessage = new TrainingModuleMessage(id, subjectId, periods);
        await _publishEndpoint.Publish(eventMessage);
    }

    public async Task PublishCreatedTrainingSubjectMessageAsync(Guid id, String description, String subject)
    {
        var eventMessage = new TrainingSubjectMessage(id, description, subject);
        await _publishEndpoint.Publish(eventMessage);
    }

    public async Task PublishCreatedTrainingPeriodMessageAsync(Guid id, PeriodDate periodDate)
    {
        var eventMessage = new TrainingPeriodMessage(id, periodDate);
        await _publishEndpoint.Publish(eventMessage);
    }
}