using Domain.Models;

namespace Application.DTO
{
    public record CollaboratorDTO
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public PeriodDateTime PeriodDateTime { get; }
    }
}
