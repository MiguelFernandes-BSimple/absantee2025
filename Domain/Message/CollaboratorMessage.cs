using Domain.Models;
namespace WebApi;
//dto
public record CollaboratorMessage(Guid Id, Guid UserId, PeriodDateTime PeriodDateTime);