using Domain.Models;

namespace Domain.Interfaces;

public interface IAssociationFormationModuleCollaborator
{
    public long _id { get; set; }
    public long _collaboratorId { get; set; }
    public long _formationModuleId { get; set; }
    public PeriodDate _periodDate { get; set; }
    public long GetId();
    public long GetCollaboratorId();
    public long GetFormationModuleId();
}