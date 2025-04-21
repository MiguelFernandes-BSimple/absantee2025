using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;
using Domain.Factory;

namespace Infrastructure.Mapper
{
    public class FormationSubjectMapper : IMapper<FormationSubject, FormationSubjectDataModel>
    {
        private IFormationSubjectFactory _formationSubjectFactory;

        public FormationSubjectMapper(IFormationSubjectFactory formationSubjectFactory)
        {
            _formationSubjectFactory = formationSubjectFactory;
        }

        public FormationSubject ToDomain(FormationSubjectDataModel formationSubjectDM)
        {
            var formationSubjectDomain = _formationSubjectFactory.Create(formationSubjectDM);
            return formationSubjectDomain;
        }

        public IEnumerable<FormationSubject> ToDomain(IEnumerable<FormationSubjectDataModel> formationSubjectsDM)
        {
            return formationSubjectsDM.Select(ToDomain);
        }

        public FormationSubjectDataModel ToDataModel(FormationSubject formationSubject)
        {
            return new FormationSubjectDataModel(formationSubject);
        }

        public IEnumerable<FormationSubjectDataModel> ToDataModel(IEnumerable<FormationSubject> formationSubjects)
        {
            return formationSubjects.Select(ToDataModel);
        }
    }
}
