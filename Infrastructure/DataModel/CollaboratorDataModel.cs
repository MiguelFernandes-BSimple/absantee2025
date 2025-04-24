using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
namespace Infrastructure.DataModel;
public class CollaboratorDataModel : ICollaboratorVisitor
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public PeriodDateTime PeriodDateTime { get; set; }

    public CollaboratorDataModel(ICollaborator collaborator)
    {
        Id = collaborator.Id;
        UserId = collaborator.UserId;
        PeriodDateTime = (PeriodDateTime)collaborator.PeriodDateTime;
    }

    public CollaboratorDataModel()
    {
    }
}
