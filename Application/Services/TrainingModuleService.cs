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

    public async Task<Result<TrainingModuleDTO>> Add(AddTrainingModuleDTO tmDTO)
    {
        TrainingModule tm;

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

        var result = _mapper.Map<TrainingModule, TrainingModuleDTO>(tm);

        return Result<TrainingModuleDTO>.Success(result);
    }
}