using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
namespace Infrastructure.DataModel;
public class CollaboratorDataModel
{
    public long Id { get; set; }
    public long UserID { get; set; }
    public User User { get; set; }
    public PeriodDateTimeDataModel PeriodDateTime { get; set; }

    public CollaboratorDataModel()
    {
    }
    public CollaboratorDataModel(Collaborator collaborator)
    {
        Id = collaborator.GetId();
        UserID = collaborator.GetUserId();
        User = (User)collaborator.GetUser();
        PeriodDateTime = new PeriodDateTimeDataModel(collaborator.GetPeriodDateTime());
    }
}
