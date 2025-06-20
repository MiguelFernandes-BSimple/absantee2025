using Domain.Models;
using WebApi;
using MassTransit;
using Application.IPublisher;

public class MassTransitPublisher : IMessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishCreatedTrainingModuleMessageAsync(Guid id, Guid subjectId, PeriodDateTime periodDateTime)
    {
        var eventMessage = new TrainingModuleMessage(id, subjectId, periodDateTime);
        await _publishEndpoint.Publish(eventMessage);
    }
}