namespace Domain.Interfaces;

public interface IAssociationTrainingModuleCollaborator {
    public long _id { get; set; }
    public long _collaboratorId { get; set; }
    public long _trainingModuleId { get; set; }

    long GetId();
    long GetCollaboratorId();
    long GetTrainingModuleId();
}
