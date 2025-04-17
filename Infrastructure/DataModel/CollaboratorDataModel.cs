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
    public long Id { get; set; }
    public long UserID { get; set; }
    public PeriodDateTime PeriodDateTime { get; set; }

    public CollaboratorDataModel(ICollaborator collaborator)
    {
        Id = collaborator.GetId();
        UserID = collaborator.GetUserId();
        PeriodDateTime = (PeriodDateTime)collaborator.GetPeriodDateTime();
    }

    public CollaboratorDataModel()
    {
    }
}
