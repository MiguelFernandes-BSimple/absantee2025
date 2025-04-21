using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;


[Table("Subject")]
public class SubjectDataModel : ISubjectVisitor
{
    public long Id { get; set; }

    public string Titulo { get; set; }

    public string Descricao { get; set; }

    public SubjectDataModel(ISubject subject)
    {
        Id = subject.GetId();
        Titulo = subject.GetTitulo();
        Descricao = subject.GetDescricao();
    }

    public SubjectDataModel()
    {}
}
