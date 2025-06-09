using Application.DTO.TrainingModule;
using AutoMapper;
using Domain.Factory;
using Domain.Interfaces;
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

    public async Task<Result<TrainingModuleDTO>> Add(AddTrainingModuleDTO tmDTO)
    {
        ITrainingModule tm;

        try
        {
            tm = await _trainingModuleFactory.Create(tmDTO.TrainingSubjectId, tmDTO.Periods);
            tm = await _trainingModuleRepository.AddAsync(tm);
        }
        catch (ArgumentException a)
        {
            return Result<TrainingModuleDTO>.Failure(Error.BadRequest(a.Message));
        }
        catch (Exception e)
        {
            return Result<TrainingModuleDTO>.Failure(Error.BadRequest(e.Message));
        }

        var result = _mapper.Map<TrainingModule, TrainingModuleDTO>((TrainingModule)tm);

        return Result<TrainingModuleDTO>.Success(result);
    }

    public async Task<Result<IEnumerable<TrainingModuleDTO>>> GetAllInfo()
    {
        var trainingModules = await _trainingModuleRepository.GetAllAsync();
        var resultList = trainingModules.Select(_mapper.Map<TrainingModuleDTO>);

        return Result<IEnumerable<TrainingModuleDTO>>.Success(resultList);
    }

    public async Task<Result<TrainingModuleDTO>> GetById(Guid id)
    {
        var trainingModule = await _trainingModuleRepository.GetByIdAsync(id);
        if (trainingModule == null)
        {
            return Result<TrainingModuleDTO>.Failure(Error.NotFound("Training module not found"));
        }
        var result = _mapper.Map<TrainingModuleDTO>(trainingModule);
        return Result<TrainingModuleDTO>.Success(result);
    }
}