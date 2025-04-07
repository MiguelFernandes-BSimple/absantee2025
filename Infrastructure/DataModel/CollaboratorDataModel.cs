using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
namespace Infrastructure.DataModel;
public class CollaboratorDataModel
{
    public long Id { get; set; }
    public UserDataModel User { get; set; }
    public PeriodDateTimeDataModel PeriodDateTime { get; set; }

    public CollaboratorDataModel()
    {
    }
}
