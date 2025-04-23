using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TrainingSubjectRepository : GenericRepository<ITrainingSubject, ITrainingSubjectVisitor>, ITrainingSubjectRepository
    {
        private readonly IMapper _mapper;
        public TrainingSubjectRepository(AbsanteeContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public override ITrainingSubject? GetById(long id)
        {
            try
            {
                var tsDM = _context.Set<TrainingSubjectDataModel>()
                                   .FirstOrDefault();

                if (tsDM == null)
                    return null;

                var ts = _mapper.Map<TrainingSubjectDataModel, TrainingSubject>(tsDM);
                return ts;
            }
            catch
            {
                throw;
            }
        }

        public override async Task<ITrainingSubject?> GetByIdAsync(long id)
        {
            try
            {
                var tsDM = await _context.Set<TrainingSubjectDataModel>()
                                   .FirstOrDefaultAsync();

                if (tsDM == null)
                    return null;

                var ts = _mapper.Map<TrainingSubjectDataModel, TrainingSubject>(tsDM);
                return ts;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> IsDuplicated(string subject)
        {
            return await _context.Set<TrainingSubjectDataModel>()
                           .AnyAsync(t => t.Subject.Equals(subject));
        }
    }
}
