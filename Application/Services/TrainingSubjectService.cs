using Application.DTO.TrainingSubject;
using AutoMapper;
using Domain.Factory;
using Domain.IRepository;
using Domain.Models;
using Infrastructure;

namespace Application.Services;

public class TrainingSubjectService
{
    private readonly ITrainingSubjectRepository _trainingSubjectRepository;
    private readonly ITrainingSubjectFactory _trainingSubjectFactory;
    private readonly IMapper _mapper;
    private AbsanteeContext _context;

    public TrainingSubjectService(ITrainingSubjectRepository trainingSubjectRepository, ITrainingSubjectFactory trainingSubjectFactory, IMapper mapper, AbsanteeContext context)
    {
        _trainingSubjectRepository = trainingSubjectRepository;
        _trainingSubjectFactory = trainingSubjectFactory;
        _mapper = mapper;
        _context = context;

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

    public async Task<Result<IEnumerable<TrainingSubjectDTO>>> GetAll()
    {
        var trainingSubjects = await _trainingSubjectRepository.GetAllAsync();
        var result = trainingSubjects.Select(_mapper.Map<TrainingSubjectDTO>);

        return Result<IEnumerable<TrainingSubjectDTO>>.Success(result);
    }

    public async Task<TrainingSubjectDTO?> UpdateTrainingSubject(TrainingSubjectDTO tsDTO)
    {
        var trainingSubject = await _trainingSubjectRepository.GetByIdAsync(tsDTO.Id);
        if (trainingSubject == null) return null;

        trainingSubject.UpdateSubject(tsDTO.Subject);
        trainingSubject.UpdateDescription(tsDTO.Description);

        var updateSubjectDetails = _trainingSubjectRepository.UpdateTrainingSubject(trainingSubject);

        if (updateSubjectDetails == null) return null;

        await _context.SaveChangesAsync();

        return new TrainingSubjectDTO(tsDTO.Id, tsDTO.Subject, tsDTO.Description);


    }
}