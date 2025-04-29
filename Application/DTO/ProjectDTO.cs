using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO
{
    public record ProjectDTO
    {
        public ProjectDTO()
        {
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Acronym { get; set; }
        public PeriodDate PeriodDate { get; set; }
    }
}
