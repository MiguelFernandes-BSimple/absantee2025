namespace Application.DTO.TrainingSubject;

public record AddTrainingSubjectDTO
{
    public string Subject { get; set; }
    public string Description { get; set; }
    public AddTrainingSubjectDTO()
    {

    }
}