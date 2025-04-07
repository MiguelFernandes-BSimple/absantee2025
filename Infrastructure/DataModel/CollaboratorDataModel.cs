using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
namespace Infrastructure.DataModel;
public class CollaboratorDataModel
{
    public UserDataModel user { get; set; }
    public PeriodDateTimeDataModel periodDateTime { get; set; }

    public class CollaboratorDataModel()
    {
    }
}
