using Application.DTO.TrainingSubject;
using AutoMapper;
using Domain.Factory;
using Domain.IRepository;
using Domain.Models;

namespace Application.Services;

public class TrainingSubjectService
{
    private readonly ITrainingSubjectRepository _trainingSubjectRepository;
    private readonly ITrainingSubjectFactory _trainingSubjectFactory;
    private readonly IMapper _mapper;

    public TrainingSubjectService(ITrainingSubjectRepository trainingSubjectRepository, ITrainingSubjectFactory trainingSubjectFactory, IMapper mapper)
    {
        _trainingSubjectRepository = trainingSubjectRepository;
        _trainingSubjectFactory = trainingSubjectFactory;
        _mapper = mapper;
    }
}