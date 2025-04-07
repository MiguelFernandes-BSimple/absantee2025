using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProjectRepositoryEF : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepositoryEF(AbsanteeContext context) : base(context)
        {
        }
    }
}
