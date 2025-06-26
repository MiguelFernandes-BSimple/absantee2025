namespace Application.DTO.TrainingSubject;

public record AddTrainingSubjectDTO
{
    public Guid id { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public AddTrainingSubjectDTO()
    {

    }
}