namespace Application.DTO.TrainingSubject;

public record TrainingSubjectDTO
{
    public Guid Id { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public TrainingSubjectDTO()
    {

    }
}