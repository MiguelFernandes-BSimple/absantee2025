using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TrainingModuleRepository : GenericRepository<ITrainingModule, ITrainingModuleVisitor>, ITrainingModuleRepository
    {
        public TrainingModuleRepository(DbContext context, IMapper<ITrainingModule, ITrainingModuleVisitor> mapper) : base(context, mapper)
        {
        }

        public override ITrainingModule? GetById(long id)
        {
            throw new NotImplementedException();
        }

        public override Task<ITrainingModule?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ITrainingModule>> GetBySubjectIdAndFinished(long subjectId)
        {
            var trainingModuleDMs = _context.Set<TrainingModuleDataModel>()
                                            .Where(t => t.TrainingSubjectId == subjectId
                                                    && !t.Periods.Any(p => DateTime.Now > p._finalDate));

            throw new NotImplementedException();
        }
    }
}
