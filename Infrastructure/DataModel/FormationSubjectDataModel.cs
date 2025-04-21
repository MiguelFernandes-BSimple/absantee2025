using Domain.Visitor;

namespace Infrastructure.DataModel
{
    public class FormationSubjectDataModel : IFormationSubjectVisitor
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public FormationSubjectDataModel(IFormationSubject formationSubject)
        {
            Id = formationSubject.GetId();
            Title = formationSubject.GetTitle();
            Description = formationSubject.GetDescription();
        }

        public FormationSubjectDataModel()
        {
        }
    }

}