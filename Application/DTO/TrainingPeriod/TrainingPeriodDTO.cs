﻿using Domain.Models;

namespace Application.DTO;

public record TrainingPeriodDTO
{
    public Guid Id { get; set; }
    public PeriodDate PeriodDate { get; set; }

    public TrainingPeriodDTO()
    {
    }

}
