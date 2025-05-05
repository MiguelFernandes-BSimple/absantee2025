using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO;
public record CreateRHManagerDTO
{
    // user attributes
    public string Names { get; set; }
    public string Surnames { get; set; }
    public string Email { get; set; }
    public DateTime deactivationDate { get; set; }

    // rhmanager attributes
    public PeriodDateTime PeriodDateTime { get; set; }

    public CreateRHManagerDTO(string names, string surnames, string email, DateTime deactivationDate, PeriodDateTime periodDateTime)
    {
        Names = names;
        Surnames = surnames;
        Email = email;
        this.deactivationDate = deactivationDate;
        PeriodDateTime = periodDateTime;
    }

    public CreateRHManagerDTO()
    {

    }
}
