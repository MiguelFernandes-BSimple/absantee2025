using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SubjectRepository : GenericRepository<ISubject, SubjectDataModel>, ISubjectRepository
    {
        private readonly SubjectMapper _mapper;

        public SubjectRepository(AbsanteeContext context, SubjectMapper mapper) : base(context, (IMapper<ISubject, SubjectDataModel>)mapper)
        {
            _mapper = mapper;
        }

        public override ISubject? GetById(long id)
        {
            var subjectDM = this._context.Set<SubjectDataModel>().FirstOrDefault(s => s._id == id);

            if (subjectDM == null)
                return null;

            var subject = _mapper.ToDomain(subjectDM);
            return subject;
        }

        public override async Task<ISubject?> GetByIdAsync(long id)
        {
            var subjectDM = await this._context.Set<SubjectDataModel>().FirstOrDefaultAsync(s => s._id == id);

            if (subjectDM == null)
                return null;

            var subject = _mapper.ToDomain(subjectDM);
            return subject;
        }

        public bool TitleExists(string title)
        {
            var titleExists = _context.Set<SubjectDataModel>().FirstOrDefault(t => t._title.Equals(title));

            if (titleExists == null) return false;

            return true;
        }
    }
}