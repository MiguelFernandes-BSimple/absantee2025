using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO;

public record RHManagerDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public PeriodDateTime PeriodDateTime { get; set; }

    public RHManagerDTO(Guid id, Guid userId, PeriodDateTime periodDateTime)
    {
        Id = id;
        UserId = userId;
        PeriodDateTime = periodDateTime;
    }

    public RHManagerDTO()
    {

    }
}
