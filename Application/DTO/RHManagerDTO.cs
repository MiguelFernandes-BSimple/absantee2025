using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO;
public record RHManagerDTO
{
    public Guid UserId { get; set; }
    public PeriodDateTime PeriodDateTime { get; }
    public RHManagerDTO()
    {
    }
}
