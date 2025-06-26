﻿using Application.DTO;
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
    public async Task<TrainingPeriodDTO> Add(CreateTrainingPeriodDTO trainingPeriodDTO)
    {
        TrainingPeriod trainingPeriod;
        try
        {
            trainingPeriod = _factory.Create(trainingPeriodDTO.InitDate, trainingPeriodDTO.FinalDate);
            await _repository.AddAsync(trainingPeriod);
            return _mapper.Map<TrainingPeriod, TrainingPeriodDTO>(trainingPeriod);
        }
        catch (Exception)
        {
            return null;
        }
    }
    public async Task SubmitAsync(DateOnly initDate, DateOnly finalDate)
    {
        var trainingModule = _factory.Create(initDate, finalDate);
        await _repository.AddAsync(trainingModule);
        await _repository.SaveChangesAsync();
    }
}
