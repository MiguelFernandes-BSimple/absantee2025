using Application.Services;
using WebApi;
using MassTransit;
using WebApi.Message;

public class TrainingSubjectCreatedConsumer : IConsumer<TrainingSubjectMessage>
{
    private readonly TrainingSubjectService _trainingSubjectService;

    public TrainingSubjectCreatedConsumer(TrainingSubjectService trainingSubjectService)
    {
        _trainingSubjectService = trainingSubjectService;
    }
    public async Task Consume(ConsumeContext<TrainingSubjectMessage> context)
    {
        var senderId = context.Headers.Get<string>("SenderId");
        if (senderId == InstanceInfo.InstanceId)
            return;
        var msg = context.Message;
        await _trainingSubjectService.SubmitAsync(msg.Id, msg.Subject, msg.Description);
    }
}