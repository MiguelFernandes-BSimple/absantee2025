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

    public async Task<TrainingSubjectDTO> Add(AddTrainingSubjectDTO tsDTO)
    {
        TrainingSubject ts;

        ts = await _trainingSubjectFactory.Create(tsDTO.Subject, tsDTO.Description);
        ts = await _trainingSubjectRepository.AddAsync(ts);

        return _mapper.Map<TrainingSubject, TrainingSubjectDTO>(ts);
    }
}