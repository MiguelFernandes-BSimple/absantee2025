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


    public TrainingModuleService(ITrainingModuleRepository trainingModuleRepository, ITrainingModuleFactory trainingModuleFactory, IMapper mapper, IMessagePublisher publisher)
    {
        _trainingModuleRepository = trainingModuleRepository;
        _trainingModuleFactory = trainingModuleFactory;
        _mapper = mapper;
        _publisher = publisher;
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
        await _publisher.PublishCreatedTrainingModuleMessageAsync(tm.Id, tm.TrainingSubjectId, tm.Periods);


        var result = _mapper.Map<TrainingModule, TrainingModuleDTO>((TrainingModule)tm);

        return Result<TrainingModuleDTO>.Success(result);
    }
    public async Task SubmitAsync(Guid subjectId, List<PeriodDateTime> periods)
    {
        var trainingModule = await _trainingModuleFactory.Create(
            subjectId,
            periods
        );

        await _trainingModuleRepository.AddAsync(trainingModule);
        await _trainingModuleRepository.SaveChangesAsync();
    }
}