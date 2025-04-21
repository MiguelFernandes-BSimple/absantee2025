using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TrainingModuleRepository : GenericRepository<ITrainingModule, ITrainingModuleVisitor>, ITrainingModuleRepository
    {
        private readonly IMapper<ITrainingModule, ITrainingModuleVisitor> _mapper;
        public TrainingModuleRepository(DbContext context, IMapper<ITrainingModule, ITrainingModuleVisitor> mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public override ITrainingModule? GetById(long id)
        {
            throw new NotImplementedException();
        }

        public override Task<ITrainingModule?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ITrainingModule>> GetBySubjectIdAndFinished(long subjectId, DateTime date)
        {
            var trainingModulesDMs = await _context.Set<TrainingModuleDataModel>()
                                            .Where(t => t.TrainingSubjectId == subjectId
                                                    && !t.Periods.Any(p => date > p._finalDate))
                                            .ToListAsync();

            var trainingModules = _mapper.ToDomain(trainingModulesDMs);

            return trainingModules;
        }
    }
}
