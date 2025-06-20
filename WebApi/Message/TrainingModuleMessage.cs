using Domain.Models;
namespace WebApi;
//dto
public record TrainingModuleMessage(Guid Id, Guid SubjectId, PeriodDateTime PeriodDateTime);