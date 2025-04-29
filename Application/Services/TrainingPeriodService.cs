using Application.DTO;
using AutoMapper;
using Domain.Factory.TrainingPeriodFactory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;

namespace Application.Services;

public class TrainingPeriodService
{
    private readonly ITrainingPeriodRepository _repository;
    private readonly ITrainingPeriodFactory _factory;
    private readonly IMapper _mapper;

    public TrainingPeriodService(ITrainingPeriodRepository repository, ITrainingPeriodFactory factory, IMapper mapper)
    {
        _repository = repository;
        _factory = factory;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ITrainingPeriod>> GetAll()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ITrainingPeriod?> GetProjectById(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    // UC2
    public async Task<TrainingPeriodDTO> Add(TrainingPeriodDTO trainingDTO)
    {
        TrainingPeriod trainingPeriod;
        try
        {
            trainingPeriod = _factory.Create(trainingDTO.PeriodDate);
            await _repository.AddAsync(trainingPeriod);
        }
        catch (Exception)
        {
            return null;
        }

        return _mapper.Map<TrainingPeriod, TrainingPeriodDTO>(trainingPeriod);
    }
}
