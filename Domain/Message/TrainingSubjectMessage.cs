using Domain.Models;
namespace WebApi.Message;
//dto
public record TrainingSubjectMessage(Guid Id, string Subject, string Description);