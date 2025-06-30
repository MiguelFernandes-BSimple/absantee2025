/* using MassTransit;

public class TrainingModuleStateMachine : MassTransitStateMachine<TrainingModuleCreatedConsumer>
{
    public State Submitted { get; private set; }
    public State Accepted { get; private set; }
    public State Rejected { get; private set; }

    public Event<SubmitTrainingModule> SubmitTrainingModule { get; private set; } = default!;
    public Event<TrainingModuleAccepted> TrainingModuleAccepted { get; private set; } = default!;
    public Event<TrainigModuleRejected> TrainigModuleRejected { get; private set; } = default!;

    public TrainingModuleStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => SubmitTrainingModule, x => x.CorrelateById(context => context.Message.Id));
        Event(() => TrainingModuleAccepted, x => x.CorrelateById(context => context.Message.Id));
        Event(() => TrainigModuleRejected, x => x.CorrelateById(context => context.Message.Id));

        Initially(
            When(SubmitTrainingModule)
                .Then(context => Console.WriteLine($"Saga started for OrderId: {context.Data.Id}"))
                .TransitionTo(Submitted)

        );

        During(Submitted,
            When(TrainingModuleAccepted)
                .Then(context => Console.WriteLine($"Order accepted: {context.Data.Id}"))
                .TransitionTo(Accepted)
                .Finalize(),

            When(TrainigModuleRejected)
                .Then(context => Console.WriteLine($"Order rejected: {context.Data.Id}"))
                .TransitionTo(Rejected)
                .Finalize()
        );

        SetCompletedWhenFinalized();
    }
} */