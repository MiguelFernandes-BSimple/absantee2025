using Application.DTO.TrainingSubject;
using AutoMapper;
using Domain.Factory;
using Domain.IRepository;
using Domain.Models;
using Application.IPublisher;
namespace Application.Services;

public class TrainingSubjectService
{
    private readonly ITrainingSubjectRepository _trainingSubjectRepository;
    private readonly ITrainingSubjectFactory _trainingSubjectFactory;
    private readonly IMessagePublisher _publisher;
    private readonly IMapper _mapper;

    public TrainingSubjectService(ITrainingSubjectRepository trainingSubjectRepository, ITrainingSubjectFactory trainingSubjectFactory, IMapper mapper, IMessagePublisher publisher)

    {
        _trainingSubjectRepository = trainingSubjectRepository;
        _trainingSubjectFactory = trainingSubjectFactory;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<Result<TrainingSubjectDTO>> Add(AddTrainingSubjectDTO tsDTO)
    {
        TrainingSubject ts;

        try
        {
            ts = await _trainingSubjectFactory.Create(tsDTO.Subject, tsDTO.Description);
            await _trainingSubjectRepository.AddAsync(ts);
        }
        catch (ArgumentException a)
        {
            return Result<TrainingSubjectDTO>.Failure(Error.BadRequest(a.Message));
        }
        catch (Exception e)
        {
            return Result<TrainingSubjectDTO>.Failure(Error.InternalServerError(e.Message));
        }
        await _publisher.PublishCreatedTrainingSubjectMessageAsync(ts.Id, ts.Subject, ts.Description);

        var result = _mapper.Map<TrainingSubject, TrainingSubjectDTO>(ts);
        return Result<TrainingSubjectDTO>.Success(result);
    }
    public async Task SubmitAsync(Guid Id, string subject, string description)
    {

        var exists = await _trainingSubjectRepository.ExistsAsync(Id);
        if (exists)
        {
            throw new ArgumentException($"Training subject with name {subject} already exists.");
        }

        var TrainingSubject = await _trainingSubjectFactory.Create(
            subject,
            description
        );

        await _trainingSubjectRepository.AddAsync(TrainingSubject);
        await _trainingSubjectRepository.SaveChangesAsync();
    }
}