using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO
{
    public class CollaboratorDTO
    {
        public long UserId { get; set; }
        public PeriodDateTime PeriodDateTime { get; set; }
    }
}