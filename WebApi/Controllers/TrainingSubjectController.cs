using Application.DTO.TrainingSubject;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/trainingsubjects")]
[ApiController]
public class TrainingSubjectController : ControllerBase
{
    private readonly TrainingSubjectService _trainingSubjectService;

    public TrainingSubjectController(TrainingSubjectService trainingSubjectService)
    {
        _trainingSubjectService = trainingSubjectService;
    }
}
