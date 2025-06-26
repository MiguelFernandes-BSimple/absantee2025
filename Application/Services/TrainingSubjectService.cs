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

        var result = _mapper.Map<TrainingSubject, TrainingSubjectDTO>(ts);
        return Result<TrainingSubjectDTO>.Success(result);
    }
    public async Task SubmitAsync(string subject, string description)
    {
        var TrainingSubject = await _trainingSubjectFactory.Create(
            subject,
            description
        );

        await _trainingSubjectRepository.AddAsync(TrainingSubject);
        await _trainingSubjectRepository.SaveChangesAsync();
    }
    public async Task<Result<IEnumerable<Guid>>> GetAll()
    {
        try
        {
            var trainingSubjects = await _trainingSubjectRepository.GetAllAsync();
            var trainingsubjectIds = trainingSubjects.Select(U => U.Id);

            return Result<IEnumerable<Guid>>.Success(trainingsubjectIds);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Guid>>.Failure(Error.InternalServerError(e.Message));
        }
    }
    public async Task<Result<TrainingSubjectDTO>> GetById(Guid id)
    {
        try
        {
            var trainingSubject = await _trainingSubjectRepository.GetByIdAsync(id);
            if (trainingSubject == null)
                return Result<TrainingSubjectDTO>.Failure(Error.NotFound("Subject not found"));
            var result = _mapper.Map<TrainingSubjectDTO>(trainingSubject);

            return Result<TrainingSubjectDTO>.Success(result);

        }
        catch (Exception e)
        {
            return Result<TrainingSubjectDTO>.Failure(Error.InternalServerError(e.Message));
        }
    }


}