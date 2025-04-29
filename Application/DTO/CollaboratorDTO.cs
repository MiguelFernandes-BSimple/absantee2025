using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO
{
    public class CollaboratorDTO
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public PeriodDateTime PeriodDateTime { get; }
    }
}
