using System.Security.Principal;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Visitor;

public interface IHolidayPlanVisitor
{
    Guid Id { get; }
    Guid CollaboratorId { get; }
    List<IHolidayPeriod> GetHolidayPeriods(IMapper mapper);
}