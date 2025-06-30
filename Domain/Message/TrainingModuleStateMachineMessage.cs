public record TrainingModuleSubmitted(Guid Id, DateTime Timestamp);

public record SubmitTrainingModule(Guid Id);

public record TrainingModuleAccepted(Guid Id);

public record TrainigModuleRejected(Guid Id);