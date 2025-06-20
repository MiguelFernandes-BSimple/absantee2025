using Application.DTO.TrainingModule;
using Application.IPublisher;
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
    private readonly IMessagePublisher _publisher;


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

        await _publisher.PublishCreatedTrainingModuleMessageAsync(result.Id, result.TrainingSubjectId, result.Periods.First());

        return Result<TrainingModuleDTO>.Success(result);
    }
    public async Task SubmitAsync(Guid subjectId, PeriodDateTime periodDateTime)
    {
        var trainingModule = await _trainingModuleFactory.Create(
            subjectId,
            new List<PeriodDateTime> { periodDateTime }
        );

        await _trainingModuleRepository.AddAsync(trainingModule);
        await _trainingModuleRepository.SaveChangesAsync();
    }
}