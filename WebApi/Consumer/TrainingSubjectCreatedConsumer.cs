using Application.Services;
using WebApi;
using MassTransit;

public class TrainingSubjectCreatedConsumer : IConsumer<TrainingSubjectMessage>
{
    private readonly TrainingSubjectService _trainingSubjectService;

    public TrainingSubjectCreatedConsumer(TrainingSubjectService trainingSubjectService)
    {
        _trainingSubjectService = trainingSubjectService;
    }
    public async Task Consume(ConsumeContext<TrainingSubjectMessage> context)
    {
        var msg = context.Message;
        await _trainingSubjectService.SubmitAsync(msg.Subject, msg.Description);
    }
}