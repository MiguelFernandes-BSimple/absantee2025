using Application.DTO.TrainingModule;
using AutoMapper;
using Domain.Factory;
using Domain.IRepository;
using Domain.Models;

namespace Application.Services;

public class TrainingModuleService
{
    private readonly ITrainingModuleRepository _trainingModuleRepository;
    private readonly ITrainingModuleFactory _trainingModuleFactory;
    private readonly IMapper _mapper;

    public TrainingModuleService(ITrainingModuleRepository trainingModuleRepository, ITrainingModuleFactory trainingModuleFactory, IMapper mapper)
    {
        _trainingModuleRepository = trainingModuleRepository;
        _trainingModuleFactory = trainingModuleFactory;
        _mapper = mapper;
    }

    public async Task<TrainingModuleDTO> Add(AddTrainingModuleDTO tmDTO)
    {
        TrainingModule tm;

        tm = await _trainingModuleFactory.Create(tmDTO.TrainingSubjectId, tmDTO.Periods);
        tm = await _trainingModuleRepository.AddAsync(tm);

        return _mapper.Map<TrainingModule, TrainingModuleDTO>(tm);
    }
}